using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {


    public GameObject sprite;
    public float bulletDamage;
	public float meleeDamage;
    public float maxHealth;
    public float range;
    public float range2;
    public float stopRange;
    public float moveSpeed;
    public float rotationSpeed;
	public float enemyScoreWorth;
    public GameObject player;
    public GameObject crosshair;
    public GameObject damageNumbers;
	public GameObject bloodStain;
    public bool isCar;
    public GameObject explosionSprite;
    public GameObject eBullet;
    public GameObject shootPoint;
    public ParticleSystem particles;
    public GameObject audioHandler;
    public GameObject hurtPoint;
    public GameObject powerUp;
    public int powerUpDropRate;
    private float nextFire = 0.0F;
    public float fireRate;
    public bool isShooter;
    public bool isCharger;
    private float currentHealth;
    bool isFrozen;
    bool isStopped;
    GameCore core;


    private float nextActionTime = 1.0f;
    public float period = 0.5f;

    // Use this for initialization
    void Start () {
        core = FindObjectOfType<GameCore>();
        currentHealth = maxHealth;
		if (player == null) 
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		}

        if (crosshair == null)
        {
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        }

        StartCoroutine(meleeAttack());

        


    }

    IEnumerator meleeAttack()
    {
        while (true)
        {
            hurtPoint.SetActive(false);
            yield return new WaitForSeconds(1f);
            hurtPoint.SetActive(true);
            yield return new WaitForSeconds(0.05f);
        }
    }


    public IEnumerator randomMovement(float w)
    {
        isStopped = true;
        yield return new WaitForSeconds(w);
        isStopped = false;
    }
    // Update is called once per frame
    void Update () {


    


        float distance = Vector2.Distance(transform.position, player.transform.position);
		Vector3 vectorToTarget = player.transform.position - transform.position;
		float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);

        float checker = Random.Range(0f, 100f);
        if(checker >= 98 && !isCar)
        {
            StartCoroutine(randomMovement(Random.Range(1f, 5f)));
        }


        if (distance <= range && distance > range2 )
        {

			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * moveSpeed);
        }


        else if (distance <= range2 && distance > stopRange && !isFrozen && !isStopped)
        {

            //move towards the player
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * moveSpeed);
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, moveSpeed * Time.deltaTime);

        }
        else if (distance <= stopRange && !isFrozen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * moveSpeed);
            if (isShooter && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
        







        if (currentHealth <= 0)
        {
			Die ();
        }
	
	}


    public float getBulletDamage()
    {
        return bulletDamage;
    }
		
    public void TakeDamage(float damage)
    {
        
        this.currentHealth -= damage;
        Vector3 bloodPos = new Vector3(transform.position.x, transform.position. y, 2f);
        if (!isCar)
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            GameObject blood = Instantiate(bloodStain, bloodPos, transform.rotation) as GameObject;
        }
        GameObject dNumbers = Instantiate(damageNumbers, transform.position, Quaternion.identity) as GameObject;
        dNumbers.GetComponentInChildren<TextMesh>().text = "-" + damage;
        crosshair.GetComponent<CrosshairScript>().enemyHit();
        audioHandler.GetComponent<AudioHandler>().playHurtSound(0);
        Destroy(dNumbers, 1);
    }

    void spawnPowerUp()
    {
        int random = Random.Range(0, 8);
        int chance = Random.Range(0, 100);
        if (chance >= powerUpDropRate)
        {
            GameObject pwUp = Instantiate(powerUp, transform.position, Quaternion.identity) as GameObject;
            pwUp.GetComponent<PowerupScript>().setPowerupType(random);
        }

    }

    IEnumerator freezeEnemy(float wait)
    {
        isFrozen = true;
        //placeholder effects
        sprite.GetComponent<SpriteRenderer>().color = Color.blue;
        yield return new WaitForSeconds(wait);
        isFrozen = false;
        sprite.GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void freeze(float time)
    {
        StartCoroutine(freezeEnemy(time));
    }


    public void explosionAmmoEffect()
    {
        StartCoroutine(expAmmoEff(1));
    }

    IEnumerator expAmmoEff(float wait)
    {
        //placeholder effects
        sprite.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(wait);
        sprite.GetComponent<SpriteRenderer>().color = Color.white;

    }



    void Die()
	{
		GameObject gm = GameObject.FindGameObjectWithTag ("GameManager");
        if (isCar)
        {
            GameObject explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 1f);
        }
        spawnPowerUp();
        core.lastLevelEnemySlain();
        core.lastLevelScoreGained(enemyScoreWorth);
		Destroy (gameObject);
	}

    void Shoot()
    {
        GameObject bullet = Instantiate(eBullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
    }

}
