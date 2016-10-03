using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

    public Canvas exitMenu;
    public Button singlePlayerButton;
    public Button exitButton;
    public Canvas levelMenu;

    void Start () {
        exitMenu = exitMenu.GetComponent<Canvas>();
        levelMenu = levelMenu.GetComponent<Canvas>();
        singlePlayerButton = singlePlayerButton.GetComponent<Button>();
        exitButton = exitButton.GetComponent<Button>();
        exitMenu.enabled = false;
        levelMenu.enabled = false;
    }

    public void ExitPress() {
        exitMenu.enabled = true;
        singlePlayerButton.enabled = false;
        exitButton.enabled = false;
    }

    public void BackPress() {
        exitMenu.enabled = false;
        singlePlayerButton.enabled = true;
        exitButton.enabled = true;
    }

    public void SinglePlayerPress() {
        levelMenu.enabled = true;
        singlePlayerButton.enabled = false;
        exitButton.enabled = false;
    }

    public void LevelSelected() {
        Application.LoadLevel(1);
    }

    public void ExitGame() {
        Application.Quit();
    }

   
}
