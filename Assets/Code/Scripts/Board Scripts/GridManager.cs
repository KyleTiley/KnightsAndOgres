using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int gridWidth = 9; 
    private int gridHeight = 9;
    private float gridGap = 1.0f;

    [SerializeField] private Vector3 gridStartPos;

    [SerializeField] private GameObject tile;

    public string playerTurn;

    private void Start() {
        playerTurn = "player1";
        GenerateGrid();
    }

    private void GenerateGrid(){
        float nextTileX = gridStartPos.x;
        float nextTileY = gridStartPos.y;

        for(float width = 0; width < gridWidth; width++){

            nextTileX = gridStartPos.x;

            for(float height = 0; height <gridHeight; height++){
                var spawnedTile = Instantiate(tile, new Vector3(nextTileX, nextTileY), Quaternion.identity);
                spawnedTile.name = $"Tile {width} {height}";

                if((height+1)%3 == 0){
                    nextTileX += gridGap/4;
                }
                nextTileX += gridGap;
            }
            
            if((width+1)%3 == 0){
                nextTileY+=gridGap/4;
            }
            nextTileY += gridGap;
        }
    }
}
