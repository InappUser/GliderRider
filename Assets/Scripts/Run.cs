using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Run : MonoBehaviour {
    public GameObject collectiblesParent;
    public Text runScoreText;
    public GameObject playerSpawn;
    public GameObject Target;

    private GameObject[] collectibleSpawnGroups;
    private Hashtable targetLandingValues;
    private int score;
    private int fuelCanisters;
    private bool isRunning = true;


    public bool GetIsRunning()
    {
        return isRunning;
    }
    // Use this for initialization
    void Start()
    {
        //Debug.Log("cc:" + collectiblesParent.transform.childCount);
        collectibleSpawnGroups = GameObject.FindGameObjectsWithTag("CollectibleGroup");

        PopulateTargetLandingVals();
        ResetRun();
    }
    void PopulateTargetLandingVals() {
        targetLandingValues = new Hashtable();
        Transform[] targetTs = System.Array.ConvertAll<MeshCollider, Transform>(Target.GetComponentsInChildren<MeshCollider>(), x => x.transform); //more complex than necessary?
        for (int i = 0; i < targetTs.Length; i++)
        {
            if (targetTs[i].GetComponent<TargetSpaceValue>())
            {
                targetLandingValues.Add(targetTs[i].name, targetTs[i].GetComponent<TargetSpaceValue>().targetValue);
            }
            else {
                Debug.LogWarning("No TargetSpaceValue component on target object");
            }
             //print("t"+i + ( targetTs[i].name)+" = " + targetLandingValues[targetTs[i].name]);
        }

    }
    
    public void AddFuelCannister()
    {
        fuelCanisters++;
        AddToScore(fuelCanisters * 10);
    }
    void AddToScore(int scoreAdded)
    {
        score = scoreAdded;
        runScoreText.text = score.ToString();
    }

    void SpawnCollectibles() {

        if (collectibleSpawnGroups.Length < 1) return;
        foreach (GameObject go in collectibleSpawnGroups)
        { //dissable all groups before enableing the one wanted
            go.SetActive(false);
        }
        int groupToSpawn = Random.Range(0, collectibleSpawnGroups.Length);
        collectibleSpawnGroups[groupToSpawn].SetActive(true);

    }
    public IEnumerator EndRun(string lastCollidedName)
    {
        isRunning = false;
        AddToScore((int)targetLandingValues[lastCollidedName]);
        yield return new WaitForSeconds(5);
        ResetRun();
    }
   void ResetRun()
    {
        score = 0;
        fuelCanisters = 0;
        runScoreText.text = "";
        isRunning = true;
        ResetPlayer();
        SpawnCollectibles();
    }
    void ResetPlayer()
    {
        print("ocur");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerSpawn.transform.position;
        player.transform.rotation = playerSpawn.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = true; //resetting the physics for the glider
        player.GetComponent<Rigidbody>().isKinematic = false;
        // player.GetComponent<PlaneControls>().Reset(); // Commented out during refactoring to add landing target

    }
}
