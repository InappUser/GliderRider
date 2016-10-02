using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Run : MonoBehaviour {
    public Text scoreText;
    public GameObject collectiblesParent;

    private GameObject[] collectibleSpawnGroups;
    private int score;
    private int fuelCanisters;
    // Use this for initialization
    void Start() {
        print("cc:" + collectiblesParent.transform.childCount);
        collectibleSpawnGroups = GameObject.FindGameObjectsWithTag("CollectibleGroup");
        foreach (GameObject go in collectibleSpawnGroups) { //dissable all groups before enableing the one wanted
            go.SetActive(false);
        }
        SpawnCollectibles();
    }
    public void FuelCanisterCollected()
    {
        fuelCanisters++;
        scoreText.text = (fuelCanisters * 10).ToString();
    }

    void SpawnCollectibles() {
        if (collectibleSpawnGroups.Length < 1) return;
        int groupToSpawn = Random.Range(0, collectibleSpawnGroups.Length);
        collectibleSpawnGroups[groupToSpawn].SetActive(true);

    }



}
