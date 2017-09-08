using UnityEngine;
using System.Collections;

public class BloodScript : MonoBehaviour {

	public GameObject[] blood;
	

	// Use this for initialization
	void Start () {
		int random = Random.Range (0, blood.Length-1);
		blood [random].SetActive(true);
		Destroy (this.gameObject, 100);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
