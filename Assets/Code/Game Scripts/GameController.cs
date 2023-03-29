using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // REFERENCES FOR GLOBAL ACCESS
    public static GameController Instance { get; private set; }
    public TurnController turnController;
    public List<BoardController> boardControllers = new List<BoardController>();

    // ?? check if this is needed
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
}
