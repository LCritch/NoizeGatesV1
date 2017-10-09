using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;

public class InGameHud : MonoBehaviour {

    public GameObject Player;
    public GameObject MainCamera;

	public Canvas InGame;
	public Canvas PauseMenu;
    public Canvas PlayerDeath;
    public Canvas EndLevel;

	public Text PlayerScore;
    public Text DeathScore;
    public Text FinalScore;
    public Text DiedTime;

    public Button PauseButton;
    public Button ResumeGame;
    public Button MainMenuReturn;
    public Button QuitGame;

    public AudioSource ASource;

    public float DiedScore;
    public float CurrentTime;
    public float TotalSeconds;
    public float CurrentSongTotal;

    Stopwatch stopWatch = new Stopwatch();
    TimeSpan ts;

    void Awake()
    {
        Time.timeScale = 1.0f;
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            Player.GetComponent<ScoreManager>().isAlive = true;
            stopWatch.Start();
        }
    }

	// Use this for initialization
	void Start () {
        
		InGame=InGame.GetComponent<Canvas>();
        PauseMenu = PauseMenu.GetComponent<Canvas>();
        PlayerDeath = PlayerDeath.GetComponent<Canvas>();
        EndLevel = EndLevel.GetComponent<Canvas>();

        PauseButton = PauseButton.GetComponent<Button>();
        ResumeGame = ResumeGame.GetComponent<Button>();
        MainMenuReturn = MainMenuReturn.GetComponent<Button>();
        QuitGame = QuitGame.GetComponent<Button>();


        InGame.enabled = true;
        PauseMenu.enabled = false;
        PlayerDeath.enabled = false;
        EndLevel.enabled = false;

        CurrentSongTotal = MainCamera.GetComponent<SongLoader>().SongSeconds;
	
	}

    void Update()
    {
        DiedScore = Player.GetComponent<ScoreManager>().CurrentScore;
    }
    

    void FixedUpdate()
    {
        TotalSeconds += Time.deltaTime;
        ts = stopWatch.Elapsed;

        DiedTime.text = "TIME SYNCED : " + string.Format("{0:00} : {1:00} : {2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        FinalScore.text = "FINAL SCORE : " + Player.GetComponent<ScoreManager>().CurrentScore.ToString();

        
        


        if(TotalSeconds >= CurrentSongTotal)
        {
            displayWinMenu();
        }

        if(Player.GetComponent<ScoreManager>().isAlive == false)
        {
            displayDeathMenu();
        }
    }

    void displayDeathMenu()
    {
        Time.timeScale = 0.0f;
        InGame.enabled = false;
        PlayerDeath.enabled = true;
        DeathScore.text = "FINAL SCORE :  " + DiedScore.ToString();
        ASource.Pause();
    }

    void displayWinMenu()
    {
        Time.timeScale = 0.0f;
        InGame.enabled = false;
        EndLevel.enabled = true;
        ASource.Stop();
    }
    public void RestartLevel()
    {
        Application.LoadLevel("TestGameplay");
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
	
}
