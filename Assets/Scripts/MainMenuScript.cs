using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {


    public Button startButton;
    public Button optionsButton;
    public Button exitButton;
    public Image Logo;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame()
    {
        Application.LoadLevel("LevelSelect");
    }


    public void Exit()
    {
        Application.Quit();
    }

}
