using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject gameBoard;
    public Sprite[] images;
    public bool unplayed = true;
    public int spriteIndex;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameBoard = GameObject.Find("GameBoard");
    }


    private void Start()
    {
        spriteRenderer.sprite = null;
    }

   

    

    private void OnMouseDown()
    {
        if (unplayed && gameBoard.GetComponent<GameScript>().isPlaying )
        {
            int index = gameBoard.GetComponent<GameScript>().PlayerTurn();
            spriteRenderer.sprite = images[index];
            spriteIndex = index;
            unplayed = false;
            gameBoard.GetComponent<GameScript>().CheckGameStatus(index);
           
        }
    }


}//class

