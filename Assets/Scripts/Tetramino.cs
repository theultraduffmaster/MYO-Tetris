using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour {

    float fall = 0;
    public float fallSpeed = 1;

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

    void CheckUserInput()
    {
        //Move right once
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D)))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        //Move right continously
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftShift)  || (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        //Move left once
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A)))
        {
            
            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        //Move left continously
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.LeftShift) || (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)))
        {

            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
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
            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

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
            transform.position += new Vector3(0, -1, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);

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
            transform.position += new Vector3(0, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().UpdateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 0, 0);

                FindObjectOfType<Game>().DeleteRow();

                enabled = false;

                FindObjectOfType<Game>().SpawnNextTetromino();
            }

            fall = Time.time;
        }
    }

    bool CheckIsValidPosition() 
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

}
