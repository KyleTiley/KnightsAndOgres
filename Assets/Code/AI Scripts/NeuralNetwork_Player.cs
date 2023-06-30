using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetwork_Player : MonoBehaviour
{
    public static NeuralNetwork_Player instance;

    public Neural_Network.NeuralNetwork neural_Network;

    public EasyAIController easyAI_Controller;

    [SerializeField] float[] game_Board;

    //[SerializeField] private string trainingDataFileName = " name ";

    private void Awake()
    {
        instance = this;

        int input_Size = 9; //Size of the input layer
        int hidden_Size = 18; //Size of hidden layer
        int output_Size = 9; //Size of the output layer

        neural_Network = new Neural_Network.NeuralNetwork(input_Size, hidden_Size, output_Size);

        //Load the training data
        neural_Network.LoadTrainingData("Assets/Training");

    }




    public void GetAI_Move()
    {
        //get the current board state
        

        //make a move baised on the output of the neural network
        int move = GetBestMove(game_Board);

        //Send info to place the characters randomly on the board
        easyAI_Controller.RandomPlay();


    }

    private int GetBestMove (float[] board_State)
    {
        //Choose the move with the hoghest output value
        float[] output = neural_Network.FeedForward(board_State);

        //Find the index of the highest output value for a valid move
        int max_Index = -1;
        float max_Value = -Mathf.Infinity;

        for(int i = 0; i < output.Length; i++)
        {
            if(board_State[i] == 0 && output[i] > max_Value)
            {
                max_Index = i;
                max_Value = output[i];
            }
        }

        if(max_Index == -1)
        {
            Debug.LogError("No valid move found / AI can't make a move");
        }
        return max_Index;
    }
}
