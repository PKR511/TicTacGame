using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject WinObject;
    public Text winText;
    public Text winScore;

    public GameObject DrawObject;
    public Text drawScore;
   

    public void Awake()
    {
        WinObject.SetActive(false);
        DrawObject.SetActive(false);
    }


    public void ShowGameWinMsg(GameData json ,string text)
    {
        DrawObject.SetActive(false);
        WinObject.SetActive(true);
        winText.text = "Player " + text + " Wins";
        winScore.text = GetScore(json);
    }

    public void ShowDrawMsg(GameData json)
    {
        WinObject.SetActive(false);
        DrawObject.SetActive(true);
        drawScore.text = GetScore(json);
    }

    public string GetScore(GameData json)
    {
        string score = "Tatal Game Played :"+ json.totalGamePlayed+"\n Red Win: "+ json.redWin + " Blue Win: " + json.blueWin;
        return score;
    }
}
