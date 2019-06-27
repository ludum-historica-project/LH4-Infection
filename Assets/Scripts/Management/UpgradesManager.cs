using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UpgradesManager : Manager
{

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public ScriptableEvent OnPlayerHealthUpdated;
    public FloatReference maxPlayerHP;

    public ScriptableEvent onXPGain;
    public FloatReference xpToNextSkillPoint;

    public ScriptableEvent OnLevelGain;
    public int upgradePoints = 0;

    public ScriptableEvent onInfectionCounterUpdate;
    public ScriptableEvent onInfectionMultUpdate;
    public FloatReference InfectionDamageThreshold;


    public List<Upgrade> upgrades = new List<Upgrade>();


    private int xp = 0;
    private int currentInfectionProgress;
    private int infectionMultiplier = 1;

    int prevHealth = 0;


#if UNITY_EDITOR
    [MenuItem("Commands/Clear Progress")]
    public static void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }

#endif





    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        LoadUpgrades();
        prevHealth = (int)maxPlayerHP.Value;
        OnPlayerHealthUpdated.OnInvoke += PlayerHealthUpdated;
    }

    void PlayerHealthUpdated(float number)
    {
        int currentHealth = (int)number;
        if (currentHealth < prevHealth)
        {

            currentInfectionProgress += prevHealth - currentHealth;
            while (currentInfectionProgress >= InfectionDamageThreshold.Value)
            {
                currentInfectionProgress -= (int)InfectionDamageThreshold.Value;
                infectionMultiplier++;
                onInfectionMultUpdate.Invoke(infectionMultiplier);
            }
            onInfectionCounterUpdate.Invoke(currentInfectionProgress);
        }
        prevHealth = currentHealth;
    }

    public void ResetInfectionMultiplier()
    {
        infectionMultiplier = 1;
        currentInfectionProgress = 0;
        onInfectionMultUpdate.Invoke(infectionMultiplier);
        onInfectionCounterUpdate.Invoke(currentInfectionProgress);
    }

    public void PurchaseUpgrade(Upgrade upgrade)
    {
        Debug.Log("Buying " + upgrade.name);
        upgradePoints -= upgrade.Cost;
        SaveUpgrades();
        upgrade.level++;
    }
    public void AddXP(int value)
    {
        xp += value * infectionMultiplier;
        while (xp >= xpToNextSkillPoint.Value)
        {
            xp -= (int)xpToNextSkillPoint.Value;
            upgradePoints++;
            OnLevelGain.Invoke(upgradePoints);
        }
        onXPGain.Invoke(xp);
    }

    void LoadUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.level = PlayerPrefs.GetInt("UPGRADE_" + upgrade.name, 0);
        }
        upgradePoints = PlayerPrefs.GetInt("UPGRADEPOINTS", 0);
    }

    void SaveUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            PlayerPrefs.SetInt("UPGRADE_" + upgrade.name, upgrade.level);
        }
        PlayerPrefs.SetInt("UPGRADEPOINTS", upgradePoints);

    }

    public void ResetUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.level = 0;
        }
        upgradePoints = 0;
        SaveUpgrades();
    }

    private void OnDestroy()
    {
        SaveUpgrades();
    }
}
