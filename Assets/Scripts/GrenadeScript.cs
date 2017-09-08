using UnityEngine;
using System.Collections;

public class GrenadeScript : MonoBehaviour {

    public float radius;
    public float power;
    public float fusetime;
    public float speed;
    public float damage;
    public GameObject explosionSprite;
    Rigidbody rb;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
        rb.AddForce(transform.forward * 30, ForceMode.Impulse);
        StartCoroutine(Explode(fusetime));

    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator Explode(float ft)
    {

        yield return new WaitForSeconds(ft);
        StartCoroutine(ExpEffect(2f));
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Rigidbody>())
            {
                hit.gameObject.GetComponent<EnemyScript>().TakeDamage(damage);
                hit.gameObject.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPos, radius, 3.0F);
            }

        }
        
        Destroy(this.gameObject);



    }
    IEnumerator ExpEffect(float ft)
    {
        GameObject explosion = Instantiate(explosionSprite, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 1f);
        yield return new WaitForSeconds(ft);
        
    }
}
