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
    private JasonClass jasonClass;


    public void Awake()
    {
        isPlaying = true;
        jasonClass = JsonUtility.FromJson<JasonClass>(jsonTextFile.text);
        string jasonObject = JsonUtility.ToJson(jasonClass);
        Debug.Log(jasonObject);
    }

    public void Start()
    {
        jasonClass.totalGamePlayed++;
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
                uiManager.ShowGameWinMsg(jasonClass, "Red");
                jasonClass.redWin++;
            }
            else
            {
               uiManager. ShowGameWinMsg(jasonClass,"Blue");
                jasonClass.blueWin++;
            }
            isPlaying = false;
            UpdateJasonFile();
        }
        else
        {
            if(spriteIndex >= 9)
            {
                uiManager.ShowDrawMsg(jasonClass);
            }
            isPlaying = true;
        }
    }

    public bool CheckWin(int index)
    {
        int count = 0;
       //performing row check
        for(int row = 0; row < tokens.Length; row++)
        {
            for(int col = 0; col < tokens[0].rowtoken.Length; col++)
            {
                if (tokens[row].rowtoken[col].unplayed == false)
                {
                    if (tokens[row].rowtoken[col].spriteIndex == index)
                        count++;
                }
            }
            if(count == tokens.Length)
            {
                Debug.Log("Win"+index);
                return true;
            }
            else
            {
                count = 0;
            }
        }

        //performing Col check
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
        //performing right Diagonal check
        for (int col = 0; col < tokens[0].rowtoken.Length; col++)
        {
            for (int row = 0; row < tokens.Length; row++)
            {
                if (tokens[row].rowtoken[col].unplayed == false && col==row)
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
        //performing right Diagonal check
        for (int col = 0; col < tokens[0].rowtoken.Length; col++)
        {
            for (int row = 0; row < tokens.Length; row++)
            {
                if (tokens[row].rowtoken[col].unplayed == false && (col + row) == tokens.Length-1)
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
    }//CheckWin

    public void UpdateJasonFile()
    {
       string jasonObject= JsonUtility.ToJson(jasonClass);
        System.IO.File.WriteAllText(Application.dataPath + "/Save.json", jasonObject);
    }
  
}//Class
[System.Serializable]
public struct rowWiseToken
{
    public TurnScript[] rowtoken;
}

