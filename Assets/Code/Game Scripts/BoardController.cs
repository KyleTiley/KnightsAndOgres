using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    // REFERENCES
    public List<TileController> tileControllers = new List<TileController>();

    // VARIABLES
    public int tilesPlayed;

    // METHODS
    private void Awake() {
        foreach(Transform child in transform){
            tileControllers.Add(child.GetComponent<TileController>());
        }

        tilesPlayed = 0;
    }

    public void SetBoardWinner(){
        this.GetComponent<Image>().color = Color.green;
    }
}
