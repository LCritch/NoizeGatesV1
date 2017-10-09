using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.UI;

public class SongLoader : MonoBehaviour
{
//http://forum.unity3d.com/threads/importing-audio-files-at-runtime.140088/
//http://hellomeow.net/unity-mp3-importer/
//http://answers.unity3d.com/questions/238727/android-music-selection.html

	public MobileImporter Importer;
    public AudioSource CurrentSong;
    public float SongSeconds;
	public Text TotalSeconds;
	public Text FileName;
    public string Trackname;

    //Variables to display stopwatch & Song Length
    Stopwatch stopWatch = new Stopwatch();
    TimeSpan ts;

    public float Seconds;
    public float Minutes;
    public float Fraction;


    void Awake()
    {
        stopWatch.Start();
    }

    // Use this for initialization
    void Start()
    {
		CurrentSong.clip = Importer.ImportFile(PlayerPrefs.GetString("CurrentCustomTrack"));
		CurrentSong.Play();
        //get the length of the song track
        SongSeconds = CurrentSong.clip.length;
        Trackname = CurrentSong.clip.ToString().Replace("(UnityEngine.AudioClip)", "");

        FileName.text = "Track: " + Trackname;
		//TotalSeconds.text = "Song File Length: " + SongSeconds.ToString() + " Seconds";


        UnityEngine.Debug.Log("Current Song Length : " + SongSeconds + " Seconds");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ts = stopWatch.Elapsed;
        Minutes = CurrentSong.clip.length / 60.0f;
        Seconds = CurrentSong.clip.length % 60.0f;
        Fraction = (CurrentSong.clip.length * 100.0f) % 100.0f;

        TotalSeconds.text = string.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10) + string.Format("  {0:00}:{1:00}:{2:00}", Minutes, Seconds, Fraction);
    }
}

