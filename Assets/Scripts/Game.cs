using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameObject chesspiece;
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] board = new GameObject[16];

    public AudioSource gameOverSound;

    public GameObject ColumnSuduko;
    public GameObject RowSuduko;

    public int numberOfShipsLeftToFind;

    public void Start()
    {
        // 12 ships to find
        numberOfShipsLeftToFind = 12;

        // Hide the game over message
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = false;
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = false;

        // Setup the board with empty pieces and also hidden ships
        board = new GameObject[] {
            Create("hidden_empty", 0, 0),
            Create("hidden_empty", 1, 0),
            Create("hidden_empty", 2, 0),
            Create("hidden_empty", 3, 0),
            Create("hidden_empty", 4, 0),
            Create("hidden_empty", 5, 0),
            Create("hidden_empty", 6, 0),
            Create("hidden_ship", 7, 0),

            Create("hidden_empty", 0, 1),
            Create("hidden_empty", 1, 1),
            Create("hidden_empty", 2, 1),
            Create("hidden_ship", 3, 1),
            Create("hidden_ship", 4, 1),
            Create("hidden_empty", 5, 1),
            Create("hidden_empty", 6, 1),
            Create("hidden_empty", 7, 1),

            Create("hidden_empty", 0, 2),
            Create("hidden_empty", 1, 2),
            Create("hidden_empty", 2, 2),
            Create("hidden_empty", 3, 2),
            Create("hidden_empty", 4, 2),
            Create("hidden_empty", 5, 2),
            Create("hidden_empty", 6, 2),
            Create("hidden_empty", 7, 2),

            Create("hidden_ship", 0, 3),
            Create("hidden_empty", 1, 3),
            Create("hidden_ship", 2, 3),
            Create("hidden_empty", 3, 3),
            Create("hidden_empty", 4, 3),
            Create("hidden_empty", 5, 3),
            Create("hidden_empty", 6, 3),
            Create("hidden_ship", 7, 3),

            Create("hidden_empty", 0, 4),
            Create("hidden_empty", 1, 4),
            Create("hidden_empty", 2, 4),
            Create("hidden_empty", 3, 4),
            Create("hidden_empty", 4, 4),
            Create("hidden_empty", 5, 4),
            Create("hidden_empty", 6, 4),
            Create("hidden_empty", 7, 4),

            Create("hidden_empty", 0, 5),
            Create("hidden_empty", 1, 5),
            Create("hidden_empty", 2, 5),
            Create("hidden_ship", 3, 5),
            Create("hidden_ship", 4, 5),
            Create("hidden_empty", 5, 5),
            Create("hidden_empty", 6, 5),
            Create("hidden_empty", 7, 5),

            Create("hidden_ship", 0, 6),
            Create("hidden_empty", 1, 6),
            Create("hidden_empty", 2, 6),
            Create("hidden_ship", 3, 6),
            Create("hidden_empty", 4, 6),
            Create("hidden_empty", 5, 6),
            Create("hidden_ship", 6, 6),
            Create("hidden_empty", 7, 6),

            Create("hidden_empty", 0, 7),
            Create("hidden_empty", 1, 7),
            Create("hidden_empty", 2, 7),
            Create("hidden_empty", 3, 7),
            Create("hidden_ship", 4, 7),
            Create("hidden_empty", 5, 7),
            Create("hidden_empty", 6, 7),
            Create("hidden_empty", 7, 7)
        };

        //Set all piece positions on the positions board
        for (int i = 0; i < board.Length; i++)
        {
            SetPosition(board[i]);
        }

        WorkOutSuduko();

        gameOverSound = GameObject.Find("GameOverSound").GetComponent<AudioSource>();
    }

    public GameObject Create(string name, int x, int y)
    {
        // Game setup
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);

        // Give this playing piece a script to run
        PieceManager cm = obj.GetComponent<PieceManager>(); 
        
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);

        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        PieceManager cm = obj.GetComponent<PieceManager>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void IsItGameOver()
    {
        bool stillSomeHiddenShipsLeft = false;

        // see if we have any hidden ships left on the board
        for (int numberOfPlaces = 0; numberOfPlaces < 64; numberOfPlaces++)
        {
            PieceManager somePiece = board[numberOfPlaces].GetComponent<PieceManager>();

            if (somePiece.name == "hidden_ship")
            {
                stillSomeHiddenShipsLeft = true;
            }
        }

        WorkOutSuduko();

        // if there aren't any hidden ships on the board it must be game over
        if (stillSomeHiddenShipsLeft == false)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Show the game over text
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;

        // Play the game over sound
        gameOverSound.Play();
    }


    public void WorkOutSuduko()
    {
        int rowOne = 0;
        int rowTwo = 0; 
        int rowThree = 0; 
        int rowFour = 0; 
        int rowFive = 0; 
        int rowSix = 0; 
        int rowSeven = 0; 
        int rowEight = 0;
        int columnOne = 0;
        int columnTwo = 0; 
        int columnThree = 0; 
        int columnFour = 0; 
        int columnFive = 0; 
        int columnSix = 0; 
        int columnSeven = 0; 
        int columnEight = 0;

        GameObject.Find("ColumnSuduko").GetComponent<Text>().text = "0 0 0 0 0 0 0 0";
        GameObject.Find("RowSuduko").GetComponent<Text>().text = "0\n0\n0\n0\n0\n0\n0\n0";

        // eigth row
        for (int counter = 0; counter < 8; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowEight = rowEight + 1;
            }
        }

        // seventh row
        for (int counter = 8; counter < 16; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowSeven = rowSeven + 1;
            }
        }

        // sixth row
        for (int counter = 16; counter < 24; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowSix = rowSix + 1;
            }
        }

        // fifth row
        for (int counter = 24; counter < 32; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowFive = rowFive + 1;
            }
        }

        // fourth row
        for (int counter = 32; counter < 40; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowFour = rowFour + 1;
            }
        }

        // third row
        for (int counter = 40; counter < 48; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowThree = rowThree + 1;
            }
        }

        // second row
        for (int counter = 48; counter < 56; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowTwo = rowTwo + 1;
            }
        }

        // first row
        for (int counter = 56; counter < 64; counter++)
        {
            PieceManager somePiece = board[counter].GetComponent<PieceManager>();

            // If we find a hidden ship in this place, increase our suduko count for this row
            if (somePiece.name == "hidden_ship")
            {
                rowOne = rowOne + 1;
            }
        }

        GameObject.Find("RowSuduko").GetComponent<Text>().text = rowOne + "\n" + rowTwo + "\n" + rowThree + "\n" + rowFour + "\n" + rowFive + "\n" + rowSix + "\n" + rowSeven + "\n" + rowEight;

        GameObject.Find("ColumnSuduko").GetComponent<Text>().text = columnOne + " " + columnTwo + " " + columnThree +
                                                                    " " + columnFour + " " + columnFive + " " +
                                                                    columnSix + " " + columnSeven + " " + columnEight;
    }

}
