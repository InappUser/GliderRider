using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Run : MonoBehaviour {
    public GameObject collectiblesParent;
    public Text runScoreText;
    public GameObject playerSpawn;

    private GameObject[] collectibleSpawnGroups;
    private int score;
    private int fuelCanisters;
    // Use this for initialization
    void Start()
    {
        //Debug.Log("cc:" + collectiblesParent.transform.childCount);
        collectibleSpawnGroups = GameObject.FindGameObjectsWithTag("CollectibleGroup");
        foreach (GameObject go in collectibleSpawnGroups) { //dissable all groups before enableing the one wanted
            go.SetActive(false);
        }
        SpawnCollectibles();
    }
    
    public void AddFuelCannister()
    {
        fuelCanisters++;
        score = fuelCanisters * 10;
        runScoreText.text= score.ToString();
    }

    void SpawnCollectibles() {
        if (collectibleSpawnGroups.Length < 1) return;
        int groupToSpawn = Random.Range(0, collectibleSpawnGroups.Length);
        collectibleSpawnGroups[groupToSpawn].SetActive(true);

    }

   public void ResetRun()
    {
        score = 0;
        fuelCanisters = 0;
        runScoreText.text = "";
        ResetPlayer();
        SpawnCollectibles();
    }
    void ResetPlayer()
    {
        print("ocur");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<PlaneControls>().Reset();

    }
}
