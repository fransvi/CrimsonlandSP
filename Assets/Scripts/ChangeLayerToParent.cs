using UnityEngine;
using System.Collections;

public class ChangeLayerToParent : MonoBehaviour {

	// Use this for initialization
	void Start () {

        this.gameObject.GetComponent<Renderer>().sortingLayerID = transform.parent.GetComponent<Renderer>().sortingLayerID;

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
