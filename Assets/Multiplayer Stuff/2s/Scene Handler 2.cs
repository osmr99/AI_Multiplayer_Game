using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneHandler2 : MonoBehaviour
{
    static PlayerInputManager manager;
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
    [SerializeField] Dotcolors playerColors;
    Color rngColor;
    public Dotcolors color1;
    public Dotcolors color2;
    public Dotcolors color3;
    public Dotcolors color4;
    public int playerIndexAvailable = 0;
    int num;
    PlayerMovement joiningPlayer;
    public PlayerMovement[] myList;
    // Start is called before the first frame update

    private void Awake()
    {
        dotSpawner = GameObject.Find("Dot Spawner");
        directorAI = GameObject.Find("Director AI");
        dotSpawner.SetActive(false);
    }
    void Start()
    {
        stats1.currentScore = 0;
        stats2.currentScore = 0;
        stats3.currentScore = 0;
        stats4.currentScore = 0;
        stats1.currentSize = 0;
        stats2.currentSize = 0;
        stats3.currentSize = 0;
        stats4.currentSize = 0;
        stats1.currentSize = 0;
        SceneManager.LoadSceneAsync("Main Menu UI", LoadSceneMode.Additive);
        manager = GetComponent<PlayerInputManager>();
        RandomPlayerColor(color1);
        RandomPlayerColor(color2);
        RandomPlayerColor(color3);
        RandomPlayerColor(color4);
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
            myList = Resources.FindObjectsOfTypeAll<PlayerMovement>();
            playerIndexAvailable++;
            yield return new WaitForSeconds(0.1f);
            for(int i = 0; i < myList.Length - 1; i++)
            {
                if (myList[i].playerIndex == 0)
                {
                    joiningPlayer = myList[i];
                    joiningPlayer.playerIndex = playerIndexAvailable;
                    if (joiningPlayer.playerIndex == 1)
                    {
                        joiningPlayer.myColor = color1;
                        joiningPlayer.playerCircle.color = color1.colors[0];
                    }
                    if (joiningPlayer.playerIndex == 2)
                    {
                        joiningPlayer.myColor = color2;
                        joiningPlayer.playerCircle.color = color2.colors[0];
                    }
                    if (joiningPlayer.playerIndex == 3)
                    {
                        joiningPlayer.myColor = color3;
                        joiningPlayer.playerCircle.color = color3.colors[0];
                    }
                    if (joiningPlayer.playerIndex == 4)
                    {
                        joiningPlayer.myColor = color4;
                        joiningPlayer.playerCircle.color = color4.colors[0];
                    }
                }
            }

        }
    }

    void RandomPlayerColor(Dotcolors targetColor)
    {
        num = Random.Range(0, playerColors.colors.Count);
        rngColor = playerColors.colors[num];
        rngColor.a = 1;
        targetColor.colors[0] = rngColor;
    }
}
