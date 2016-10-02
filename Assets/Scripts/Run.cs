using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Run : MonoBehaviour {
    public Text scoreText;

    int score;
    int fuelCanisters;
    // Use this for initialization
    public void FuelCanisterCollected()
    {
        fuelCanisters++;
        scoreText.text = (fuelCanisters * 10).ToString();
    }


}
