using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

    private Rigidbody ammoRb;
	public Sprite[] sprite;
    private GameObject weapon;
    private float bulletDamage;
    public float speed;
    public bool sniperBullet;
    public bool isRocket;
    bool explosiveAmmo;
    bool freezingAmmo;
    public float EERadius;
    public float EEDamage;
    public float EEPower;
    public float rocketRadius;
    public float rocketExplosionDamage;
    public float rocketExplosionPower;
    public float FreezeTime;
    public GameObject explosionSprite;
    public float bulletLifeTime;
    public float bulletSize;
    public GameObject audioHandler;

    // Use this for initialization
    void Start()
    {
        
        ammoRb = GetComponent<Rigidbody>();
        ammoRb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
        GetComponent<SphereCollider>().radius = bulletSize;
        ammoRb.AddForce(transform.forward * 30, ForceMode.Impulse);
        StartCoroutine(explosionWO(bulletLifeTime));


        

    }

	public void createBullet(float bdam, float bspd, int snum, bool sB, bool eA, bool fA, bool iR, float rD, float rR, float bLT, float bS)
    {
        bulletDamage = bdam;
		speed = bspd;
        sniperBullet = sB;
        isRocket = iR;
        rocketExplosionDamage = rD;
        explosiveAmmo = eA;
        freezingAmmo = fA;
        rocketRadius = rR;
        bulletLifeTime = bLT;
        bulletSize = bS;
		GetComponent<SpriteRenderer> ().sprite = sprite [snum];
		Start ();
    }
    IEnumerator explosionWO(float ft)
    {
        yield return new WaitForSeconds(ft);
        if (isRocket)
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, rocketRadius);
            StartCoroutine(ExpEffect(2f));

            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
                {
                    hit.gameObject.GetComponent<EnemyScript>().TakeDamage(rocketExplosionDamage);
                    hit.gameObject.GetComponent<EnemyScript>().explosionAmmoEffect();
                    hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(rocketExplosionPower, explosionPos, rocketRadius, 3.0F);
                }

            }
        }
        Destroy(this.gameObject);
    }


    IEnumerator ExpEffect(float ft)
    {
        GameObject explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity) as GameObject;
        audioHandler.GetComponent<AudioHandler>().playExplosionSound(1);
        Destroy(explosion, 1f);
        yield return new WaitForSeconds(ft);

    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
			collision.gameObject.GetComponent<EnemyScript> ().TakeDamage (bulletDamage);
            if (explosiveAmmo)
            {
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, EERadius);

                foreach (Collider hit in colliders)
                {
                    if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
                    {
                        hit.gameObject.GetComponent<EnemyScript>().TakeDamage(EEDamage);
                        hit.gameObject.GetComponent<EnemyScript>().explosionAmmoEffect();
                        hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(EEPower, explosionPos, EERadius, 3.0F);
                    }

                }
            }
            if (isRocket)
            {
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, rocketRadius);
                StartCoroutine(ExpEffect(2f));

                foreach (Collider hit in colliders)
                {
                    if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
                    {
                        hit.gameObject.GetComponent<EnemyScript>().TakeDamage(rocketExplosionDamage);
                        hit.gameObject.GetComponent<EnemyScript>().explosionAmmoEffect();
                        hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(rocketExplosionPower, explosionPos, rocketRadius, 3.0F);
                    }

                }
            }
        
            if (freezingAmmo)
            {
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, EERadius);
                foreach (Collider hit in colliders)
                {
                    if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
                    {
                        hit.gameObject.GetComponent<EnemyScript>().freeze(FreezeTime);
                    }

                }
            }
            if (!sniperBullet)
            {
                Destroy(this.gameObject);
            }

        }

		if (collision.gameObject.tag == "Wall")
		{
			Destroy (this.gameObject);
		}

        if(collision.gameObject.tag == "GameManager")
        {
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Bullet")
        {
        }

        if(collision.gameObject.tag == "Vehicle")
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}
