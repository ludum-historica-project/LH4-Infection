using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesMenu : MonoBehaviour
{
    public UpgradeDisplay upgradeDisplayPrefab;
    public Transform container;
    public TextMeshProUGUI availablePointsLabel;
    public TextMeshProUGUI descriptionText;

    private List<UpgradeDisplay> _displays = new List<UpgradeDisplay>();
    // Start is called before the first frame update

    void Start()
    {
        int availablePoints = Director.GetManager<UpgradesManager>().upgradePoints;
        availablePointsLabel.text = availablePoints.ToString();
        foreach (var upgrade in Director.GetManager<UpgradesManager>().upgrades)
        {
            var display = Instantiate(upgradeDisplayPrefab, container);
            display.SetUpgrade(upgrade);
            display.GetComponent<Button>().interactable = availablePoints >= upgrade.Cost;
            display.OnClick = () => { PurchaseUpgrade(upgrade); };
            display.OnMouseOver = () => { descriptionText.text = upgrade.description; };
            display.OnMouseExit = () => { descriptionText.text = ""; };
            _displays.Add(display);
        }
    }

    void Refresh()
    {
        int availablePoints = Director.GetManager<UpgradesManager>().upgradePoints;
        availablePointsLabel.text = availablePoints.ToString();
        foreach (var display in _displays)
        {
            display.GetComponent<Button>().interactable = availablePoints >= display.upgrade.Cost;
            display.SetUpgrade(display.upgrade);
        }
    }

    void PurchaseUpgrade(Upgrade upgrade)
    {
        Director.GetManager<UpgradesManager>().PurchaseUpgrade(upgrade);
        Refresh();
    }

    // Update is called once per frame
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }
}
