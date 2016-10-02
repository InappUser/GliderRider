using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public Text overallScoreText;

    private List<int> runScores;
    private float totalScore;

    void Start()
    {
        runScores = new List<int>();
    }


   public void SaveRunScore(int curRunScore) {
        runScores.Add(curRunScore);
    }
 
}
