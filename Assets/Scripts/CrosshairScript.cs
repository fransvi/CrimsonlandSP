using UnityEngine;
using System.Collections;

public class CrosshairScript : MonoBehaviour {
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;
    public float hitmarkerDuration;
    public GameObject[] crosshair;

    // Use this for initialization
    void Start () {

        crosshair[0].SetActive(true);
	
	}
	
	// Update is called once per frame
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

    }

    public void enemyHit()
    {

        StartCoroutine(hitMarker(hitmarkerDuration));

    }

    IEnumerator hitMarker(float wait)
    {
        crosshair[1].SetActive(true);
        yield return new WaitForSeconds(wait);
        crosshair[1].SetActive(false);
    }
}
