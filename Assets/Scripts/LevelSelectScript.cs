using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour {

    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button back;
    public SceneLoader sl;
    GameCore core;
    bool[] stagesUnlocked;
    public Button[] stageButtons;
    // Use this for initialization
    void Start()
    {
        core = FindObjectOfType<GameCore>();
        stagesUnlocked = core.getStagesUnlocked();
        for (int i = 0; i < stagesUnlocked.Length; i++)
        {
            if (stagesUnlocked[i])
            {
                stageButtons[i].interactable = (true);
            }
            else
            {
                stageButtons[i].interactable = (false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	
	}

    public void loadLevel1()
    {
        sl.setNextScene(5);
    }
    public void loadLevel2()
    {
        sl.setNextScene(6);
    }
    public void loadLevel3()
    {
        sl.setNextScene(7);
    }
    public void loadLevel4()
    {
        sl.setNextScene(8);
    }

    public void loadStore()
    {
        Application.LoadLevel("StoreMenu");
    }
    public void goBack()
    {
        Application.LoadLevel("MainMenu");
    }
}
