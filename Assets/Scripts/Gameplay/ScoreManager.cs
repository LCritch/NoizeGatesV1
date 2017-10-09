using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public float CurrentScore;
    public float MoveAddPoints;
    public float BarrierAddPoints;
    public float JumpOverSpikeAddPoints;

    private bool oneTime;
    public bool isAlive;

    public GameObject PlayerCube;
    public GameObject Barrier;

    public bool BarrierDestroyed;
    public Text ScoreHolder;

	void Awake()
    {
        isAlive = true;
    }
    
    // Use this for initialization
	void Start () {
        CurrentScore = 0.0f;
        BarrierAddPoints = 20.0f;
        oneTime = false;

        StartCoroutine("AddMovingScore");
	
	}
	
    void Update()
    {
        ScoreHolder.text = "SCORE: " + CurrentScore.ToString();
    }

	// Update is called once per frame
	void FixedUpdate () {


        if (BarrierDestroyed && !oneTime)
        {
            oneTime = true;
            Invoke("BarrierDestroyAdd", 0.3f);
            
        }

        
	}

    IEnumerator AddMovingScore()
    {
        while(true)
        {
            CurrentScore = CurrentScore + 10.0f;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void BarrierDestroyAdd()
    {
        CurrentScore = CurrentScore + BarrierAddPoints;
        oneTime = false;
    }
}
