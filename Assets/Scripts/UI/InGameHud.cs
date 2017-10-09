using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InGameHud : MonoBehaviour {

	public Canvas InGame;
	public Canvas PauseMenu;

	public Text ComboMeter;
	public Text PlayerScore;

    public Button PauseButton;
    public Button ResumeGame;
    public Button MainMenuReturn;
    public Button QuitGame;

    public AudioSource ASource;

//	protected virtual void OnEnable()
//	{
//		Lean.LeanTouch.OnMultiTap += OnMultiTap;
//	}
//
//	protected virtual void OnDisable()
//	{
//		Lean.LeanTouch.OnMultiTap -= OnMultiTap;
//	}
//
//	public void OnMultiTap(Lean.LeanFinger finger)
//	{
//		if(!finger.IsOverGui)
//		{
//
//		}
//	}

	// Use this for initialization
	void Start () {

		InGame=InGame.GetComponent<Canvas>();
        PauseMenu = PauseMenu.GetComponent<Canvas>();
        PauseButton = PauseButton.GetComponent<Button>();
        ResumeGame = ResumeGame.GetComponent<Button>();
        MainMenuReturn = MainMenuReturn.GetComponent<Button>();
        QuitGame = QuitGame.GetComponent<Button>();



        InGame.enabled = true;
        PauseMenu.enabled = false;

	
	}

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        ASource.Pause();
        InGame.enabled = false;
        PauseMenu.enabled = true;
    }

    public void ContinuePlay()
    {
        Time.timeScale = 1.0f;
        InGame.enabled = true;
        PauseMenu.enabled = false;
        ASource.UnPause();
        
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        ASource.Stop();
        Application.LoadLevel("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
