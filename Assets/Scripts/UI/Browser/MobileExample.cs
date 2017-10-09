using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MobileImporter))]
public class MobileExample : MonoBehaviour {

    public MobileImporter importer;

	// Use this for initialization
	void Start () {
        importer = GetComponent<MobileImporter>();

        importer.OpenBrowser();
	}

    public void FileSelected(string path)
    {
        GetComponent<AudioSource>().clip = importer.ImportFile(path);
        GetComponent<AudioSource>().Play();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
