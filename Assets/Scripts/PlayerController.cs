using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{




    public float speed;
    public float sprintSpeed;
    public float maxStamina;
    public float maxHealth;
    public float armor;

    float baseMaxHealth;
    float baseMaxStamina;
    float baseSprintSpeed;
    float baseSpeed;
    float baseMaxGrenades;
    float baseArmor;
    public GameObject grenadePrefab;
    public GameObject selectedWeapon;
    public GameObject[] weaponsList;
    public GameObject grenadeSliderGO;
    public Slider grenadeSlider;
    public float throwPower;
    public float maxGrenades;
    float grenades;
    float currentStamina;
    float health;
	public ParticleSystem particles;
	public GameObject bloodStain;
    public GameObject audioHandler;
    bool cookingGrenade;
    public GameObject currentWeapon;
    public Text powerUpGainedText;
    float timeLeftBuffTimer;
    float[] timeLeftBuffTimers;
    bool buffActive;
    int currentBuff;
    bool[] currentBuffs;
    bool invulnurable;
    bool stimpackActive;
    bool flashOn;
    float tempSpeed;
    float tempSprintSpeed;
    public float[] upgradeLevels;
    public GameObject flashLight;

    GameCore core;


    void Start()
    {
        core = FindObjectOfType<GameCore>();
        flashOn = false;
        throwPower = 0;
        currentBuff = 0;
        buffActive = false;
        currentBuffs = new bool[6];
        timeLeftBuffTimers = new float[6];

        weaponsList[0].SetActive(true);
        cookingGrenade = false;
        grenadeSliderGO.SetActive(false);
        grenadeSlider.maxValue = 80;

        setUpPlayerStats();
        health = maxHealth;
        tempSpeed = speed;
        grenades = maxGrenades;
        tempSprintSpeed = sprintSpeed;

    }

    void Awake()
    {
        baseMaxHealth = maxHealth;
        baseMaxStamina = maxStamina;
        baseSprintSpeed = sprintSpeed;
        baseSpeed = speed;
        baseMaxGrenades = maxGrenades;
        baseArmor = armor;
    }


    void Update()
    {


        if (cookingGrenade)
        {
            grenadeSliderGO.SetActive(true);
            grenadeSlider.value = throwPower;
        }
        else
        {
            grenadeSliderGO.SetActive(false);
        }


        if (Input.GetKey(KeyCode.G) && !selectedWeapon.GetComponent<AimScript>().getReloading() && grenades > 0)
        {
            throwPower += 1;
            cookingGrenade = true;
        }

        if (Input.GetKeyUp(KeyCode.G) && !selectedWeapon.GetComponent<AimScript>().getReloading() && grenades > 0)
        {
            ThrowGrenade(throwPower);
            grenades -= 1;
            throwPower = 0;
            cookingGrenade = false;
        }

    }

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

    public void setUpPlayerStats()
    {
        float[] list = core.getUpgradeLevels();

        for(int i = 0; i < list.Length; i++)
        {
            if(i == 6)
            {
                if(list[i] == 0)
                {
                    
                }else if(list[i] == 1)
                {

                }
                else if (list[i] == 2)
                {

                }
                else if (list[i] == 3)
                {

                }
                else if (list[i] == 4)
                {

                }
                else if (list[i] == 5)
                {

                }
            }
            if (i == 7)
            {
                if (list[i] == 0)
                {
                    speed = baseSpeed;
                }
                else if (list[i] == 1)
                {
                    speed = baseSpeed * 1.1f;
                }
                else if (list[i] == 2)
                {
                    speed = baseSpeed * 1.2f;
                }
                else if (list[i] == 3)
                {
                    speed = baseSpeed * 1.3f;
                }
                else if (list[i] == 4)
                {
                    speed = baseSpeed * 1.4f;
                }
                else if (list[i] == 5)
                {
                    speed = baseSpeed * 1.5f;
                }
            }
            if (i == 8)
            {
                if (list[i] == 0)
                {
                    maxGrenades = baseMaxGrenades;
                }
                else if (list[i] == 1)
                {
                    maxGrenades = baseMaxGrenades+2;
                }
                else if (list[i] == 2)
                {
                    maxGrenades = baseMaxGrenades + 4;
                }
                else if (list[i] == 3)
                {
                    maxGrenades = baseMaxGrenades + 6;
                }
                else if (list[i] == 4)
                {
                    maxGrenades = baseMaxGrenades + 8;
                }
                else if (list[i] == 5)
                {
                    maxGrenades = baseMaxGrenades + 10;
                }
            }
            if (i == 9)
            {
                if (list[i] == 0)
                {
                    armor = baseArmor;
                }
                else if (list[i] == 1)
                {
                    armor = baseArmor * 1.2f;
                }
                else if (list[i] == 2)
                {
                    armor = baseArmor * 1.4f;
                }
                else if (list[i] == 3)
                {
                    armor = baseArmor * 1.6f;
                }
                else if (list[i] == 4)
                {
                    armor = baseArmor * 2f;

                }
                else if (list[i] == 5)
                {
                    armor = baseArmor * 3f;
                }
            }
            if (i == 10)
            {
                if (list[i] == 0)
                {
                    maxStamina = baseMaxStamina;
                }
                else if (list[i] == 1)
                {
                    maxStamina = baseMaxStamina * 1.2f;
                }
                else if (list[i] == 2)
                {
                    maxStamina = baseMaxStamina * 1.4f;
                }
                else if (list[i] == 3)
                {
                    maxStamina = baseMaxStamina * 1.6f;
                }
                else if (list[i] == 4)
                {
                    maxStamina = baseMaxStamina * 1.8f;
                }
                else if (list[i] == 5)
                {
                    maxStamina = baseMaxStamina * 2f;
                }
            }
            if (i == 11)
            {
                if (list[i] == 0)
                {
                    maxHealth = baseMaxHealth;
                }
                else if (list[i] == 1)
                {
                    maxHealth = baseMaxHealth * 1.2f;
                }
                else if (list[i] == 2)
                {
                    maxHealth = baseMaxHealth * 1.4f;
                }
                else if (list[i] == 3)
                {
                    maxHealth = baseMaxHealth * 1.8f;
                }
                else if (list[i] == 4)
                {
                    maxHealth = baseMaxHealth * 2f;
                }
                else if (list[i] == 5)
                {
                    maxHealth = baseMaxHealth * 2.4f;
                }
            }

        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        //Basic Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        if (Input.GetKeyDown(KeyCode.F) && !flashOn){
            flashOn = true;
            flashLight.SetActive(true);
        }else if(Input.GetKeyDown(KeyCode.F) && flashOn)
        {
            flashOn = false;
            flashLight.SetActive(false);
        }

        //Weapon switching
        if (Input.GetKey(KeyCode.Alpha1) && !selectedWeapon.GetComponent<AimScript>().getReloading())
        {
            this.selectedWeapon = weaponsList[0];
            foreach(GameObject weapon in weaponsList)
            {
                if(weapon != weaponsList[0])
                {
                    weapon.SetActive(false);
                }
                else
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                    
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha2) && !selectedWeapon.GetComponent<AimScript>().getReloading())
        {
            this.selectedWeapon = weaponsList[1];
            foreach (GameObject weapon in weaponsList)
            {
                if (weapon != weaponsList[1])
                {
                    weapon.SetActive(false);
                }
                else
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                }
            }


        }
        if (Input.GetKey(KeyCode.Alpha3) && !selectedWeapon.GetComponent<AimScript>().getReloading())
        {
            this.selectedWeapon = weaponsList[2];
            foreach (GameObject weapon in weaponsList)
            {
                if (weapon != weaponsList[2])
                {
                    weapon.SetActive(false);
                }
                else
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.Alpha4) && !selectedWeapon.GetComponent<AimScript>().getReloading())
        {
            this.selectedWeapon = weaponsList[3];
            foreach (GameObject weapon in weaponsList)
            {
                if (weapon != weaponsList[3])
                {
                    weapon.SetActive(false);
                }
                else
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                }
            }
        }
		if (Input.GetKey(KeyCode.Alpha5) && !selectedWeapon.GetComponent<AimScript>().getReloading())
		{
			this.selectedWeapon = weaponsList[4];
			foreach (GameObject weapon in weaponsList)
			{
				if (weapon != weaponsList[4])
				{
					weapon.SetActive(false);
				}
				else
				{
					weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                }
			}
        }
        if (Input.GetKey(KeyCode.Alpha6) && !selectedWeapon.GetComponent<AimScript>().getReloading())
        {
            this.selectedWeapon = weaponsList[5];
            foreach (GameObject weapon in weaponsList)
            {
                if (weapon != weaponsList[5])
                {
                    weapon.SetActive(false);
                }
                else
                {
                    weapon.SetActive(true);
                    weapon.GetComponent<AimScript>().refreshWeapon(core.getUpgradeLevels());
                    if (buffActive)
                    {
                        refreshActiveBuffs();
                    }
                }
            }
        }





        //Sprinting
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            
                currentStamina -= 1;
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(x * sprintSpeed, y * sprintSpeed, 0);
            
        }
        else
        {
            if (currentStamina < maxStamina)
            {
                currentStamina += 1;
                
            }
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(x * speed, y * speed, 0);

        }



    }
    //Keep buffs active while switching weapons
    void refreshActiveBuffs()
    {
        for(int i = 0; i < 3; i++)
        {
            if (currentBuffs[i])
            {
                gainBuff(i, timeLeftBuffTimers[i]);
            }
        }
    }

    IEnumerator grenadeSound(float wait)
    {
        yield return new WaitForSeconds(wait);
        audioHandler.GetComponent<AudioHandler>().playExplosionSound(0);

    }

    private void ThrowGrenade(float tP)
    {
        StartCoroutine(grenadeSound(1.5f));
        
        if (tP > 80)
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation) as GameObject;
            grenade.GetComponent<GrenadeScript>().speed = 80;
        }
        else
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation) as GameObject;
            grenade.GetComponent<GrenadeScript>().speed = tP;
        }

    }

    public void takeDamage(float dam)
	{
        if (!invulnurable)
        {
            if(this.health - dam < 0)
            {
                core.loadLossScreen();
            }
            this.health -= dam;
            Instantiate(particles, transform.position, Quaternion.identity);
            GameObject blood = Instantiate(bloodStain, transform.position, transform.rotation) as GameObject;
        }

	}

    public IEnumerator powerupText(float w, int n)
    {
        powerUpGainedText.gameObject.SetActive(true);
        if(n == 0){
            powerUpGainedText.text = "Health Pack";
        }
        else if(n == 1)
        {
            powerUpGainedText.text = "Ammo Pack";
        }
        else if(n == 2)
        {
            powerUpGainedText.text = "Grenades";
        }
        else if (n == 3)
        {
            powerUpGainedText.text = "Explosive Ammo";
        }
        else if (n == 4)
        {
            powerUpGainedText.text = "Freezing Ammo";
        }
        else if (n == 5)
        {
            powerUpGainedText.text = "Invulnerability";
        }
        else if (n == 6)
        {
            powerUpGainedText.text = "4x Damage";
        }
        else if (n == 7)
        {
            powerUpGainedText.text = "Stimpack";
        }
        else if (n == 8)
        {
            powerUpGainedText.text = "Nuke";
        }
        else if (n == 9)
        {
            powerUpGainedText.text = "Slow time";
        }

        yield return new WaitForSeconds(w);
        powerUpGainedText.gameObject.SetActive(false);
    }

    public void gainHealthPack()
    {
        audioHandler.GetComponent<AudioHandler>().playPickUpSound();
        health = maxHealth;
        StartCoroutine(powerupText(1, 0));

    }

    public void gainGrenadePack()
    {
        audioHandler.GetComponent<AudioHandler>().playPickUpSound();
        grenades = maxGrenades;
        StartCoroutine(powerupText(1, 2));
    }


    public void refillAmmo()
    {
        audioHandler.GetComponent<AudioHandler>().playPickUpSound();
        currentWeapon.GetComponent<AimScript>().gainAmmoPack();
        StartCoroutine(powerupText(1, 1));
    }
    public void setCurrentWeapon(GameObject curWep)
    {
        currentWeapon = curWep;
    }

    
    //Used to refresh duration of buff if weapon is switched
    public void gainBuff(int x, float timer)
    {
        audioHandler.GetComponent<AudioHandler>().playPickUpSound();
        if (x == 0)
        {
            if(currentBuffs[0] == false)
            {
                timeLeftBuffTimers[0] = timer;
                StartCoroutine(powerupText(1, 6));
            }

            StartCoroutine(qDBuffTimer(timer, x));
        }
        else if (x == 1)
        {
            if (currentBuffs[1] == false)
            {
                timeLeftBuffTimers[1] = timer;
                StartCoroutine(powerupText(1, 3));
            }

            StartCoroutine(qDBuffTimer(timer, x));
        }
        else if (x == 2)
        {
            if (currentBuffs[2] == false)
            {
                timeLeftBuffTimers[2] = timer;
                StartCoroutine(powerupText(1, 4));
            }

            StartCoroutine(qDBuffTimer(timer, x));
        }
        else if (x == 3)
        {
            if (currentBuffs[3] == false)
            {
                timeLeftBuffTimers[3] = timer;
                StartCoroutine(powerupText(1, 5));
            }

            StartCoroutine(qDBuffTimer(timer, x));
        }
        else if (x == 4)
        {
            if (currentBuffs[4] == false)
            {
                timeLeftBuffTimers[4] = timer;
                StartCoroutine(powerupText(1, 7));
            }

            StartCoroutine(qDBuffTimer(timer, x));
        }
        else if (x == 5)
        {
            if (currentBuffs[5] == false)
            {
                timeLeftBuffTimers[5] = timer;
            }
            StartCoroutine(powerupText(1, 9));
            //TODO SLOW TIME POWERUP
        }

    }

    //Set Buffs active with timers
    IEnumerator qDBuffTimer(float wait, int x)
    {
        buffActive = true;
        if(x == 0)
        {
            currentBuffs[0] = true;
            currentWeapon.GetComponent<AimScript>().setQuadDamage(true);
            yield return new WaitForSeconds(wait);
            currentBuffs[0] = false;
            buffActive = false;
            currentWeapon.GetComponent<AimScript>().setQuadDamage(false);
        }else if (x == 1)
        {
            currentBuffs[1] = true;
            currentWeapon.GetComponent<AimScript>().setHasExplosiveAmmo(true);
            yield return new WaitForSeconds(wait);
            currentBuffs[1] = false;
            buffActive = false;
            currentWeapon.GetComponent<AimScript>().setHasExplosiveAmmo(false);
        }else if(x == 2)
        {
            currentBuffs[2] = true;
            currentWeapon.GetComponent<AimScript>().setHasFreezingAmmo(true);
            yield return new WaitForSeconds(wait);
            currentBuffs[2] = false;
            buffActive = false;
            currentWeapon.GetComponent<AimScript>().setHasFreezingAmmo(false);
        }else if(x == 3)
        {
            invulnurable = true;
            currentBuffs[3] = true;
            yield return new WaitForSeconds(wait);
            invulnurable = false;
            currentBuffs[3] = false;
        }
        else if (x == 4)
        {

            speed = speed * 2;
            sprintSpeed = sprintSpeed * 2;
            currentBuffs[4] = true;
            yield return new WaitForSeconds(wait);
            speed = tempSpeed;
            currentBuffs[4] = false;
            sprintSpeed = tempSprintSpeed;
        }
        else if (x == 5)
        {
            currentBuffs[5] = true;
            yield return new WaitForSeconds(wait);
            currentBuffs[5] = false;
        }

    }




    // Nuke powerup activation
    public void nukePowerUp()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 15f);
        audioHandler.GetComponent<AudioHandler>().playPickUpSound();
        StartCoroutine(powerupText(1, 8));
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
            {
                hit.gameObject.GetComponent<EnemyScript>().TakeDamage(2000f);
                hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(20f, explosionPos, 12.5f, 3.0F);
            }

        }
    }

    public GameObject getCurrentWeapon()
    {
        return currentWeapon;
    }

    public bool[] getCurrentBuffs()
    {
        return currentBuffs;
    }
    public float[] getTimeLeftBuffTimers()
    {
        return timeLeftBuffTimers;
    }

    public float getCurrentHealth()
    {
        return health;
    }

    public float getCurrentStamina()
    {
        return currentStamina;
    }
    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getMaxStamina()
    {
        return maxStamina;
    }
    public float getCurrentGrenades()
    {
        return grenades;
    }


}