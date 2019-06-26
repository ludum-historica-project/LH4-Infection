using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int InfectionMultiplier = 1;

    int prevHealth = 0;





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
                InfectionMultiplier++;
                onInfectionMultUpdate.Invoke(InfectionMultiplier);
            }
            onInfectionCounterUpdate.Invoke(currentInfectionProgress);
        }
        prevHealth = currentHealth;
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
        xp += value * InfectionMultiplier;
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

    private void OnDestroy()
    {
        SaveUpgrades();
    }
}
