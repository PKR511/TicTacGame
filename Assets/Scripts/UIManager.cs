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


    public void ShowGameWinMsg(JasonClass jasson ,string text)
    {
        DrawObject.SetActive(false);
        WinObject.SetActive(true);
        winText.text = "Player " + text + " Wins";
        winScore.text = GetScore(jasson);
    }

    public void ShowDrawMsg(JasonClass jasson)
    {
        WinObject.SetActive(false);
        DrawObject.SetActive(true);
        drawScore.text = GetScore(jasson);
    }

    public string GetScore(JasonClass jasson)
    {
        string score = "Tatal Game Played :"+jasson.totalGamePlayed+"\n Red Win: "+jasson.redWin + " Blue Win: " + jasson.blueWin;
        return score;
    }
}
