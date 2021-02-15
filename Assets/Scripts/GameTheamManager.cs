using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTheamManager : MonoBehaviour
{

    private DictionaryScript dictionaryScript;
    public SpriteRenderer gameBoardSpritRenderer;
    public GameObject backGroundObject;
    public bool changeTheam;
    private string theamKey ="";
    // Start is called before the first frame update
    void Awake()
    {
        dictionaryScript = GetComponent<DictionaryScript>();
       
        if (dictionaryScript.MyDictionary.Count > 0)
        {
            foreach (var pair in dictionaryScript.MyDictionary)
            {
                theamKey = pair.Key;
               // Debug.Log("Theam Changed-> Key: " + pair.Key + " Value: " + pair.Value.backGroundColor);
                ChangeCurrentTheam(dictionaryScript.MyDictionary[pair.Key]);
                break;
            }
           
        }
        else
        {

        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (changeTheam)
        {
            changeTheam = false;
            foreach (var pair in dictionaryScript.MyDictionary)
            {
                if(pair.Key != theamKey)
                {
                    theamKey = pair.Key;
                    //Debug.Log("Theam Changed-> Key: " + pair.Key + " Value: " + pair.Value.backGroundColor);
                    ChangeCurrentTheam(dictionaryScript.MyDictionary[pair.Key]);
                    break;
                }
                
            }


        }
    }

    private void ChangeCurrentTheam(TheamData theamData)
    {
        if (gameBoardSpritRenderer == null || backGroundObject == null)
            return;
        gameBoardSpritRenderer.color = theamData.boardColor;
     
        backGroundObject.GetComponent<Image>().color = theamData.backGroundColor;
    }



    /// <summary>
    /// Sets color to default color
    /// </summary>
    private void SetTheamToDefault()
    {
        if (gameBoardSpritRenderer == null || backGroundObject == null)
            return;
        gameBoardSpritRenderer.color = Color.cyan;
        backGroundObject.GetComponent<Image>().color = Color.cyan;
    }


}
