using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelUpMenuScript : MonoBehaviour {


    public GameObject menuCamera;
    public Text currentMoneyText;

    float[] prices = { 5000, 7500, 10000, 15000, 30000 };

    public GameCore core;

    public Slider[] upgradeSliders;
    public Text[] upgradeCostTexts;
    public float[] upgradeLevel;
    float currentMoney;
	// Use this for initialization
    /*
     * 0.pistol
     * 1.rifle
     * 2.sniper
     * 3.shotgun
     * 4.lmg
     * 5.rpg
     * 6.reload
     * 7.move
     * 8.grenade
     * 9.armor
     * 10.stamina
     * 11.health
    */

	void Start () {
        core = FindObjectOfType<GameCore>();
        for(int i = 0; i < upgradeLevel.Length; i++)
        {
            upgradeLevel[i] = 0;
            upgradeCostTexts[i].text = "Cost: " + prices[0];
        }
        currentMoney = core.getPlayerCredits();
        updateMoneyCounter();
        upgradeLevel = core.getUpgradeLevels();
        core = FindObjectOfType<GameCore>();
	
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < upgradeLevel.Length; i++)
        {
            upgradeSliders[i].value = upgradeLevel[i];
        }


        menuCamera.transform.Rotate(new Vector3(0.02f, 0.02f, 0));
	
	}

    void updateMoneyCounter()
    {
        currentMoneyText.text = "Money: " + currentMoney;

    }

    public void gainUpgrade(int value)
    {
        if (upgradeLevel[value] + 1 <= 5)
            if (upgradeLevel[value] == 0 && currentMoney > prices[0])
            {
                upgradeLevel[value] += 1;
                currentMoney -= prices[0];
                updateMoneyCounter();
                upgradeCostTexts[value].text = "Cost: " + prices[1];


            }
            else if (upgradeLevel[value] == 1 && currentMoney > prices[1])
            {
                upgradeLevel[value] += 1;
                currentMoney -= prices[1];
                updateMoneyCounter();
                upgradeCostTexts[value].text = "Cost: " + prices[2];
            }
            else if (upgradeLevel[value] == 2 && currentMoney > prices[2])
            {
                upgradeLevel[value] += 1;
                currentMoney -= prices[2];
                updateMoneyCounter();
                upgradeCostTexts[value].text = "Cost: " + prices[3];
            }
            else if (upgradeLevel[value] == 3 && currentMoney > prices[3])
            {
                upgradeLevel[value] += 1;
                currentMoney -= prices[3];
                updateMoneyCounter();
                upgradeCostTexts[value].text = "Cost: " + prices[4];
            }
            else if (upgradeLevel[value] == 4 && currentMoney > prices[4])
            {

                upgradeLevel[value] += 1;
                currentMoney -= prices[4];
                updateMoneyCounter();
                upgradeCostTexts[value].text = "Fully upgraded";

            }
            else
            {

            }


    }

    public float[] getUpgradeLevels()
    {
        return upgradeLevel;
    }

    public void goBack()
    {
        Application.LoadLevel("LevelSelect");
    }

    public void applyAndContinue()
    {
        core.recieveUpgradeLevels(upgradeLevel);
        //Deliver upgradeLevel array to gameManager--
        Application.LoadLevel("LevelSelect");

    }

    public void showInfo()
    {
        //TODO
    }
}
