using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    int curentPhase;
    GameObject Edit;
    GameObject MainMenu;
    GameObject Game;
    TMP_Text ListOfName;
    InputField InputName;
    ArrayList Players = new ArrayList();
    int Turn = 0;

    private void Start()
    {
        InputName = GameObject.Find("Name").GetComponent<InputField>();
        ListOfName = GameObject.Find("List").GetComponent<TMP_Text>();
        MainMenu = GameObject.Find("Main Menu");
        Edit = GameObject.Find("Edit");
        Game = GameObject.Find("Game");
        Edit.SetActive(false);
        Game.SetActive(false);
    }
    public void PLay()
    {
        MainMenu.SetActive(false);
        Edit.SetActive(true);
    }
    public void Add()
    {
        if (InputName.text != "" && Players.Count < 6)
        {
            Players.Add(InputName.text);
            InputName.text = "";
            RefreshPlayersUI();
        }
    }
    private void RefreshPlayersUI()
    {
        var text = "";
        var firstCheck = 1;
        foreach (var player in Players)
        {
            if (firstCheck != 1)
            {
                text = text + " \\ " + player;
            }
            else
            {
                text = text + player;
                firstCheck++;
            }
        }
        ListOfName.text = text;
    }

    public void Reset()
    {
        Players = new ArrayList();
        RefreshPlayersUI();
    }
    public void Back()
    {
        MainMenu.SetActive(true);
        Edit.SetActive(false);
    }
    public void StartTheGame()
    {
        if (Players.Count > 1)
        {
            Turn = 1;
            Edit.SetActive(false);
            Game.SetActive(true);
            StarterPlayerUI();
            curentPhase = 1;
            PhaseNextStep(curentPhase);
        }
    }
    #region Phase 
    public void NextPhase()
    {
        if (curentPhase != 4)
        {
            curentPhase++;
        }
        else
        {
            Turn++;
            var box = GameObject.Find("Turn").GetComponent<TMP_Text>();
            box.text = "Turn: "+Turn;
            curentPhase = 1;
            PlayerSelecter();
        }
        PhaseNextStep(curentPhase);
    }
    public void PrePhase()
    {
        if (curentPhase == 1 && Turn == 1)
        {
            return;
        }
        if (curentPhase != 1)
        {
            curentPhase--;
        }
        else
        {
            Turn--;
            var box = GameObject.Find("Turn").GetComponent<TMP_Text>();
            box.text = "Turn: " + Turn;
            curentPhase = 4;
            PlayerSelecter();
        }
        PhasePreStep(curentPhase);
    }
    private void PhaseNextStep(int phaseNumber)
    {
        var previous = 0;
        if (curentPhase == 1)
        {
            previous = 4;
        }
        else
        {
            previous = phaseNumber - 1;
        }
        PhaseSelection(phaseNumber, previous);
    }
    private void PhasePreStep(int phaseNumber)
    {
        var next = 0;
        if (curentPhase == 4)
        {
            next = 1;
        }
        else
        {
            next = phaseNumber + 1;
        }
        PhaseSelection(phaseNumber, next);
    }
    private void PhaseSelection(int phaseNumber, int deselected)
    {
        Image image;
        image = GameObject.Find("select" + deselected).GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
        image = GameObject.Find("select" + phaseNumber).GetComponent<Image>();
        tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
    }
    #endregion

    private void StarterPlayerUI()
    {
        int index = 1;
        foreach (var player in Players)
        {
            var box = GameObject.Find(index + ".Player").GetComponent<TMP_Text>();
            box.text = player.ToString();
            index++;
        }
        PlayerSelecter();
    }
    private void PlayerSelecter()
    {
        var selectedPlayer = Turn % Players.Count;
        if (selectedPlayer == 0)
        {
            selectedPlayer = Players.Count;
        }
        Image image;
        Color tempColor;
        for (int i = 0; i < Players.Count; i++)
        {
            image = GameObject.Find("player" + (i+1)).GetComponent<Image>();
            tempColor = image.color;
            tempColor.a = 0.0f;
            image.color = tempColor;
        }
        image = GameObject.Find("player" + selectedPlayer).GetComponent<Image>();
        tempColor = image.color;
        tempColor.a = 0.5f;
        image.color = tempColor;
    }
    public void EndGame()
    {
        Game.SetActive(false);
        Edit.SetActive(true);
        Players = new ArrayList();
        RefreshPlayersUI();
    }
}
