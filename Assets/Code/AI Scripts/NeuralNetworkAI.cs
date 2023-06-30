using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NeuralNetworkAI : AIController
{
    // I COULD NOT FINISH THE NEURAL NETWORK IN TIME
    // SO I COMMENTED IT OUT AND USE A RANDOM PLAY FUNCTION INSTEAD
    // JUST SO THE TURN IS AT LEAST PLAYED WHEN THE NEURAL NETWORK AI IS CALLED

    public void NeuralNetwork(){
        // Only collects tiles, since easy AI does not use minimax and rather chooses a random tile
        CollectAvailableTiles();

        // Randomly chooses the tile to play
        int tileToPlay;
        tileToPlay = Random.Range(0, availableTiles.Count);
        availableTiles[tileToPlay].OnTileClick();

        //make a move baised on the output of the neural network
        int move = GetBestMove(game_Board);
    }


    [SerializeField] float[] game_Board;

    //[SerializeField] private string trainingDataFileName = " name ";

    private void Awake()
    {
        //Load the training data
        LoadTrainingData("Assets/Training");
    }

    private void Start()
    {
        //Trains the neural network
        Train_NeuralNetwork(training_Session);

        //Saves the training data
        SaveTrainingData("training_data.txt");
    }

    private int GetBestMove (float[] board_State)
    {
        //Choose the move with the hoghest output value
        float[] output = FeedForward(board_State);

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

    private void TrainNetwork(){

    }

    public MiniMaxAI miniMaxAiScript;

    [SerializeField] int training_Session; //amount of sessions to train


    


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

    private int input_Size; //Size of the input layer / Amount of Inputs

        private int hidden_Size; //Size of the hidden layer (input + output sizes of the game / amount of nodes)

        private int output_Size; //Size of the output layer / Amount of outputs

        private float[,] weights_InputHidden;

        private float[,] weights_OutputHidden;

        private float[] biasesHidden;

        private float[] biasesOutput;

        private float learning_Rate = 0.01f; //Default learning rate but 0.01 and 0.1 are most used and recomended as a staring point

        public void CreateNetwork(int _inputSize, int _hiddenSize, int _outputSize) //Constructor for the Neural Network
        {
            input_Size = _inputSize;
            hidden_Size = _hiddenSize;
            output_Size = _outputSize;

            weights_InputHidden = new float[input_Size, hidden_Size];
            weights_OutputHidden = new float[hidden_Size, output_Size];
            biasesHidden = new float[hidden_Size];
            biasesOutput = new float[output_Size];

            InitializaeWeightsandBiases();
        }

        private void InitializaeWeightsandBiases() //Randomly sets values for all the spots in the array
        {
            //Initialize the weights with random values
            for (int i = 0; i < input_Size; i++)
            {
                for (int j = 0; j < hidden_Size; j++)
                {
                    weights_InputHidden[i, j] = Random.Range(-1f, 1f);
                }
            }

            for (int i = 0; i < hidden_Size; i++)
            {
                for (int j = 0; j < output_Size; j++)
                {
                    weights_OutputHidden[i, j] = Random.Range(-1f, 1f);
                }
            }

            //Initialize biases with random values
            for (int i = 0; i < hidden_Size; i++)
            {
                biasesHidden[i] = Random.Range(-0.5f, 0.5f);
            }

            for (int i = 0; i < output_Size; i++)
            {
                biasesOutput[i] = Random.Range(-0.5f, 0.5f);
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            float[] hiddenLayer = new float[hidden_Size];
            float[] outputLayer = new float[output_Size];

            // Calculates the values of the hidden Layer
            for (int i = 0; i < hidden_Size; i++)
            {
                float weighted_Sum = 0f;
                for (int j = 0; j < input_Size; j++)
                {
                    weighted_Sum += inputs[j] * weights_InputHidden[j, i];
                }
                hiddenLayer[i] = Sigmoid(weighted_Sum + biasesHidden[i]);
            }
            
            //Calculates the values of the output layer
            for (int i = 0; i < output_Size; i++)
            {
                float weighted_Sum = 0f;
                for (int j = 0; j < hidden_Size; j++)
                {
                    weighted_Sum += hiddenLayer[j] * weights_OutputHidden[j, i];
                }
                outputLayer[i] = Sigmoid(weighted_Sum + biasesOutput[i]);
            }

            return outputLayer;
        }

        //Activation Method
        private float Sigmoid(float x)
        {
            return 1f / (1f + Mathf.Exp(-x));
        }


        public void Train(float[] inputs, float[] target_Outputs) //Backpropagation
        {
            float[] hiddenLayer = new float[hidden_Size];
            float[] outputLayer = new float[output_Size];

            //calculates the values of the hidden layer
            for (int i = 0; i < hidden_Size; i++)
            {
                float weighted_Sum = 0f;
                for (int j = 0; j < input_Size; j++)
                {
                    weighted_Sum += inputs[j] * weights_InputHidden[j, i];
                }
                hiddenLayer[i] = Sigmoid(weighted_Sum + biasesHidden[i]);
            }

            //calculates the values of the output layer
            for (int i = 0; i < output_Size; i++)
            {
                float weighted_Sum = 0f;
                for (int j = 0; j < hidden_Size; j++)
                {
                    weighted_Sum += hiddenLayer[j] * weights_OutputHidden[j, i];
                }
                outputLayer[i] = Sigmoid(weighted_Sum + biasesOutput[i]);
            }

            //Where the backpropagation begins
            float[] output_Errors = new float[output_Size];
            for (int i = 0; i < output_Size; i++)
            {
                output_Errors[i] = target_Outputs[i] - outputLayer[i];
            }

            float[] hiddenErrors = new float[hidden_Size];
            for (int i = 0; i < hidden_Size; i++)
            {
                float error = 0f;
                for (int j = 0; j < output_Size; j++)
                {
                    error += output_Errors[j] * weights_OutputHidden[i, j];
                }
                hiddenErrors[i] = error;
            }

            //Update weights and biases
            for (int i = 0; i < hidden_Size; i++)
            {
                for (int j = 0; j < output_Size; j++)
                {
                    weights_OutputHidden[i, j] += learning_Rate * output_Errors[j] * hiddenLayer[i];
                }
            }

            for (int i = 0; i < input_Size; i++)
            {
                for (int j = 0; j < hidden_Size; j++)
                {
                    weights_InputHidden[i, j] += learning_Rate * hiddenErrors[j] * inputs[i];
                }
            }

            for (int i = 0; i < output_Size; i++)
            {
                biasesOutput[i] += learning_Rate * output_Errors[i];
            }

            for (int i = 0; i < hidden_Size; i++)
            {
                biasesHidden[i] += learning_Rate * hiddenErrors[i];
            }
        }

        //Save to a text file
        /*This code writes down basiicaly all of the ai's movements*/
        public void SaveTrainingData(string file_Name)                               
        {
            string folder_Path = "Assets/TrainingData";
            string file_Path = Path.Combine(folder_Path, file_Name);

            //Creates the directory if the file doesn't exist
            if (!Directory.Exists(folder_Path))
            {
                Directory.CreateDirectory(folder_Path);
            }

            using (StreamWriter writer = new StreamWriter(file_Path))
            {
                //Writes the weights_InputHidden
                for (int i = 0; i < weights_InputHidden.GetLength(0); i++)
                {
                    for (int j = 0; j < weights_InputHidden.GetLength(1); j++)
                    {
                        writer.Write(weights_InputHidden[i, j] + " / ");
                    }
                    writer.WriteLine();
                }

                //Writes the weigths_OutputHidden
                for (int i = 0; i < weights_OutputHidden.GetLength(0); i++)
                {
                    for (int j = 0; j < weights_OutputHidden.GetLength(1); j++)
                    {
                        writer.Write(weights_OutputHidden[i, j] + " | ");
                    }
                    writer.WriteLine();
                }

                //Writes the biasesHidden
                for (int i = 0; i < biasesHidden.Length; i++)
                {
                    writer.Write(biasesHidden[i] + " / ");
                }
                writer.WriteLine();


                //Writes the biasesOutput
                for (int i = 0; i < biasesOutput.Length; i++)
                {
                    writer.Write(biasesOutput[i] + " | ");
                }
                writer.WriteLine();
            }
            Debug.Log("Training data saved to: " + file_Path);
        }

        //To load the taining data of the game
        /*This code uses the training data file that was written using the writing data function and laods it for the ai so it knows what moves are best to do*/
        public void LoadTrainingData(string file_Name)
        {
            string folder_Path = "Assets/TrainingData";
            string file_Path = Path.Combine(folder_Path, file_Name);

            if (File.Exists(file_Path))
            {
                using (StreamReader reader = new StreamReader(file_Path))
                {
                    //Reads the weigths_InputHidden
                    for (int i = 0; i < weights_InputHidden.GetLength(0); i++)
                    {
                        string[] weights = reader.ReadLine().Split(" / ");
                        for (int j = 0; j < weights_InputHidden.GetLength(1); j++)
                        {
                            float weight = float.Parse(weights[j]);
                            weights_InputHidden[i, j] = weight;
                        }
                    }
                    // Read the weights_OutputHidden
                    for (int i = 0; i < weights_OutputHidden.GetLength(0); i++)
                    {
                        string[] weights = reader.ReadLine().Split(" | ");
                        for (int j = 0; j < weights_OutputHidden.GetLength(1); j++)
                        {
                            float weight = float.Parse(weights[j]);
                            weights_OutputHidden[i, j] = weight;
                        }
                    }

                    //Read the biasesHidden
                    string[] biasHidden_Values = reader.ReadLine().Split(" / ");
                    for (int i = 0; i < biasesHidden.Length; i++)
                    {
                        float bias = float.Parse(biasHidden_Values[i]);
                        biasesHidden[i] = bias;
                    }

                    //Read the biasOutput
                    string[] biasOutput_Values = reader.ReadLine().Split(" | ");
                    for (int i = 0; i < biasesOutput.Length; i++)
                    {
                        float bias = float.Parse(biasOutput_Values[i]);
                        biasesOutput[i] = bias;
                    }
                }

                Debug.Log("Training data loaded from: " + file_Path);
            }
            else
            {
                Debug.LogError("Training data file not found");
            }
        }
}
