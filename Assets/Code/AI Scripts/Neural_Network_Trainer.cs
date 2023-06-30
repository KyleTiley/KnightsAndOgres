using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neural_Network_Trainer : MonoBehaviour
{
    public Neural_Network.NeuralNetwork neural_Network;
    public MiniMaxAI miniMaxAiScript;
    public EasyAIController easyAI_Controller;

    [SerializeField] int training_Session; //amount of sessions to train


    private void Start()
    {
        int input_Size = 81; //Size of the input layer
        int hidden_Size = 162; //Size of the hidden layer (input + output)
        int output_Size = 81; // Size of the output layer

        neural_Network = new Neural_Network.NeuralNetwork(input_Size, hidden_Size, output_Size);

        //Trains the neural network
        Train_NeuralNetwork(training_Session);

        //Saves the training data
        neural_Network.SaveTrainingData("training_data.txt");

        //This Random Play controller will start the process of the ai placing down the characters on random spots on the board
        AI_Placing_Random();

    }


    //uses the board
    private void Train_NeuralNetwork(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            //Sets the board to empty
            float[] board_State = new float[9];

            //Randomly chooses which player starts in the training
            int choose_Start = Random.Range(0, 2);

            if(choose_Start == 0)
            {
                //PlayTrainingGame_AIStart(board_State);
            }
            else
            {
                //PlayerTrainingGamePLayerStart(board_State);
            }
        }
    }

    private void AI_Placing_Random()
    {
        easyAI_Controller.RandomPlay();

    }

    //void PlayTrainingGame_AIStart(float[] board_State)
    //{
    //    int ai_Move = -2; //Initializes the AIMove Variable

    //    while (!GameWon(board_State, 1) && !GameWon(board_State, -1) && !BoardFull(board_State))
    //    {
    //        // Let's the AI make a move and updates the board after the move
    //        ai_Move = GetBestMove(board_State);
    //        board_State[ai_Move] = 1f;

    //        if (GameWon(board_State, 1) || BoardFull(board_State))
    //        {
    //            break;
    //        }

    //        //Simulates opponent's moves by random selection and updates the board after the move

    //        int opponent_Move = GetRandomValidMove(board_State);
    //        board_State[opponent_Move] = -1f;

    //        if(GameWon(board_State, -1) || BoardFull(board_State))
    //        {
    //            break;
    //        }
    //    }

        //Assign target ouputs for the ai's move
    //    float[] targetOutputs = new float[board_State.Length];
    //    targetOutputs[ai_Move] = 1f;

    //    //Train the player with the current board state and target outputs
    //    neural_Network.Train(board_State, targetOutputs);
    //}

    //void PlayerTrainingGamePLayerStart(float[] board_State)
    //{
    //    int aiMove = -1; //Initalizes the AIMove variable
        
    //    while (!GameWon(board_State, 1) && !GameWon(board_State, -1) && !BoardFull(board_State))
    //    {
    //        //Simulats opponent's move and updates board
    //        int opponentMove = GetRandomValidMove(board_State);
    //        board_State[opponentMove] = -1f;

    //        if(GameWon(board_State, -1) || BoardFull(board_State))
    //        {
    //            break;
    //        }

    //        //Let the Ai make a move and updates board
    //        aiMove = GetBestMove(board_State);
    //        board_State[aiMove] = 1f;

    //        if(GameWon(board_State, 1) || BoardFull(board_State))
    //        {
    //            break;
    //        }

    //    }

        //Assigns the target output for the players move
    //    float[] targetOutputs = new float[board_State.Length];
    //    targetOutputs[aiMove] = 1f;

    //    //trains the player with the current board state and target outputs
    //    neural_Network.Train(board_State, targetOutputs);
    //}

    //private int GetRandomValidMove(float[] board_State) // Creates an index for the player
    //{
    //    int randomIndex;
    //    do
    //    {
    //        randomIndex = Random.Range(0, 9);
    //    }
    //    while (board_State[randomIndex] != 0f);

    //    return randomIndex;
    //}

    //public int GetBestMove(float[] board_State)
    //{
    //    float[] outputs = neural_Network.FeedForward(board_State);

    //    //Finds teh index of the highest output value for a valid move
    //    int maxIndex = -1;
    //    float maxValue = Mathf.Infinity;

    //    for (int i = 0; i < outputs.Length; i++)
    //    {
    //        if(board_State[i] == 0 && outputs[i] > maxValue)
    //        {
    //            maxIndex = i;
    //            maxValue = outputs[i];
    //        }
    //    }

    //    if (maxIndex == -1)
    //    {
    //        Debug.LogError("No valid move found! The AI player cannot make a move");
    //    }

    //    return maxIndex;
    //}

    ////public bool GameWon(bool) //Winning conditions             //This method is to pull the win game method that was made in another script and shows for when the game was won
    ////{
    ////    return miniMaxAiScript.CheckforWin(bool);
    ////}

    //public bool BoardFull(float[] _boardSate)
    //{
    //    for (int i = 0; i < 9; i++)
    //    {
    //        if (_boardSate[i] == 0f)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
}
