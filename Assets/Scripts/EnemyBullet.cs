using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    private Rigidbody ammoRb;
    public GameObject Enemy;
    public float damage;
    public float speed;

    // Use this for initialization
    void Start () {

        ammoRb = GetComponent<Rigidbody>();
        ammoRb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
        ammoRb.AddForce(transform.forward * 30, ForceMode.Impulse);
        Destroy(this.gameObject, 3);
        damage = Enemy.GetComponent<EnemyScript>().getBulletDamage();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().takeDamage(damage);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
            Destroy(this.gameObject);
         
        }

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "GameManager")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "PowerUp")
        {
            Destroy(this.gameObject);
        }
    }
}
