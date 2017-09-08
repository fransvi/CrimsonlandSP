using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    public GameObject player;
    public GameObject weapon;
    public GameObject Gm;
    public Slider HpSlider;
    public Slider StSlider;
    public Text grenadeText;
    public Text ammoText;
    public Text healthText;
    public Text staminaText;
    public Text scoreText;
    public Text weaponNameText;
    public Text enemiesLeftText;

    GameCore core;
    GameManager gm;

    //Buffbar
    public GameObject[] buffIcons;
    float[] buffTimers;
    bool[] currentBuffs;

    // Use this for initialization
    void Start () {
        core = FindObjectOfType<GameCore>();
        gm = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        HpSlider.value = player.GetComponent<PlayerController>().getCurrentHealth();
        StSlider.value = player.GetComponent<PlayerController>().getCurrentStamina();
        HpSlider.maxValue = player.GetComponent<PlayerController>().getMaxHealth();
        StSlider.maxValue = player.GetComponent<PlayerController>().getMaxStamina();
        grenadeText.text = ""+ player.GetComponent<PlayerController>().getCurrentGrenades();
        ammoText.text = weapon.GetComponent<AimScript>().getCurrentAmmo() + " / " + weapon.GetComponent<AimScript>().getMaxAmmo();
        healthText.text = "HP: "+player.GetComponent<PlayerController>().getCurrentHealth();
        staminaText.text = "STA: " + player.GetComponent<PlayerController>().getCurrentStamina();
        scoreText.text = "" + core.getLastLevelScore();
        weaponNameText.text = weapon.GetComponent<AimScript>().getName();
        buffTimers = player.GetComponent<PlayerController>().getTimeLeftBuffTimers();
        currentBuffs = player.GetComponent<PlayerController>().getCurrentBuffs();
        enemiesLeftText.text = "Enemies left: " + core.getNextEnemiesLeft();

        for(int i = 0; i < buffIcons.Length; i++)
        {       
            if (currentBuffs[i])
            {
                buffIcons[i].gameObject.SetActive(true);
                buffTimers[i] -= Time.deltaTime;
                float timeLeftRound = Mathf.Round(buffTimers[i] * 100f) / 100f;
                buffIcons[i].gameObject.GetComponentInChildren<Text>().text = ""+timeLeftRound;
            }
            else
            {
                buffIcons[i].gameObject.SetActive(false);

            }
        }



    }
    public void setCurrentWeapon(GameObject wep)
    {
        this.weapon = wep;
    }
}
