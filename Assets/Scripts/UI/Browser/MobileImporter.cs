using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.IO;

using System;

[AddComponentMenu("Audio/MobileImporter")]
public class MobileImporter : MonoBehaviour
{
    Browser browser;

    private AudioClip audioClip;

    private WWW www;

    /// <summary>
    /// Imports an mp3 file. Only the start of a file is imported at first.
    /// The remaining part of the file will be imported bit by bit to speed things up. 
    /// </summary>
    /// <returns>
    /// Audioclip containing the song.
    /// </returns>
    /// <param name='filePath'>
    /// Path to mp3 file.
    /// </param>
    public AudioClip ImportFile(string filePath)
    {

        if (audioClip != null)
            AudioClip.Destroy(audioClip);

        www = new WWW("file://" + filePath);

        while (www.progress < 1) { }

       // Debug.Log(www.data);

        audioClip = www.GetAudioClip(false,false);

        audioClip.name = Path.GetFileName(filePath);

        return audioClip;
    }

    public void FileSelectedCallback(string path)
    {
        SendMessage("FileSelected", path, SendMessageOptions.DontRequireReceiver);
        CloseBrowser();
    }

    /// <summary>
    /// Opens a browser for song selection. Imports a (new) song. (re) initializes analyses and variables.
    /// </summary>
    /// <param name='c'>
    /// When a file is selected c is called.
    /// </param>
    public void OpenBrowser()
    {
        if (browser == null)
            browser = Browser.Create(gameObject);
        else
            browser.gameObject.SetActive(true);
    }

    /// <summary>
    /// Close the browser without doing anything else.
    /// </summary>
    public void CloseBrowser()
    {
        if (browser != null)
            browser.gameObject.SetActive(false);
    }
}
