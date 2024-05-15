using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool singleton = true;
    private static ApplicationManager instance = null;

    public int choice = 0;
    public int winner = 0;
    public int player1 = 0;
    public int player2 = 0;
    public int player3 = 0;
    public int maxPoints = 0;

    public int cntWinner = 0;
    public int cntPlayer1 = 0;
    public int cntPlayer2 = 0;
    public int cntPlayer3 = 0;
    public int cntGames = 0;

    private List<Vector3> pos = new();

    private int currentPos;
    private const int LEFT = 0;
    private const int CENTER = 1;
    private const int RIGHT = 2;

    private List<GameObject> players = new();

    private bool FirstStats = false;

    private void Awake()
    {
        // Only one instance of applicationManager is allowed
        if (instance == null)
        {
            instance = this;

            // If it is a singleton object, don't destroy it between scene changes
            if (singleton)
                DontDestroyOnLoad(this.gameObject);
        }
        else if (this != instance)
        {
            Destroy(gameObject);
            return;
        }
    }


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ShowStartStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            CheckInput();
        }
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    public void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            int newPos = 0;
            switch (currentPos)
            {
                case CENTER:
                    newPos = LEFT;
                    break;
                case RIGHT:
                    newPos = CENTER;
                    break;
                case LEFT:
                    newPos = RIGHT;
                    break;
            }
            ChangePosition(currentPos, newPos);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            int newPos = 0;
            switch (currentPos)
            {
                case CENTER:
                    newPos = RIGHT;
                    break;
                case RIGHT:
                    newPos = LEFT;
                    break;
                case LEFT:
                    newPos = CENTER;
                    break;
            }
            ChangePosition(currentPos, newPos);
        }
    }

    public void ChangePosition(int oldPos, int newPos)
    {
        GameObject player = players[currentPos];
        GameObject swap = players[newPos];
        player.transform.position = pos[newPos];
        swap.transform.position = pos[currentPos];
        players[newPos] = player;
        players[currentPos] = swap;
        currentPos = newPos;
    }

    public void SetMaxPoints(int p)
    {
        this.maxPoints = p;
    }

    public void CheckWinner()
    {
        winner = 0;
        if ((player2 > player1) && (player2 > player3))
        {
            winner = 1;
        }
        else if((player3 > player1) && (player3 > player2))
        {
            winner = 2;
        }
      
        Debug.Log("Winner is: " + winner);

        cntGames++;
        PlayerPrefs.SetInt("cntGames", cntGames);

        switch(winner)
        {
            case 0:
                cntPlayer1++;
                PlayerPrefs.SetInt("cntPlayer1", cntPlayer1);
                break;
            case 1:
                cntPlayer2++;
                PlayerPrefs.SetInt("cntPlayer2", cntPlayer2);
                break;
            case 2:
                cntPlayer3++;
                PlayerPrefs.SetInt("cntPlayer3", cntPlayer3);
                break;
        }


        if (choice == winner)
        {
            cntWinner++;
            PlayerPrefs.SetInt("cntWinner", cntWinner);
            Debug.Log("You win");
            SceneManager.LoadScene("End_Won");
        }
        else
        {
            Debug.Log("You loose");
            SceneManager.LoadScene("End_Lost");
        }

    }


    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "Game":
                Debug.Log("Game scene loaded");
                players.Add(GameObject.Find("Player1"));
                players.Add(GameObject.Find("Player2"));
                players.Add(GameObject.Find("Player3"));
                switch(choice)
                {
                    case 0:
                        GameObject.Find("Player2/Selected").SetActive(false);
                        GameObject.Find("Player3/Selected").SetActive(false);
                        break;
                    case 1:
                        GameObject.Find("Player1/Selected").SetActive(false);
                        GameObject.Find("Player3/Selected").SetActive(false);
                        break;
                    case 2:
                        GameObject.Find("Player1/Selected").SetActive(false);
                        GameObject.Find("Player2/Selected").SetActive(false);

                        break;
                }
                pos.Add(GameObject.Find("Player1").transform.position);
                pos.Add(GameObject.Find("Player2").transform.position);
                pos.Add(GameObject.Find("Player3").transform.position);
                currentPos = choice;
                break;
            case "End_Won":
            case "End_Lost":
                TextMeshProUGUI tStats = GameObject.Find("Stats").GetComponent<TextMeshProUGUI>();

                tStats.text = "Winner:\tPlayer " + (winner + 1) + "\r\n\n" +
                    "Max. Points:\t" + maxPoints + "\r\n" +
                    "Player 1:\t" + player1 + " Points\r\n" +
                    "Player 2:\t" + player2 + " Points\r\n" +
                    "Player 3:\t" + player3 + " Points\r\n\n" +
                    "You selected:\tPlayer " + (choice + 1) + "\r\n\n";
                if (winner == choice)
                {
                    tStats.text += "You're the winner!";
                }
                break;
            case "Start":
                if(FirstStats == true)
                {
                    ShowStartStats();
                }
                break;
        }
        
    }

    public void ShowStartStats()
    {
        cntGames = PlayerPrefs.GetInt("cntGames", 0);
        cntWinner = PlayerPrefs.GetInt("cntWinner", 0);
        cntPlayer1 = PlayerPrefs.GetInt("cntPlayer1", 0);
        cntPlayer2 = PlayerPrefs.GetInt("cntPlayer2", 0);
        cntPlayer3 = PlayerPrefs.GetInt("cntPlayer3", 0);

        TextMeshProUGUI tStatsStart = GameObject.Find("Stats").GetComponent<TextMeshProUGUI>();
        if (tStatsStart != null)
        {
            tStatsStart.text = "Games:\t" + cntGames + "\r\n" +
                "Wins:\t\t" + cntWinner + "\r\n" +
                "Player 1:\t" + cntPlayer1 + "\r\n" +
                "Player 2:\t" + cntPlayer2 + "\r\n" +
                "Player 3:\t" + cntPlayer3;
        }
        FirstStats = true;
    }

    public void ResetGame()
    {
        winner = 0;
        choice = 0;
        player1 = player2 = player3 = 0;

        SceneManager.LoadScene("Start");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddPointForPlayer(string name, int points)
    {
        switch(name)
        {
            case "Player1": 
                player1 = points;
                break;
            case "Player2":
                player2 = points;
                break;
            case "Player3":
                player3 = points;
                break;
        }
    }

}
