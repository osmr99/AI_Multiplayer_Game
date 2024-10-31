using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler2 : MonoBehaviour
{
    static GameObject dotSpawner;
    static GameObject player1;
    static GameObject player2;
    static GameObject player3;
    static GameObject player4;
    static GameObject directorAI;
    public ScoreScriptable score1;
    public ScoreScriptable score2;
    public ScoreScriptable score3;
    public ScoreScriptable score4;
    // Start is called before the first frame update

    private void Awake()
    {
        dotSpawner = GameObject.Find("Dot Spawner 2");
        player1 = GameObject.Find("Player Prefab 1");
        player2 = GameObject.Find("Player Prefab 2");
        player3 = GameObject.Find("Player Prefab 3");
        player4 = GameObject.Find("Player Prefab 4");
        directorAI = GameObject.Find("Director AI");
        player1.SetActive(false);
        player2.SetActive(false);
        player3.SetActive(false);
        player4.SetActive(false);
        dotSpawner.SetActive(false);
    }
    void Start()
    {
        score1.currentScore = 0;
        score2.currentScore = 0;
        score3.currentScore = 0;
        score4.currentScore = 0;
        SceneManager.LoadSceneAsync("Main Menu UI", LoadSceneMode.Additive);
    }

    static public void LoadGame()
    {
        SceneManager.UnloadSceneAsync("Main Menu UI");
        setActive(dotSpawner);
        spawnPlayer(player1);
        spawnPlayer(player2);
        //spawnPlayer(player3);
        //spawnPlayer(player4);
        SceneManager.LoadSceneAsync("Gameplay UI 2", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Dot Spawner"));
        //Debug.Log("All scenes loaded");
        directorAI.GetComponent<DirectorAI>().TheStart();
    }

    static void setActive(GameObject name)
    {
        name.SetActive(true);
    }

    static void spawnPlayer(GameObject player)
    {
        setActive(player);
        player.transform.position = Vector2.zero;
    }
}
