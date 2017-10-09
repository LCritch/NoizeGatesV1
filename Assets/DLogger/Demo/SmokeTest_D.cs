using UnityEngine;
using System.Collections;

public class SmokeTest_D : MonoBehaviour {
	
	private int i;
	
	// Use this for initialization
	void Start () {
		
		D.log("LOG Test");
		D.warn("WARN Test");
		D.error("ERROR Test");
		
		D.log("AI", "AI Log");	
		D.log("Assert", "Asser Log");
		D.log("Audio", "Audio Log");
		D.log("Content", "Content Log");	
		D.log("Exception", "Exception Log");
		D.log("GameLogic", "GameLogic Log");	
		D.log("Graphics", "Graphics Log");	
		D.log("GUI", "GUI Log");
		D.log("GUIMessage", "GUIMEssage Log");	
		D.log("Input", "Input Log");	
		D.log("Networking", "Networking Log");	
		D.log("NetworkClient", "NetworkClient Log");	
		D.log("NetworkServer", "NetworkServer Log");	
		D.log("Physics", "Physics Log");
		D.log("Replay", "Replay Log");	
		D.log("System", "System Log");	
		D.log("Terrain", "Terrain Log");	
		
		D.log( "System", "Dump Stack<br><br>{0}", System.Environment.StackTrace );
		
	}
	
	void Update() {
		i++;	
		D.log("Another Log Entry");
		D.log("GameLogic", "SmokeTest i = {0}", i);
		D.log("GameLogic", "Some GameObject = " + gameObject);
	}

}
