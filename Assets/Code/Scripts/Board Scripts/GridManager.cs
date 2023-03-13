using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridHeight;

    [SerializeField] private GameObject tile;

    private void Start() {
        GenerateGrid();
    }

    private void GenerateGrid(){
        for(int width = 0; width < gridWidth; width++){
            for(int height = 0; height <gridHeight; height++){
                var spawnedTile = Instantiate(tile, new Vector3(width, height), Quaternion.identity);
                spawnedTile.name = $"Tile {width} {height}";
            }
        }
    }
}
