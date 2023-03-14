using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    GameObject gridManager;
    GridManager newGridManager;

    [SerializeField] private GameObject _highlight;

    bool notClicked = true;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        newGridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
    }

    private void OnMouseEnter(){
        if(notClicked){
            _highlight.SetActive(true);
        }
    }

    private void OnMouseExit(){
        _highlight.SetActive(false);
    }

    private void OnMouseDown(){
        if(notClicked){
            if(newGridManager.playerTurn == "player1"){
                spriteRenderer.color = Color.blue;
                newGridManager.playerTurn = "player2";
            }
            else if(newGridManager.playerTurn == "player2"){
                spriteRenderer.color = Color.red;
                newGridManager.playerTurn = "player1";
            }
            else{
                // pray for no exceptions
            }
            _highlight.SetActive(false);
            notClicked = false;
        }
    }
}
