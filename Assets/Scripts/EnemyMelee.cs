using UnityEngine;
using System.Collections;

public class EnemyMelee : MonoBehaviour {


    public float meleeDamage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {

            col.gameObject.GetComponent<PlayerController>().takeDamage(meleeDamage);
        }

        if (col.gameObject.tag == "GameManager")
        {

        }
    }
}
