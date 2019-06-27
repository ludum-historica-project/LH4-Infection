using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCanvas : MonoBehaviour
{
    public ScriptableEvent OnPlayerHealthChanged;
    public ScriptableEvent OnMutationCountChanged;
    public ScriptableEvent OnInfectionMultChanged;
    public TextMeshProUGUI mutationText;
    public TextMeshProUGUI infectionText;

    // Start is called before the first frame update


    void Start()
    {
        OnMutationCountChanged.OnInvoke += UpdateMutationCount;
        OnInfectionMultChanged.OnInvoke += UpdateInfectionCount;
        OnPlayerHealthChanged.OnInvoke += ReviseMenuStatus;
        UpdateMutationCount(Director.GetManager<UpgradesManager>().upgradePoints);
        UpdateInfectionCount(1);
    }
    void UpdateMutationCount(float count)
    {
        mutationText.text = count.ToString();
    }

    void UpdateInfectionCount(float count)
    {
        infectionText.text = "x" + count.ToString();
    }

    void ReviseMenuStatus(float count)
    {
        if (count <= 0)
        {
            OnMutationCountChanged.OnInvoke -= UpdateMutationCount;
            OnInfectionMultChanged.OnInvoke -= UpdateInfectionCount;
            OnPlayerHealthChanged.OnInvoke -= ReviseMenuStatus;
            Destroy(gameObject);
        }
    }
}
