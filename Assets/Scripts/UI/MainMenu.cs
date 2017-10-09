using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MobileImporter))]
public class MainMenu : MonoBehaviour
{

	public MobileImporter importer;
	public AudioSource ASource;

    //http://xenosmashgames.com/creating-start-menu-unity-5/

    public Canvas MainMenuCanvas;
    public Canvas OptionsMenuCanvas;
    public Canvas MainLevelsCanvas;
    public Canvas CustomLevelsCanvas;
    public Canvas CustomLevelWarnCanvas;

    //Button holders for main menu
    public Button PlayMainLevels;
    public Button PlayCustomLevels;
    public Button QuitGame;

	//Buttons for main levels
	public Button TestLevelSelect;

    //Button holders for custom track warn
    public Button CustomWarnOK;
    public Button CustomWarnBack;

    //Button holders for Options Menu
    public Button DeleteSaves;
    public Button RefreshCustomTracks;

	//Button to play custom track
	public Button PlayCustomTrack;

	//Public variables for holding the track info on the custom level screen
	public float SongSeconds;
	public Text CurrentPlayingTrack;
	public Text CurrentPlayingSeconds;
	private string CurrentTrackTitle;
	public string CustomTrackLocation;

    private float Minutes;
    private float Seconds;
    private float Fraction;

    private bool SongSelected;

    // Use this for initialization
    void Start()
    {

		PlayMainLevels = PlayMainLevels.GetComponent<Button> ();
		PlayCustomLevels = PlayCustomLevels.GetComponent<Button> ();
		QuitGame = QuitGame.GetComponent<Button>();

		MainMenuCanvas = MainMenuCanvas.GetComponent<Canvas> ();
		OptionsMenuCanvas = OptionsMenuCanvas.GetComponent<Canvas> ();
		MainLevelsCanvas = MainLevelsCanvas.GetComponent <Canvas> ();
		CustomLevelsCanvas = CustomLevelsCanvas.GetComponent<Canvas> ();
		CustomLevelWarnCanvas = CustomLevelWarnCanvas.GetComponent<Canvas> ();

		//buttons on custom warn
		CustomWarnOK = CustomWarnOK.GetComponent<Button> ();
		CustomWarnBack = CustomWarnBack.GetComponent<Button> ();

		PlayCustomTrack = PlayCustomTrack.GetComponent<Button>();

		//buttons for options menu
		DeleteSaves = DeleteSaves.GetComponent<Button> ();
		RefreshCustomTracks = RefreshCustomTracks.GetComponent<Button> ();

		//Testlevel select button
		TestLevelSelect = TestLevelSelect.GetComponent<Button>();

		MainMenuCanvas.enabled = true;
		OptionsMenuCanvas.enabled = false;
		CustomLevelsCanvas.enabled = false;
		CustomLevelWarnCanvas.enabled = false;
		MainLevelsCanvas.enabled = false;



    }


	public void GoToMainMenu()
	{
		MainMenuCanvas.enabled = true;
		OptionsMenuCanvas.enabled = false;
		CustomLevelsCanvas.enabled = false;
		CustomLevelWarnCanvas.enabled = false;
		MainLevelsCanvas.enabled = false;
	}

	public void GoToMainLevels()
	{
		MainLevelsCanvas.enabled = true;
		MainMenuCanvas.enabled = false;
	}

	public void GoToTestLevel()
	{
		Application.LoadLevel("TestGameplay");
		return;
	}

	public void GoToCustomWarn()
	{ 
		CustomLevelWarnCanvas.enabled = true;
		MainMenuCanvas.enabled = false;
	}

	public void GoToCustomLevels()
	{
		CustomLevelsCanvas.enabled = true;
		CustomLevelWarnCanvas.enabled = false;
        PlayCustomTrack.enabled = false;
	}


	public void GoToOptions()
	{
		OptionsMenuCanvas.enabled = true;
		MainMenuCanvas.enabled = false;

	}

	public void DeleteSaveData()
	{
		Debug.LogWarning ("Deleting player save data file");
	}

	public void RefreshCustomSongs()
	{
		Debug.LogWarning ("Refreshing Custom Tracks on Device, may take some time");
	}

	public void ExitGame()
	{
		Application.Quit ();
		Debug.Log ("Quitting Game");
	}

	public void SaveTrackLocation()
	{
		PlayerPrefs.SetString("CurrentCustomTrack",CustomTrackLocation);
		PlayerPrefs.Save();
		Application.LoadLevel("TestGameplay");
	}

	public void FileSelected(string path)
	{
		GetComponent<AudioSource>().clip = importer.ImportFile(path);
		CustomTrackLocation = path;
        SongSelected = true;
		GetComponent<AudioSource>().Play();
	}

    // Update is called once per frame
    void Update()
    {
		if (CustomLevelsCanvas.enabled)
		{
			importer = GetComponent<MobileImporter>();
			
			importer.OpenBrowser();

            if (SongSelected)
            {
                PlayCustomTrack.enabled = true;
                SongSeconds = ASource.clip.length;
                CurrentTrackTitle = ASource.clip.ToString().Replace("(UnityEngine.AudioClip)", "");

                Minutes = ASource.clip.length / 60.0f;
                Seconds = ASource.clip.length % 60.0f;
                Fraction = (ASource.clip.length * 100.0f) % 100.0f;

                CurrentPlayingTrack.text = "File Name: " + CurrentTrackTitle;
                CurrentPlayingSeconds.text = "File Length: " + string.Format("{0:00}:{1:00}:{2:00}", Minutes, Seconds, Fraction);
            }
		}
		else{
			importer.CloseBrowser();
		}
    }
}
