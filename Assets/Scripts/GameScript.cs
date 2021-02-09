using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    int spriteIndex = -1;

    public UIManager uiManager;
    public bool isPlaying =true;
    public rowWiseToken[] tokens;
    public TextAsset jsonTextFile;
    private GameData gameData;


    public void Awake()
    {
        isPlaying = true;
        gameData = JsonUtility.FromJson<GameData>(jsonTextFile.text);
        string jsonObject = JsonUtility.ToJson(gameData);
        Debug.Log(jsonObject);
    }

    public void Start()
    {
        gameData.totalGamePlayed++;
    }

    public int PlayerTurn()
    {
        spriteIndex++;
        return spriteIndex % 2;
    }


    public void CheckGameStatus(int index)
    {
        if (CheckWin(index))
        {
           if(index == 0)
            {
                uiManager.ShowGameWinMsg(gameData, "Red");
                gameData.redWin++;
            }
            else
            {
               uiManager. ShowGameWinMsg(gameData,"Blue");
                gameData.blueWin++;
            }
            isPlaying = false;
            UpdateJsonFile();
        }
        else
        {
            if(spriteIndex >= 9)
            {
                uiManager.ShowDrawMsg(gameData);
            }
            isPlaying = true;
        }
    }

    public bool CheckWin(int index)
    {
        if (CheckWinForRowBased(index) 
            || CheckWinForColBased(index) 
            || CheckWinForRightDiagonal(index) 
            || CHeckWinForLeftDiagonal(index))
        {
            return true;
        }
        else
        {
            return false;
        }
      
    }//CheckWin

    public bool CheckWinForRowBased(int index)
    {
        int count = 0;
        for (int row = 0; row < tokens.Length; row++)
        {
            for (int col = 0; col < tokens[0].rowtoken.Length; col++)
            {
                if (tokens[row].rowtoken[col].unplayed == false)
                {
                    if (tokens[row].rowtoken[col].spriteIndex == index)
                        count++;
                }
            }
            if (count == tokens.Length)
            {
                Debug.Log("Win" + index);
                return true;
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }

    public bool CheckWinForColBased(int index)
    {
        int count = 0;
        for (int col = 0; col < tokens[0].rowtoken.Length; col++)
        {
            for (int row = 0; row < tokens.Length; row++)
            {
                if (tokens[row].rowtoken[col].unplayed == false)
                {
                    if (tokens[row].rowtoken[col].spriteIndex == index)
                        count++;
                }
            }
            if (count == tokens.Length)
            {
                Debug.Log("Win" + index);
                return true;
            }
            else
            {
                count = 0;
            }
        }
        return false;
    }
    public bool CheckWinForRightDiagonal(int index)
    {
        int count = 0;
        for (int col = 0; col < tokens[0].rowtoken.Length; col++)
        {
            for (int row = 0; row < tokens.Length; row++)
            {
                if (tokens[row].rowtoken[col].unplayed == false && col == row)
                {
                    if (tokens[row].rowtoken[col].spriteIndex == index)
                        count++;
                }
            }

        }
        if (count == tokens.Length)
        {
            Debug.Log("Win" + index);
            return true;
        }
        else
        {
            count = 0;
        }
        return false;
    }
    public bool CHeckWinForLeftDiagonal(int index)
    {
        int count = 0;
        for (int col = 0; col < tokens[0].rowtoken.Length; col++)
        {
            for (int row = 0; row < tokens.Length; row++)
            {
                if (tokens[row].rowtoken[col].unplayed == false && (col + row) == tokens.Length - 1)
                {
                    if (tokens[row].rowtoken[col].spriteIndex == index)
                        count++;
                }
            }

        }
        if (count == tokens.Length)
        {
            Debug.Log("Win" + index);
            return true;
        }
        else
        {
            count = 0;
        }
        return false;
    }
    public void UpdateJsonFile()
    {
       string jsonObject= JsonUtility.ToJson(gameData);
        System.IO.File.WriteAllText(Application.dataPath + "/Save.json", jsonObject);
    }
  
}//Class
[System.Serializable]
public struct rowWiseToken
{
    public TurnScript[] rowtoken;
}

