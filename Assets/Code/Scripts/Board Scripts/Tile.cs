using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;

    private void OnMouseEnter(){
        _highlight.SetActive(true);
    }

    private void OnMouseExit(){
        _highlight.SetActive(false);
    }
}
