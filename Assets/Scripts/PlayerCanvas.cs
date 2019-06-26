﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerCanvas : MonoBehaviour
{
    public ScriptableEvent OnMutationCountChanged;
    public ScriptableEvent OnInfectionMultChanged;
    public TextMeshProUGUI mutationText;
    public TextMeshProUGUI infectionText;

    // Start is called before the first frame update


    void Start()
    {
        OnMutationCountChanged.OnInvoke += UpdateMutationCount;
        OnInfectionMultChanged.OnInvoke += UpdateInfectionCount;
        UpdateMutationCount(Director.GetManager<UpgradesManager>().upgradePoints);
        UpdateInfectionCount(1);
    }
    void UpdateMutationCount(float count)
    {
        mutationText.text = "Mutations" + '\n' + count;
    }

    void UpdateInfectionCount(float count)
    {
        infectionText.text = "Infection" + '\n' + "x" + count;

    }

    // Update is called once per frame
    void Update()
    {

    }
}