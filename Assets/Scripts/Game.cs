using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    // Pixel Width
    public static int gridWidth = 10;
    // Pixl Height
    public static int gridHeight = 20;
    // Initial score
    public static int highscore = 0;

    
    // This allows us to map the score to an object in Unity
    public Text hud_score;
    // 
    public int numberOfRowsThisTurn = 0;
    // Sets the grid height and width
    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
    // Continously updates the UI of the game with the score as it goes up
    public void UpdateUI()
    {
        hud_score.text = highscore.ToString();
    }
    // Method for how the score will be updated
    public void UpdateScore()
    {
        if (numberOfRowsThisTurn > 0)
        {
            if (numberOfRowsThisTurn == 1)
            {
                // 1 Row equals 100 points
                highscore += 100;
            }
            else if (numberOfRowsThisTurn == 2)
            {
                // 2 rows equals 200 points plus a 50% points bonus totalling 300 points (100 bonus points)
                highscore += 300;
            }
            else if (numberOfRowsThisTurn == 3)
            {
                // 3 rows equals 300 points plus a 75% points bonus totalling 525 points (225 bonus points)
                highscore += 525;
            }
            else if(numberOfRowsThisTurn == 4)
            {
                // 4 rowsequals 400 points plus a 100% points bonus for achieveing a Tetris totalling 800 points (400 bonus points)
                highscore += 800;
            }
            // Reseting numberOfRowsThisTurn so that scoring will not continously go up when the player has not actually
            // done anything to deserve those points
            numberOfRowsThisTurn = 0;
        }
    }

	// Use this for initialization
	void Start () {
        // This initially spawns a tetramino when the game is started
        // Call the method we will continue using for spawning tetraminos
        SpawnNextTetromino();
        
	}

    void Update()
    {
        // Continously updates the score with points
        UpdateScore();
        // Continously updates the UI with the new score
        UpdateUI();
    }

    public bool CheckIsAboveGrid(Tetramino tetramino)
    {
        for (int x = 0; x <gridWidth; ++x)
        {
            foreach(Transform mino in tetramino.transform)
            {
                Vector2 pos = Round(mino.position);

                if (pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Method for tetramino spawning
    public void SpawnNextTetromino()
    {
        GameObject nextTetramino = (GameObject)Instantiate(Resources.Load(GetRandomTetramino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    }
	
    public bool IsFullRowAt (int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x,y] == null)
            {
                return false;
            }
        }
        numberOfRowsThisTurn++;

        return true;
    }


    public void DeleteMinoAt (int y)
    {
        for (int x=0; x <gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }

    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void MoveAllRowsDown (int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
        }
    }

    public void DeleteRow ()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
                --y;
            }
        }
    }

    public void UpdateGrid (Tetramino tetramino)
    {
        for(int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x,y] != null)
                {
                    if(grid[x,y].parent == tetramino.transform)
                    {
                        grid[x,y] = null;
                    }
                }
            }
        }

        foreach (Transform mino in tetramino.transform)
        {
            Vector2 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }

    public Transform GetTransformAtGridPosition(Vector2 pos)
    {
        if (pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }

    public bool CheckIsInsideGrid(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }

    // This is the method that decides both what type of tetramino will spawn and what colour it will be
    string GetRandomTetramino()
    {
        int RandomTetramino = Random.Range(1, 11);
        string RandomTetraminoName = "Prefabs/BlockJ";

        if (RandomTetramino == 1)
        {
            RandomTetraminoName = "Prefabs/BlockJ";
            int RandomTetraminoJ = Random.Range(1, 10);
            string RandomTetraminoNameJ = "Prefabs/BlockJ";

            switch (RandomTetraminoJ)
            {
                case 1:
                    RandomTetraminoNameJ = "Prefabs/BlockJ";
                    break;
                case 2:
                    RandomTetraminoNameJ = "Prefabs/BlockJ1";
                    break;
                case 3:
                    RandomTetraminoNameJ = "Prefabs/BlockJ2";
                    break;
                case 4:
                    RandomTetraminoNameJ = "Prefabs/BlockJ3";
                    break;
                case 5:
                    RandomTetraminoNameJ = "Prefabs/BlockJ4";
                    break;
                case 6:
                    RandomTetraminoNameJ = "Prefabs/BlockJ5";
                    break;
                case 7:
                    RandomTetraminoNameJ = "Prefabs/BlockJ6";
                    break;
                case 8:
                    RandomTetraminoNameJ = "Prefabs/BlockJ7";
                    break;
                case 9:
                    RandomTetraminoNameJ = "Prefabs/BlockJ8";
                    break;
                    
            }
            return RandomTetraminoNameJ;
        }
        else if (RandomTetramino == 2)
        {
            RandomTetraminoName = "Prefabs/BlockL";
            int RandomTetraminoL = Random.Range(1, 10);
            string RandomTetraminoNameL = "Prefabs/BlockL";

            switch (RandomTetraminoL)
            {
                case 1:
                    RandomTetraminoNameL = "Prefabs/BlockL";
                    break;
                case 2:
                    RandomTetraminoNameL = "Prefabs/BlockL1";
                    break;
                case 3:
                    RandomTetraminoNameL = "Prefabs/BlockL2";
                    break;
                case 4:
                    RandomTetraminoNameL = "Prefabs/BlockL3";
                    break;
                case 5:
                    RandomTetraminoNameL = "Prefabs/BlockL4";
                    break;
                case 6:
                    RandomTetraminoNameL = "Prefabs/BlockL5";
                    break;
                case 7:
                    RandomTetraminoNameL = "Prefabs/BlockL6";
                    break;
                case 8:
                    RandomTetraminoNameL = "Prefabs/BlockL7";
                    break;
                case 9:
                    RandomTetraminoNameL = "Prefabs/BlockL8";
                    break;

            }
            return RandomTetraminoNameL;
        }
        else if (RandomTetramino == 3)
        {
            RandomTetraminoName = "Prefabs/BlockS";
            int RandomTetraminoS = Random.Range(1, 10);
            string RandomTetraminoNameS = "Prefabs/BlockS";

            switch (RandomTetraminoS)
            {
                case 1:
                    RandomTetraminoNameS = "Prefabs/BlockS";
                    break;
                case 2:
                    RandomTetraminoNameS = "Prefabs/BlockS1";
                    break;
                case 3:
                    RandomTetraminoNameS = "Prefabs/BlockS2";
                    break;
                case 4:
                    RandomTetraminoNameS = "Prefabs/BlockS3";
                    break;
                case 5:
                    RandomTetraminoNameS = "Prefabs/BlockS4";
                    break;
                case 6:
                    RandomTetraminoNameS = "Prefabs/BlockS5";
                    break;
                case 7:
                    RandomTetraminoNameS = "Prefabs/BlockS6";
                    break;
                case 8:
                    RandomTetraminoNameS = "Prefabs/BlockS7";
                    break;
                case 9:
                    RandomTetraminoNameS = "Prefabs/BlockS8";
                    break;

            }
            return RandomTetraminoNameS;
        }
        else if (RandomTetramino == 4)
        {
            RandomTetraminoName = "Prefabs/BlockT";
            int RandomTetraminoT = Random.Range(1, 10);
            string RandomTetraminoNameT = "Prefabs/BlockT";

            switch (RandomTetraminoT)
            {
                case 1:
                    RandomTetraminoNameT = "Prefabs/BlockT";
                    break;
                case 2:
                    RandomTetraminoNameT = "Prefabs/BlockT1";
                    break;
                case 3:
                    RandomTetraminoNameT = "Prefabs/BlockT2";
                    break;
                case 4:
                    RandomTetraminoNameT = "Prefabs/BlockT3";
                    break;
                case 5:
                    RandomTetraminoNameT = "Prefabs/BlockT4";
                    break;
                case 6:
                    RandomTetraminoNameT = "Prefabs/BlockT5";
                    break;
                case 7:
                    RandomTetraminoNameT = "Prefabs/BlockT6";
                    break;
                case 8:
                    RandomTetraminoNameT = "Prefabs/BlockT7";
                    break;
                case 29:
                    RandomTetraminoNameT = "Prefabs/BlockT8";
                    break;

            }
            return RandomTetraminoNameT;
        }
        else if (RandomTetramino == 5)
        {
            RandomTetraminoName = "Prefabs/BlockN";
            int RandomTetraminoN = Random.Range(1, 10);
            string RandomTetraminoNameN = "Prefabs/BlockN";

            switch (RandomTetraminoN)
            {
                case 1:
                    RandomTetraminoNameN = "Prefabs/BlockN";
                    break;
                case 2:
                    RandomTetraminoNameN = "Prefabs/BlockN1";
                    break;
                case 3:
                    RandomTetraminoNameN = "Prefabs/BlockN2";
                    break;
                case 4:
                    RandomTetraminoNameN = "Prefabs/BlockN3";
                    break;
                case 5:
                    RandomTetraminoNameN = "Prefabs/BlockN4";
                    break;
                case 6:
                    RandomTetraminoNameN = "Prefabs/BlockN5";
                    break;
                case 7:
                    RandomTetraminoNameN = "Prefabs/BlockN6";
                    break;
                case 8:
                    RandomTetraminoNameN = "Prefabs/BlockN7";
                    break;
                case 9:
                    RandomTetraminoNameN = "Prefabs/BlockN8";
                    break;

            }
            return RandomTetraminoNameN;
        }
        else if (RandomTetramino == 6)
        {
            RandomTetraminoName = "Prefabs/BlockZ";
            int RandomTetraminoZ = Random.Range(1, 10);
            string RandomTetraminoNameZ = "Prefabs/BlockZ";

            switch (RandomTetraminoZ)
            {
                case 1:
                    RandomTetraminoNameZ = "Prefabs/BlockZ";
                    break;
                case 2:
                    RandomTetraminoNameZ = "Prefabs/BlockZ1";
                    break;
                case 3:
                    RandomTetraminoNameZ = "Prefabs/BlockZ2";
                    break;
                case 4:
                    RandomTetraminoNameZ = "Prefabs/BlockZ3";
                    break;
                case 5:
                    RandomTetraminoNameZ = "Prefabs/BlockZ4";
                    break;
                case 6:
                    RandomTetraminoNameZ = "Prefabs/BlockZ5";
                    break;
                case 7:
                    RandomTetraminoNameZ = "Prefabs/BlockZ6";
                    break;
                case 8:
                    RandomTetraminoNameZ = "Prefabs/BlockZ7";
                    break;
                case 9:
                    RandomTetraminoNameZ = "Prefabs/BlockZ8";
                    break;

            }
            return RandomTetraminoNameZ;
        }
        else if (RandomTetramino == 7)
        {
            RandomTetraminoName = "Prefabs/BlockSideT";
            int RandomTetraminoSideT = Random.Range(1, 10);
            string RandomTetraminoNameSideT = "Prefabs/BlockSideT";

            switch (RandomTetraminoSideT)
            {
                case 1:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT";
                    break;
                case 2:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT1";
                    break;
                case 3:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT2";
                    break;
                case 4:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT3";
                    break;
                case 5:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT4";
                    break;
                case 6:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT5";
                    break;
                case 7:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT6";
                    break;
                case 8:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT7";
                    break;
                case 9:
                    RandomTetraminoNameSideT = "Prefabs/BlockSideT8";
                    break;

            }
            return RandomTetraminoNameSideT;
        }
        else if (RandomTetramino == 8)
        {
            RandomTetraminoName = "Prefabs/BlockTiltT";
            int RandomTetraminoTiltT = Random.Range(1, 10);
            string RandomTetraminoNameTiltT = "Prefabs/BlockTiltT";

            switch (RandomTetraminoTiltT)
            {
                case 1:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT";
                    break;
                case 2:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT1";
                    break;
                case 3:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT2";
                    break;
                case 4:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT3";
                    break;
                case 5:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT4";
                    break;
                case 6:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT5";
                    break;
                case 7:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT6";
                    break;
                case 8:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT7";
                    break;
                case 9:
                    RandomTetraminoNameTiltT = "Prefabs/BlockTiltT8";
                    break;

            }
            return RandomTetraminoNameTiltT;
        }
        else if (RandomTetramino == 9)
        {
            RandomTetraminoName = "Prefabs/BlockLine";
            int RandomTetraminoLine = Random.Range(1, 10);
            string RandomTetraminoNameLine = "Prefabs/BlockLine";

            switch (RandomTetraminoLine)
            {
                case 1:
                    RandomTetraminoNameLine = "Prefabs/BlockLine";
                    break;
                case 2:
                    RandomTetraminoNameLine = "Prefabs/BlockLine1";
                    break;
                case 3:
                    RandomTetraminoNameLine = "Prefabs/BlockLine2";
                    break;
                case 4:
                    RandomTetraminoNameLine = "Prefabs/BlockLine3";
                    break;
                case 5:
                    RandomTetraminoNameLine = "Prefabs/BlockLine4";
                    break;
                case 6:
                    RandomTetraminoNameLine = "Prefabs/BlockLine5";
                    break;
                case 7:
                    RandomTetraminoNameLine = "Prefabs/BlockLine6";
                    break;
                case 8:
                    RandomTetraminoNameLine = "Prefabs/BlockLine7";
                    break;
                case 9:
                    RandomTetraminoNameLine = "Prefabs/BlockLine8";
                    break;

            }
            return RandomTetraminoNameLine;
        }
        else if (RandomTetramino == 10)
        {
            RandomTetraminoName = "Prefabs/BlockSquare";
            int RandomTetraminoSquare = Random.Range(1, 10);
            string RandomTetraminoNameSquare = "Prefabs/BlockSquare";

            switch (RandomTetraminoSquare)
            {
                case 1:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare";
                    break;
                case 2:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare1";
                    break;
                case 3:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare2";
                    break;
                case 4:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare3";
                    break;
                case 5:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare4";
                    break;
                case 6:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare5";
                    break;
                case 7:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare6";
                    break;
                case 8:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare7";
                    break;
                case 9:
                    RandomTetraminoNameSquare = "Prefabs/BlockSquare8";
                    break;

            }
            return RandomTetraminoNameSquare;
        }
        return RandomTetraminoName;
    }

    // Called when Tetraminos fill up the whole grid and no more can be placed
    public void GameOver()
    {
        // Writes to a global variable so that any scene can display this score
        Globalclass.globalHighscore = highscore;
        // Load into the GameOver scene which displays the final score and asks the player if they want to play again?
        // Which reloads the Level scene
        Application.LoadLevel("GameOver");
    }
}



