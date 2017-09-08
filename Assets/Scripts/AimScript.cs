using UnityEngine;
using System.Collections;

public class AimScript : MonoBehaviour
{

    public int weaponID;
    public GameObject bullet;
    public GameObject reloadingText;
    public Transform shootPoint;
    public GameObject uiview;
    public float weaponDamage;
    public string weaponName;
    public float maxAmmo;
    public float reloadSpeed;
    public float fireRate;
    public GameObject crosshair;
    public float burstAmount;
    public float inaccuracy;
    public float totalAmmo;

    float baseTotalAmmo;
    float baseInaccuracy;
    float baseBurstAmount;
    float baseFireRate;
    float baseWeaponDamage;
    float baseBulletSpeed;

    public float bulletSpeed;
	public bool useRecoil;
	public int spriteNum;
    public bool isSniperBullet;
    public GameObject audioHandler;
    public int gunSound;
    bool allowFire;

    public GameObject mainCamera;
    public float shakeAmount;
    public GameObject player;

    public bool isRocket;
    public float rocketExplosionDamage;
    public float rocketExplosionRadius;
    public float bulletLifeTime;
    public float bulletSize;


    private float nextFire = 0.0F;
    float currentAmmo;
    float AmmoCap;
    bool reloading;
    float timeLeft;
    float recoilControl = 0;
    bool hasExplosiveAmmo;
    bool hasFreezeAmmo;
    float baseDamage;
    bool quadDamageOn;
    GameCore core;



    // Use this for initialization
    void Start()
    {


        core = FindObjectOfType<GameCore>();
        currentAmmo = maxAmmo;
        AmmoCap = totalAmmo;
        allowFire = true;
        reloadingText.SetActive(false);
        reloading = false;
        hasExplosiveAmmo = false;
        hasFreezeAmmo = false;
        refreshWeapon(core.getUpgradeLevels());
        baseDamage = weaponDamage;
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera.GetComponent<CameraScript>().setShakeAmount(shakeAmount);
        
    }

    void Awake()
    {
        baseBurstAmount = burstAmount;
        baseWeaponDamage = weaponDamage;
        baseFireRate = fireRate;
        baseInaccuracy = inaccuracy;
        baseTotalAmmo = totalAmmo;
        baseBulletSpeed = bulletSpeed;
    }

