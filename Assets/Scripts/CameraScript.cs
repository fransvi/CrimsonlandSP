using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {


    public GameObject player;
    public float leftLimit;
    public float rightLimit;
    public float topLimit;
    public float bottomLimit;

    Vector3 originalCameraPosition;
    public float baseShakeAmt;
    float shakeAmt = 0;
    public Camera mainCamera;


    // Use this for initialization
    void Start () {

        mainCamera = this.gameObject.GetComponent<Camera>();
        this.player = GameObject.FindGameObjectWithTag("Player");
       // Camera.main.orthographicSize = Camera.main.orthographicSize * Screen.width / Screen.height * 2.0f;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shakeCamera();
        }


        Vector3 fixedPos = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, fixedPos, Time.deltaTime * Mathf.Clamp((fixedPos - transform.position).sqrMagnitude * 8, .1f, 5));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, bottomLimit,topLimit), transform.position.z);
        originalCameraPosition = transform.position;

    }

    public void shakeCamera()
    {
        shakeAmt = baseShakeAmt * 0.025f;
        InvokeRepeating("CameraShake", 0, 0.01f);
        Invoke("StopShaking", 0.3f);
    }

    void CameraShake()
    {
        if(shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            pp.z += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }


    public void setShakeAmount(float amt)
    {
        this.baseShakeAmt = amt;
    }


}
