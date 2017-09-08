using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {

    public AudioClip[] weaponSounds;
    public AudioClip[] hurtSounds;
    public AudioClip[] explosionSounds;
    public AudioClip reloadSound;
    public AudioClip pickUpSound;
    private float volLowRange = .25f;
    private float volHighRange = 0.5f;
    public AudioSource source;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	
	}

    public void playGunSound(int num)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(weaponSounds[num], vol);
    }
    public void playReloadSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(reloadSound, vol);
    }
    public void playHurtSound(int num)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(hurtSounds[num], vol);
    }

    public void playPickUpSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        source.PlayOneShot(pickUpSound, vol);
    }
    public void playExplosionSound(int num)
    {
        float vol = Random.Range(volLowRange, volHighRange);
        if(gameObject != null)
        {
            source.PlayOneShot(explosionSounds[num], vol);
        }

    }
}
