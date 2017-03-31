using System.Collections;
using System.Collections.Generic;
using Thalmic.Myo;
using UnityEngine;

public class Tetramino : MonoBehaviour {

    // Script to handle all features, functions and characteristics of each tetramino

    public float fall = 0;
    public float fallSpeed = 1;
    //public ThalmicMyo thalmicMyo;
    //public GameObject myo = null;
    //private Pose _lastPose = Pose.Unknown;
    public int individualScore = 100;

    public float individualTime;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        CheckUserInput();
        UpdateIndividualScore();
    }

    // Used to calculate score depending on time
    void UpdateIndividualScore()
    {
        if (individualTime < 1)
        {
            individualTime += Time.deltaTime;
        }
        else
        {
            individualTime = 0;
            individualScore = Mathf.Max(individualScore - 5, 0);
        }
    }

    // Checks for keyboard inputs
    void CheckUserInput()
    {
        //Move right once
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
        {
            transform.position += new UnityEngine.Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(-1, 0, 0);
            }
        }
        //Move right continously
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift)  || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)))
        {
            transform.position += new UnityEngine.Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(-1, 0, 0);
            }
        }
        //Move left once
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A)))
        {
            
            transform.position += new UnityEngine.Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(1, 0, 0);
            }
        }
        //Move left continously
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)))
        {

            transform.position += new UnityEngine.Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(1, 0, 0);
            }
        }
        //Rotate clockwise
        else if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W)))
        {
            transform.Rotate(0, 0, 90);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, -90);
            }
        }
        //Rotate counter-clockwise
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S)))
        {
            transform.Rotate(0, 0, -90);
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.Rotate(0, 0, 90);
            }
        }
        //Speed up
        else if (Input.GetKeyDown(KeyCode.Space) || Time.time - fall >= fallSpeed)
        {
            transform.position += new UnityEngine.Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(0, 1, 0);

                FindObjectOfType<Game>().DeleteRow();

                if(FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }

                

                FindObjectOfType<Game>().SpawnNextTetromino();

                Game.highscore += individualScore;

                enabled = false;
            }

            fall = Time.time;
        }
        //Move down continously
        else if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.LeftShift) || Time.time - fall >= fallSpeed)
        {
            transform.position += new UnityEngine.Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(0, 1, 0);

                FindObjectOfType<Game>().DeleteRow();

                if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }



                FindObjectOfType<Game>().SpawnNextTetromino();

                Game.highscore += individualScore;

                enabled = false;
            }

            fall = Time.time;
        }

        //Hold Piece
        else if (Input.GetKey(KeyCode.Space) || Time.time - fall >= fallSpeed)
        {
            transform.position += new UnityEngine.Vector3(0, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new UnityEngine.Vector3(0, 0, 0);

                FindObjectOfType<Game>().DeleteRow();

                enabled = false;

                FindObjectOfType<Game>().SpawnNextTetromino();
            }

            fall = Time.time;
        }
    }
    // Used to make sure a piece can move into a certain place
    public bool CheckIsValidPosition() 
        {
            foreach (Transform mino in transform)
            {
                Vector2 pos = FindObjectOfType<Game>().Round(mino.position);

                if(FindObjectOfType<Game>().CheckIsInsideGrid (pos) == false)
                {
                    return false;
                }

                if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
                {
                    return false;
                }
             }
        return true;
    }
    // Method that will be called by MYO for same functionality as keyboard inputs
    // Move tetramino right once
    public void MoveRightOnce()
    {
        transform.position += new UnityEngine.Vector3(1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new UnityEngine.Vector3(-1, 0, 0);
        }
    }
    // Method that will be called by MYO for same functionality as keyboard inputs
    // Mobe tetramino left once
    public void MoveLeftOnce()
    {
        transform.position += new UnityEngine.Vector3(-1, 0, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new UnityEngine.Vector3(1, 0, 0);
        }
    }
    // Method that will be called by MYO for same functionality as keyboard inputs
    // Rotate tetramino 90 degrees
    public void RotateClockwise()
    {
        transform.Rotate(0, 0, 90);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {
            transform.Rotate(0, 0, -90);
        }
    }
    // Method that will be called by MYO for same functionality as keyboard inputs
    // Rotate tetramino -90 degrees
    public void RotateCounterClockwise()
    {
        transform.Rotate(0, 0, -90);
        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {

        }
    }
    // Method that will be called by MYO for same functionality as keyboard inputs
    // Moves tetraminos down 1 space faster
    public void SpeedUp()
    {

        transform.position += new UnityEngine.Vector3(0, -1, 0);

        if (CheckIsValidPosition())
        {
            FindObjectOfType<Game>().UpdateGrid(this);
        }
        else
        {
            transform.position += new UnityEngine.Vector3(0, 1, 0);

            FindObjectOfType<Game>().DeleteRow();

            if (FindObjectOfType<Game>().CheckIsAboveGrid(this))
            {
                FindObjectOfType<Game>().GameOver();
            }



            FindObjectOfType<Game>().SpawnNextTetromino();

            Game.highscore += individualScore;

            enabled = false;
        }

        fall = Time.time;
    }
}
