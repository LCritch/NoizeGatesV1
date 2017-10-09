using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spectrum : MonoBehaviour {

    public GameObject Barrier;
    public GameObject SingleSpike;
    public GameObject DoubleSpike;
    public GameObject PlayerCube;

    public AudioSource Asource;
	public AudioSource PlayingAudioSource;

    public Vector3 currentPlayerPos;

    public Text OBJSpawnNum;
    public int ObjectToSpawn;

    //Channels for L/M/T/H
    float Channel1;
    float Channel2;
    float Channel3;
    float Channel4;
    float Channel5;

    private float[] Samples = new float [1024];
    public float Sum = 0.0f;

    public float interval = 2.0f;
    public float currentChannel;

    //delay timer for instantiating object
    public float firstInstantiate = 5;

    //0 equals only spawning a single object
    public float repeatRate = 5;


	//float values for blocking spawns of too many objects on the update function
	private float SingleSpikeMax;
	private float DoubleSpikeMax;
	private float BarrierMax;

	void Start () {
        StartCoroutine("randomSpawner");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        Asource.GetOutputData(Samples, 0);

        //Channels for L/M/T/H
        Channel1 = Samples[2] + Samples[4];
        Channel2 = Samples[12] + Samples[14];
        Channel3 = Samples[22] + Samples[24];
        Channel4 = Samples[32] + Samples[34];
        Channel5 = Samples[57] + Samples[60];


        //get the corresponding channel to use the switch statement to spawn the object;
        for (int i = 0; i < Channel1; i++)
        {
            ObjectToSpawn = 3;
        }

        for (int i = 0; i < Channel3; i++)
        {
            ObjectToSpawn = 1;
        }

        for (int i = 0; i < Channel5; i++)
        {
            ObjectToSpawn = 2;
        }

        //Asource.GetSpectrumData(Samples, 0, FFTWindow.BlackmanHarris);
 
        //OBJSpawnNum.text = "Object To Spawn: " + ObjectToSpawn.ToString();

        Debug.Log("Object Spawning: " + ObjectToSpawn.ToString());

        currentPlayerPos = PlayerCube.transform.position;
	
	}

    IEnumerator randomSpawner()
    {
        while (true)
        {

            switch (ObjectToSpawn)
            {
                case 1:
                    //InvokeRepeating("SpawnSingleSpike", firstInstantiate,repeatRate);
                    SpawnSingleSpike();
                    break;

                case 2:
                    //InvokeRepeating("SpawnDoubleSpike", firstInstantiate,repeatRate);
                    SpawnDoubleSpike();
                    break;

                case 3:
                    //InvokeRepeating("SpawnBarrier", firstInstantiate,repeatRate);
                    SpawnBarrier();
                    break;


            }

            yield return new WaitForSeconds(1.8f);
        }
    }

    void SpawnSingleSpike()
    {

        if (PlayerCube.GetComponent<PlayerController>().isOnBottom)
        {
            Instantiate(SingleSpike, new Vector3(currentPlayerPos.x, -6.5f) + new Vector3(30.0f, 0), Quaternion.identity);
        }
        //if the player is at the top of the screen then set the spawn loc to be the opposite side
        else if (PlayerCube.GetComponent<PlayerController>().isOnTop)
        {
            Instantiate(SingleSpike, new Vector3(currentPlayerPos.x, 6.5f) + new Vector3(30.0f, 0), Quaternion.Euler(0, 0, 180));
        }

    }

  void SpawnDoubleSpike()
    {
        if (PlayerCube.GetComponent<PlayerController>().isOnBottom)
        {
            Instantiate(DoubleSpike, new Vector3(currentPlayerPos.x, -6.5f) + new Vector3(30.0f, 0), Quaternion.identity);
        }
        //if the player is at the top of the screen then set the spawn loc to be the opposite side
        else if (PlayerCube.GetComponent<PlayerController>().isOnTop)
        {
            Instantiate(DoubleSpike, new Vector3(currentPlayerPos.x, 6.5f) + new Vector3(30.0f, 0), Quaternion.Euler(0, 0, 180));
        }
		
    }

    void SpawnBarrier()
    {
        if (PlayerCube.GetComponent<PlayerController>().isOnBottom)
        {
            Instantiate(Barrier, new Vector3(currentPlayerPos.x, 0) + new Vector3(30.0f, 0), Quaternion.identity);
        }
        //if the player is at the top of the screen then set the spawn loc to be the opposite side
        else if (PlayerCube.GetComponent<PlayerController>().isOnTop)
        {
            Instantiate(Barrier, new Vector3(currentPlayerPos.x, 0) + new Vector3(30.0f, 0), Quaternion.identity);
        }
        
    }
}
