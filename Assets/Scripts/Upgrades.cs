using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    public player Player;
    public HealthBar healthBar;
    public Health health;
    public HealthBar OxygenBar;
    public Oxygen oxygen;
    public HealthBar FuelBar;
    public Fuel fuel;
    public Drill drill;
    public Trigger trigger;
    public PlayerMoney playerMoney;
        public InventoryManager inventoryManager;
    public Transform drillrange;
    public MeteorExplode meteorexplode;
    public Teleport teleport;
    public GameObject forcefield;
    public int Dashlvl, MaxHealthlvl, Oxygenlvl, HPregenlvl, Speedlvl, Drillvl, Fuellvl, FuelRegenlvl, StackSizelvl, MiningRangelvl, Fortunelvl, BaseTPlvl, ForceShieldlvl;
    public int Success;
    public TextMeshProUGUI DashUpgrade, HealthUpgrade, OxygenUpgrade, HPregenUpgrade, SpeedUpgrade, DrillUpgrade, FuelUpgrade, StackSizeUpgrade, MiningRangeUpgrade, FortuneUpgrade, BaseTPUpgrade, ForcceShieldUpgrade;

    [Header("------------------Base Upgrades------------------")]
    public TextMeshProUGUI FuelRegenUpgrade;
    public void UpgradeDashSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Dashlvl + 1),2,Dashlvl);
        if (Success == 1)
        {
            Player.multiplier = Player.multiplier + 0.5f;
            Dashlvl = Dashlvl + 1;
            DashUpgrade.text = (100 * (Dashlvl + 1)).ToString();
        }

    }
    public void UpgradeHealth()
    {
        Success = playerMoney.SpendMoney(100 * (MaxHealthlvl + 1), 2, MaxHealthlvl);
        if (Success == 1)
        {
            healthBar.SetMaxHealth(health.maxhealth + 50);
            healthBar.SetHealth(health.currenthealth + 50);
            health.maxhealth = health.maxhealth + 50;
            health.currenthealth = health.currenthealth + 50;
            MaxHealthlvl = MaxHealthlvl + 1;
            HealthUpgrade.text = (100 * (MaxHealthlvl + 1)).ToString();
        }
    }
    public void UpgradeOxygen()
    {
        Success = playerMoney.SpendMoney(100 * (Oxygenlvl + 1), 2, Oxygenlvl);
        if (Success == 1)
        {
            OxygenBar.SetMaxHealth(oxygen.maxhealth + 50);
            OxygenBar.SetHealth(oxygen.currenthealth + 50);
            oxygen.maxhealth = oxygen.maxhealth + 50;
            oxygen.currenthealth = oxygen.currenthealth + 50;

            trigger.oxlast = trigger.oxlast + 0.2f;
            Oxygenlvl = Oxygenlvl + 1;
            OxygenUpgrade.text = (100 * (Oxygenlvl + 1)).ToString();
        }
    }
    public void UpgradeHPregen()
    {
        Success = playerMoney.SpendMoney(100 * (HPregenlvl + 1), 2, HPregenlvl);
        if (Success == 1)
        {
            trigger.HPregen = trigger.HPregen + 0.2f;
            HPregenlvl = HPregenlvl + 1;
            HPregenUpgrade.text = (100 * (HPregenlvl + 1)).ToString();
        }
    }

    public void UpgradeSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Speedlvl + 1), 2, Speedlvl);
        if (Success == 1)
        {
            Player.speedbonus = Player.speedbonus + 0.5f;
            Speedlvl = Speedlvl + 1;
            SpeedUpgrade.text = (100 * (Speedlvl + 1)).ToString();
        }
    }

    public void UpgradeFuel()
    {
        Success = playerMoney.SpendMoney(100 * (Fuellvl + 1), 3, Fuellvl);
        if (Success == 1)
        {
            FuelBar.SetMaxHealth(oxygen.maxhealth + 50);
            FuelBar.SetHealth(oxygen.currenthealth + 50);
            fuel.maxhealth = fuel.maxhealth + 50;
            fuel.currenthealth = fuel.currenthealth + 50;
            Fuellvl = Fuellvl + 1;
            FuelUpgrade.text = (100 * (Fuellvl + 1)).ToString();

        }

    }
    public void UpgradeDrillPower()
    {
        Success = playerMoney.SpendMoney(100 * (Drillvl + 1), 2, Drillvl);
        if (Success == 1)
        {
            drill.damagemod = drill.damagemod*2;
            Drillvl = Drillvl + 1;
            DrillUpgrade.text = (100 * (Drillvl + 1)).ToString();

        }


    }

    public void UpgradeFuelRegen()
    {
        Success = playerMoney.SpendMoney(100 * (FuelRegenlvl + 1), 2, FuelRegenlvl);
        if (Success == 1)
        {
            Player.fuelmod = Player.fuelmod + 0.2f;
            FuelRegenlvl += 1;
            FuelRegenUpgrade.text = (100 * (FuelRegenlvl + 1)).ToString();
        }
    }

    public void Upgradestacksize()
    {
        Success = playerMoney.SpendMoney(100 * (StackSizelvl + 1), 4, FuelRegenlvl);
        if (Success == 1)
        {
            StackSizelvl = StackSizelvl + 1;
            inventoryManager.maxStack = inventoryManager.maxStack + 1;
            FuelUpgrade.text = (100 * (Fuellvl + 1)).ToString();
        }

    }
    public void UpgradeMiningRange()
    {
        Success = playerMoney.SpendMoney(200 * (MiningRangelvl + 1), 2, MiningRangelvl);
        if (Success == 1)
        {
            float x = 0;
            x = x + 1;
            MiningRangelvl = MiningRangelvl + 1;
            drillrange.localScale = new Vector3(1 + x, 1 + x, 1 + x);
            MiningRangeUpgrade.text = (200 * (MiningRangelvl + 1)).ToString();
        }
    }
    public void UpgradeFortune()
    {
        Success = playerMoney.SpendMoney(200 * (Fortunelvl + 1), 3, Fortunelvl);
        if (Success == 1)
        {
            Fortunelvl = Fortunelvl + 1;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Meteor");
            foreach (GameObject go in gos)
                meteorexplode.rarity = meteorexplode.rarity + 10;


            FortuneUpgrade.text = (200 * (Fortunelvl + 1)).ToString();
        }
    }
    public void UpgradeTP()
    {
        Success = playerMoney.SpendMoney(2000 * (BaseTPlvl + 1), 1, BaseTPlvl);
        if (Success == 1)
        {
            BaseTPlvl = BaseTPlvl + 1;
            teleport.CanTeleport = true;
            BaseTPUpgrade.text = ("Sold");
        }
    }
    public void UpgradeForceField()
    {
        Success = playerMoney.SpendMoney(2000 * (ForceShieldlvl + 1), 1, ForceShieldlvl);
        if (Success == 1)
        {
            health.shieldmod = health.shieldmod * (8 / 10);
            forcefield.SetActive(true);
            ForceShieldlvl = ForceShieldlvl + 1;
            BaseTPUpgrade.text = ("Sold");
        }
    }
}
