using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class SceneHandler2 : MonoBehaviour
{
    static PlayerInputManager manager;
    static InputControl control;
    static GameObject dotSpawner;
    static GameObject player1;
    static GameObject player2;
    static GameObject player3;
    static GameObject player4;
    static GameObject directorAI;
    public ScoreScriptable stats1;
    public ScoreScriptable stats2;
    public ScoreScriptable stats3;
    public ScoreScriptable stats4;
    GameObject playersJoinedParent;
    TMP_Text playerOneJoined;
    TMP_Text playerTwoJoined;
    TMP_Text playerThreeJoined;
    TMP_Text playerFourJoined;
    [SerializeField] Dotcolors playerColors;
    Color rngColor;
    public Dotcolors color1;
    public Dotcolors color2;
    public Dotcolors color3;
    public Dotcolors color4;
    public int playerIndexAvailable = 0;
    int num;
    PlayerMovement joiningPlayer;
    public PlayerMovement[] myArray;
    // Start is called before the first frame update

    private void Awake()
    {
        dotSpawner = GameObject.Find("Dot Spawner");
        directorAI = GameObject.Find("Director AI");
        dotSpawner.SetActive(false);
        stats1.currentScore = 0;
        stats2.currentScore = 0;
        stats3.currentScore = 0;
        stats4.currentScore = 0;
        stats1.currentSize = 0;
        stats2.currentSize = 0;
        stats3.currentSize = 0;
        stats4.currentSize = 0;
        stats1.currentSize = 0;
    }
    void Start()
    {
        SceneManager.LoadSceneAsync("Main Menu UI", LoadSceneMode.Additive);
        manager = GetComponent<PlayerInputManager>();
        RandomPlayerColor(color1);
        RandomPlayerColor(color2);
        RandomPlayerColor(color3);
        RandomPlayerColor(color4);
        StartCoroutine(gettingParent());
    }

    static public void LoadGame()
    {
        manager.DisableJoining();
        SceneManager.UnloadSceneAsync("Main Menu UI");
        setActive(dotSpawner);
        SceneManager.LoadSceneAsync("Gameplay UI 2", LoadSceneMode.Additive);
        directorAI.GetComponent<DirectorAI>().TheStart();
    }

    static void setActive(GameObject name)
    {
        name.SetActive(true);
    }

    public void playerJoin()
    {
        StartCoroutine(playerHere());
    }

    IEnumerator playerHere()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (playerIndexAvailable < 4)
        {
            myArray = Resources.FindObjectsOfTypeAll<PlayerMovement>();
            playerIndexAvailable++;
            yield return new WaitForSeconds(0.1f);
            for(int i = 0; i < myArray.Length - 1; i++)
            {
                if (myArray[i].playerIndex == 0)
                {
                    joiningPlayer = myArray[i];
                    joiningPlayer.playerIndex = playerIndexAvailable;
                    if (joiningPlayer.playerIndex == 1)
                    {
                        joiningPlayer.myColor = color1;
                        joiningPlayer.playerCircle.color = color1.colors[0];
                        playerJoinedText(playerOneJoined, 1);
                    }
                    if (joiningPlayer.playerIndex == 2)
                    {
                        joiningPlayer.myColor = color2;
                        joiningPlayer.playerCircle.color = color2.colors[0];
                        playerJoinedText(playerTwoJoined, 2);
                    }
                    if (joiningPlayer.playerIndex == 3)
                    {
                        joiningPlayer.myColor = color3;
                        joiningPlayer.playerCircle.color = color3.colors[0];
                        playerJoinedText(playerThreeJoined, 3);
                    }
                    if (joiningPlayer.playerIndex == 4)
                    {
                        joiningPlayer.myColor = color4;
                        joiningPlayer.playerCircle.color = color4.colors[0];
                        playerJoinedText(playerFourJoined, 1);
                    }
                }
            }

        }
    }

    IEnumerator gettingParent()
    {
        yield return new WaitForSeconds(0.05f);
        playersJoinedParent = GameObject.Find("Players Joined");
        foreach (TMP_Text text in playersJoinedParent.GetComponentsInChildren<TMP_Text>())
        {
            if (text.text.Length == 1)
            {
                playerOneJoined = text;
            }
            else if (text.text.Length == 2)
            {
                playerTwoJoined = text;
            }
            else if (text.text.Length == 3)
            {
                playerThreeJoined = text;
            }
            else if (text.text.Length == 4)
            {
                playerFourJoined = text;
            }
        }
    }

    void RandomPlayerColor(Dotcolors targetColor)
    {
        num = UnityEngine.Random.Range(0, playerColors.colors.Count);
        rngColor = playerColors.colors[num];
        rngColor.a = 1;
        targetColor.colors[0] = rngColor;
    }

    void playerJoinedText(TMP_Text text, int index)
    {
        text.text = "Player " + index.ToString() + "\nconnected!";
    }
}
