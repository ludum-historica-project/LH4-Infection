using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UpgradeDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image upgradeIcon;
    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeLevel;
    public TextMeshProUGUI upgradeCost;

    public Upgrade upgrade;

    public System.Action OnMouseOver = () => { };
    public System.Action OnMouseExit = () => { };
    public System.Action OnClick = () => { };

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        upgradeIcon.sprite = upgrade.sprite;
        upgradeName.text = upgrade.name;
        //upgradeDescription.text = upgrade.description;
        upgradeLevel.text = "Level: " + upgrade.level + " > " + (upgrade.level + 1);
        upgradeCost.text = "" + upgrade.Cost;
    }
    public void Click()
    {
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit();
    }
}
