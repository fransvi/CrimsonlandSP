using UnityEngine;
using System.Collections;

public class PowerupScript : MonoBehaviour {

    public int powerUpType;
    public GameObject[] objects;
    public string powerUpName;
    public GameObject player;
    public GameObject bullet;
    public GameObject ebullet;
    public float quadDamageTimer;
    public float explosiveAmmoTimer;
    public float freezeAmmoTimer;
    public float invulnTimer;
    public float stimpackTimer;
    public float slowtimeTimer;

    /*
     * 0.HealthPack
     * 1.AmmoPack
     * 2.GrenadePack
     * 3.ExplosiveAmmo
     * 4.FreezeAmmo
     * 5.Invulnurability
     * 6.QuadDamage
     * 7.SpeedUp
     * 8.Nuke
     * 9.SlowTime
    */

    // Use this for initialization
    void Start()
    {
        powerUpName = setupName(powerUpType);
        objects[powerUpType].SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(bullet.GetComponent<SphereCollider>(), GetComponent<BoxCollider>());
        Physics.IgnoreCollision(ebullet.GetComponent<SphereCollider>(), GetComponent<BoxCollider>());
    }
    void Awake()
    {

    }

    public void setPowerupType(int t)
    {
        powerUpType = t;
    }



    
	// Update is called once per frame
	void Update () {
	
	}

    string setupName(int type)
    {
        switch (type)
        {
            case 0:
                return "Health Pack";
            case 1:
                return "Ammo Pack";
            case 2:
                return "Grenade Pack";
            case 3:
                return "Explosive Ammo";
            case 4:
                return "Freezing Ammo";
            case 5:
                return "Invulnurability";
            case 6:
                return "Quad Damage";
            case 7:
                return "Stimpack";
            case 8:
                return "Nuke";
            case 9:
                return "Slow Time";
            default:
                return "Undefined";
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            switch (powerUpType)
            {
                case 0:
                    player.GetComponent<PlayerController>().gainHealthPack();
                    break;
                case 1:
                    player.GetComponent<PlayerController>().refillAmmo();
                    break;
                case 2:
                    player.GetComponent<PlayerController>().gainGrenadePack();
                    break;
                case 3:
                    player.GetComponent<PlayerController>().gainBuff(1,explosiveAmmoTimer);
                    break;
                case 4:
                    player.GetComponent<PlayerController>().gainBuff(2,freezeAmmoTimer);
                    break;
                case 5:
                    player.GetComponent<PlayerController>().gainBuff(3,invulnTimer);
                    break;
                case 6:
                    player.GetComponent<PlayerController>().gainBuff(0,quadDamageTimer);
                    break;
                case 7:
                    player.GetComponent<PlayerController>().gainBuff(4,stimpackTimer);
                    break;
                case 8:
                    player.GetComponent<PlayerController>().nukePowerUp();
                    break;
                case 9:
                    player.GetComponent<PlayerController>().gainBuff(5,slowtimeTimer);
                    break;
                default:
                    break;
            }

            Destroy(this.gameObject);

        }
    }
}