    void Update()
    {
        //Shooting
        if (Input.GetMouseButton(0) && Time.time > nextFire && currentAmmo > 0 && !reloading)
        {
			
            nextFire = Time.time + fireRate;
			//Burst of bullets shot

            for(int i = 0; i < burstAmount; i++)
            {

				//Inaccuracy and recoil
				var spreadX = Random.Range (-inaccuracy, inaccuracy);
				var spreadY = Random.Range (-inaccuracy, inaccuracy);
				var spreadZ = Random.Range (-inaccuracy, inaccuracy);
				if (recoilControl <= inaccuracy && useRecoil) {
					recoilControl += 0.5f;
					spreadX = Random.Range (-recoilControl, recoilControl);
					spreadY = Random.Range (-recoilControl, recoilControl);
					spreadZ = Random.Range (-recoilControl, recoilControl);
				} 
				float bulletAngle = Random.Range (-inaccuracy, inaccuracy);
				GameObject bullett = Instantiate (bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
                mainCamera.GetComponent<CameraScript>().shakeCamera();
                bullett.GetComponent<BulletScript>().createBullet(weaponDamage,bulletSpeed, spriteNum, isSniperBullet, hasExplosiveAmmo, hasFreezeAmmo, isRocket, rocketExplosionDamage, rocketExplosionRadius, bulletLifeTime, bulletSize);
				bullett.transform.Rotate (spreadX, spreadY, spreadZ);
                
            }

            audioHandler.GetComponent<AudioHandler>().playGunSound(gunSound);
            currentAmmo -= 1;


        }
		if(Input.GetMouseButtonUp(0))
		{
				recoilControl = 0;
		}


        //Reloading
        if (Input.GetKeyDown(KeyCode.R) || currentAmmo <= 0)
        {
			recoilControl = 0;
            if (totalAmmo > 0)
            {
                timeLeft = reloadSpeed;
                reloadingText.SetActive(true);
                if (!reloading)
                {
                    audioHandler.GetComponent<AudioHandler>().playReloadSound();
                    StartCoroutine(ReloadGun(reloadSpeed));
                }


            }
            else
            {
            }

        }

        if (reloading)
        {


            //Displaying reload timer on screen
            timeLeft -= Time.deltaTime;
            float timeLeftRound = Mathf.Round(timeLeft * 100f) / 100f;
            reloadingText.GetComponent<TextMesh>().text = "Reloading (" + (timeLeftRound) + "s)";
        }

        if (quadDamageOn)
        {
            weaponDamage = baseDamage * 4;
        }
        else
        {
            weaponDamage = baseDamage;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {



    }

    public bool getReloading()
    {
        return this.reloading;
    }


    IEnumerator ReloadGun(float wait)
    {
        reloading = true;
        yield return new WaitForSeconds(wait);

        float reloadableAmmo = maxAmmo;
        if(totalAmmo < maxAmmo)
        {
            reloadableAmmo = totalAmmo;
        }
        totalAmmo -= maxAmmo - currentAmmo;
        currentAmmo = reloadableAmmo;
        reloadingText.SetActive(false);
        reloading = false;
    }


    public void gainAmmoPack()
    {
        totalAmmo = AmmoCap;
    }

    public string getName()
    {
        return weaponName;
    }

    public float getDamage()
    {
        return weaponDamage;
    }

    public float getCurrentAmmo()
    {
        return currentAmmo;
    }
    public float getMaxAmmo()
    {
        return totalAmmo;
    }

    public void refreshWeapon(float[] upgradeList)
    {
        uiview.GetComponent<UiManager>().setCurrentWeapon(this.gameObject);
        player.GetComponent<PlayerController>().setCurrentWeapon(this.gameObject);

        for(int i = 0; i < upgradeList.Length; i++)
        {
            if(weaponID == i)
            {

                if(upgradeList[i] == 0)
                {

                    weaponDamage = baseWeaponDamage;
                    fireRate = baseFireRate;

                    

                }else if(upgradeList[i] == 1)
                {
                    if (inaccuracy * 0.9f > 0 && weaponID != 3)
                    {
                        inaccuracy = baseInaccuracy * 0.9f;
                    }
                    weaponDamage = baseWeaponDamage * 1.2f;
                    fireRate = baseFireRate * 0.80f;

                }
                else if (upgradeList[i] == 2)
                {
                    if (inaccuracy * 0.8f > 0 && weaponID != 3)
                    {
                        inaccuracy = baseInaccuracy * 0.8f;
                    }
                    weaponDamage = baseWeaponDamage * 1.3f;
                    fireRate = baseFireRate * 0.64f;

                }
                else if (upgradeList[i] == 3)
                {
                    if(weaponID == 5)
                    {
                        burstAmount = 2;
                    }
                    if (weaponID == 3)
                    {
                        burstAmount = 12;
                    }
                    if (inaccuracy * 0.7f > 0 && weaponID != 3)
                    {
                        inaccuracy = baseInaccuracy * 0.7f;
                    }
                    weaponDamage = baseWeaponDamage * 1.5f;
                    fireRate = baseFireRate * 0.51f;
                }
                else if (upgradeList[i] == 4)
                {
                    if (inaccuracy * 0.6f > 0 && weaponID != 3)
                    {
                        inaccuracy = baseInaccuracy * 0.6f;
                    }
                    weaponDamage = baseWeaponDamage * 1.7f;
                    fireRate = baseFireRate * 0.40f;
                }
                else if (upgradeList[i] == 5)
                {
                    if (inaccuracy * 0.5f > 0 && weaponID != 3)
                    {
                        inaccuracy = baseInaccuracy * 0.5f;
                    }
                    if (weaponID == 5)
                    {
                        burstAmount = 3;
                    }
                    if (weaponID == 3)
                    {
                        burstAmount = 20;
                    }
                    if(weaponID == 1)
                    {
                        burstAmount = 2;
                    }
                    weaponDamage = baseWeaponDamage * 2.2f;
                    fireRate = baseFireRate * 0.32f;
                }
            }
        }
    }

    public void setQuadDamage(bool b)
    {
        quadDamageOn = b;
    }

    public void setHasExplosiveAmmo(bool b)
    {
        hasExplosiveAmmo = b;
    }
    public void setHasFreezingAmmo(bool b)
    {
        hasFreezeAmmo = b;
    }

    void OnGUI()
    {

    }
}