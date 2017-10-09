using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System;
using System.IO;
using System.Collections.Generic;

public class Browser : MonoBehaviour
{
    private static GameObject listItemPrefab = Resources.Load<GameObject>("ListItemPrefab");
    private static GameObject browserPrefab = Resources.Load<GameObject>("BrowserPrefab");
    
    public List<string> extensions;

    public GameObject nothing;
    public GameObject upButton;
    public ScrollRect scrollRect;

    string[] drives;
    int currentDrive;
    string currentDirectory;

    public GameObject folderPanel;
    public GameObject filePanel;

    public List<string> folders;
    public List<string> files;
    public int folderIndex;
    public int fileIndex;

    public bool selectDrive;

    private GameObject receiver;

    /// <summary>
    /// instantiates a canvas and a file browser panel
    /// </summary>
    /// <param name="receiver"></param>
    /// <returns>the Browser component of the browser panel</returns>
    public static Browser Create(GameObject receiver)
    {       
        GameObject browserCanvas =  (GameObject)Instantiate(browserPrefab);
        Browser browser = browserCanvas.GetComponentInChildren<Browser>();

        browser.receiver = receiver;

        return browser;
    }



    void OnEnable()
    {
        if (GameObject.Find("EventSystem") == null)
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule), typeof(TouchInputModule));
    }

    public void Up()
    {
        if (currentDirectory == "/")
        {            
            selectDrive = true;
            ClearContent();
            BuildContent();
        }
        else
        {
            string cd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(drives[currentDrive]);

            currentDirectory = Directory.GetParent(drives[currentDrive] + "/" + currentDirectory).FullName;

            Directory.SetCurrentDirectory(cd);
            
            if (currentDirectory.Contains(drives[currentDrive]))               
                currentDirectory = currentDirectory.Remove(0, drives[currentDrive].Length-1);

            currentDirectory = currentDirectory.Replace("\\", "/");
           
            if (currentDirectory == "")
            {
                currentDirectory = "/";
            }

            ClearContent();
            BuildContent();
        }
    }

    

    // Use this for initialization
    void Start()
    {
        drives = Directory.GetLogicalDrives();        
        currentDirectory = "/";
        currentDrive = Mathf.Min(drives.Length-1,currentDrive);
        selectDrive = false;

        LoadLastDirectory();
        
        //check if directory still exists
        //if not, set directory back to "/"
        
        BuildContent();       
    }

    private void BuildContent()
    {
        folderIndex = 0;
        fileIndex = 0;
        folders = new List<string>();
        files = new List<string>();


        if (selectDrive)
        {
            foreach(string s in drives)
            {
                folders.Add(s);
            }
        }
        else
        {
            string cd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(drives[currentDrive]);

            try
            {
                foreach (string s in Directory.GetDirectories(drives[currentDrive] + currentDirectory))
                {
                    DirectoryInfo info = new DirectoryInfo(s);
                    string name = info.Name;
                    folders.Add(name);
                }
                
                foreach (string s in Directory.GetFiles(drives[currentDrive] + currentDirectory))
                {
                    string name = Path.GetFileName(s);
                    
                    FileInfo info = new FileInfo(s);
                    if(extensions.Contains(info.Extension))
                        files.Add(name);
                }
            }
            catch(Exception e) {
                Debug.LogWarning(e.GetType());
            }

            Directory.SetCurrentDirectory(cd);

        }

        if (folders.Count + files.Count == 0)
            EventSystem.current.SetSelectedGameObject(upButton);
    }

    private void ClearContent()
    {
        Button[] children = filePanel.GetComponentsInChildren<Button>();

        foreach (Button child in children)
            GameObject.Destroy(child.gameObject);

        children = folderPanel.GetComponentsInChildren<Button>();

        foreach (Button child in children)
            GameObject.Destroy(child.gameObject);
    }

    

    public void FileSelected(int index)
    {
        string path = drives[currentDrive] + currentDirectory + "/" +files[index];

        if (receiver == null)
            Debug.LogWarning("Receiver not set. Set reveiver to the GameObject that should receive the FileSelectedCallback(string path) message");
        else
            receiver.SendMessage("FileSelectedCallback", path, SendMessageOptions.DontRequireReceiver);
        
        SaveLastDirectory();
    }

    public void FolderSelected(int index)
    {
        if (selectDrive)
        {
            currentDrive = index;
            currentDirectory = "/";
            selectDrive = false;
        }
        else
        {
            if (currentDirectory == "/")
                currentDirectory = "";
            currentDirectory = currentDirectory + "/" + folders[index];
        }

        ClearContent();
        BuildContent();
    }

    // Update is called once per frame
    void Update()
    {
        //in coroutine
        if (fileIndex < files.Count)
        {
            GameObject listItem = (GameObject)Instantiate(listItemPrefab);

            Button button = listItem.GetComponent<Button>();
            int index = fileIndex;
            button.onClick.AddListener(
                () => { FileSelected(index); }
                );
            
            listItem.GetComponentInChildren<Text>().text = files[fileIndex];

            listItem.transform.SetParent(filePanel.transform, false);
            
            if (fileIndex == 0)
                EventSystem.current.SetSelectedGameObject(listItem);
            fileIndex++;
        }

        if (folderIndex < folders.Count)
        {
            GameObject listItem = (GameObject)Instantiate(listItemPrefab);

            Button button = listItem.GetComponent<Button>();
            int index = folderIndex;
            button.onClick.AddListener(
                () => {  FolderSelected(index); }
                );

            listItem.GetComponentInChildren<Text>().text = folders[folderIndex];


            listItem.transform.SetParent(folderPanel.transform, false);
            
            if (folderIndex == 0)
                EventSystem.current.SetSelectedGameObject(listItem);
            folderIndex++;
        }

        GameObject selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null)
        {
            EventSystem.current.SetSelectedGameObject(nothing);
        }
        else
        {
            RectTransform rt = selected.GetComponent<RectTransform>();

            float scrollRectY = scrollRect.transform.position.y;
            float selectedY = rt.position.y;
            
            if (Input.GetAxis("Vertical") < -.3f)
            {
                scrollRect.movementType = ScrollRect.MovementType.Clamped;
                Vector2 scrollVelocity = new Vector2();
                if (selectedY < scrollRectY)
                {
                    scrollVelocity.y = 900;
                    scrollRect.velocity = scrollVelocity;
                }
            }
            else if (Input.GetAxis("Vertical") > .3f)
            {
                scrollRect.movementType = ScrollRect.MovementType.Clamped;
                Vector2 scrollVelocity = new Vector2();
                if (selectedY > scrollRectY)
                {
                    scrollVelocity.y = -900;
                    scrollRect.velocity = scrollVelocity;
                }
            }
            else if (scrollRect.velocity.sqrMagnitude < 1)
                scrollRect.movementType = ScrollRect.MovementType.Elastic;

        }

        //scrolling with controller right stick
        //Vector2 scrollSpeed = new Vector2(0,Input.GetAxis("Vertical2"));
        //if (Mathf.Abs(scrollSpeed.y) > .2f)
        //    scrollRect.velocity = scrollSpeed * 900;
    }

    private void LoadLastDirectory()
    {
        currentDirectory = PlayerPrefs.GetString("currentDirectory","/");
        currentDrive = PlayerPrefs.GetInt("currentDrive", 0);
    }

    protected void SaveLastDirectory()
    {
        PlayerPrefs.SetString("currentDirectory", currentDirectory);
        PlayerPrefs.SetInt("currentDrive", currentDrive);
    }
}