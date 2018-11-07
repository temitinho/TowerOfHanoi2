using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour {

    public Text moves;
    public GameObject optionsPanel;
    public GameObject completedPanel;
    public Toggle three;
    public Toggle four;
    public Toggle five;
    public Toggle six;
    public Toggle seven;
    public Toggle eight;
    public Toggle nine;
    public Toggle ten;
    public GameObject movesText;

    private void Start()
    {
        //three.isOn = true;
    }
    public void ShowMoves()
    {
        movesText.SetActive(true);
    }
    public void HideMoves()
    {
        movesText.SetActive(false);
    }
    public void Restat()
    {
        SceneManager.LoadScene("TowerOfHanoi");
    }
    public void ShowCompletedPanel()
    {
        completedPanel.SetActive(true);
    }
    void HideOptionsPanel ()
    {
        ShowMoves();
        optionsPanel.SetActive(false);
	}
    void ShowOptionsPanel()
    {
        optionsPanel.SetActive(true);
    }
    public void SubmitToggles()
    {
        SetToggles();
    }
    public void StartPlay()
    {
        SetToggles();
        GameController.instance.StartPlay();
        HideOptionsPanel();
        
    }
    void SetToggles()
    {
        if (three.isOn)
        {
            GameController.instance.numDiscs = 3;
            GameController.instance.InitializeGame();
        }
        else if (four.isOn)
        {
            GameController.instance.numDiscs = 4;
            GameController.instance.InitializeGame();

        }
        else if (five.isOn)
        {
            GameController.instance.numDiscs = 5;
            GameController.instance.InitializeGame();

        }
        else if (six.isOn)
        {
            GameController.instance.numDiscs = 6;
            GameController.instance.InitializeGame();

        }
        else if (seven.isOn)
        {
            GameController.instance.numDiscs = 7;
            GameController.instance.InitializeGame();

        }
        else if (eight.isOn)
        {
            GameController.instance.numDiscs = 8;
            GameController.instance.InitializeGame();

        }
        else if (nine.isOn)
        {
            GameController.instance.numDiscs = 9;
            GameController.instance.InitializeGame();

        }
        else if (ten.isOn)
        {
            GameController.instance.numDiscs = 10;
            GameController.instance.InitializeGame();

        }
    }
}
