using UnityEngine;
using System.Collections;
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

    // Use this for initialization
    void Start()
    {
		CurrentSong.clip = Importer.ImportFile(PlayerPrefs.GetString("CurrentCustomTrack"));
		CurrentSong.Play();
        //get the length of the song track
        SongSeconds = CurrentSong.clip.length;

        Trackname = CurrentSong.clip.ToString().Replace("(UnityEngine.AudioClip)", "");

        FileName.text = "Track: " + Trackname;
		TotalSeconds.text = "Song File Length: " + SongSeconds.ToString() + " Seconds";



        Debug.Log("Current Song Length : " + SongSeconds + " Seconds");

    }

    // Update is called once per frame
    void Update()
    {

    }
}

