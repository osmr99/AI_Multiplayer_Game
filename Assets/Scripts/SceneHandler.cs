using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    static GameObject player;
    static GameObject dotSpawner;
    public ScoreScriptable score;
    // Start is called before the first frame update

    private void Awake()
    {
        player = GameObject.Find("Player Prefab");
        dotSpawner = GameObject.Find("Dot Spawner 2");
        player.SetActive(false);
        dotSpawner.SetActive(false);
    }
    void Start()
    {
        score.currentScore = 0;
        SceneManager.LoadSceneAsync("Main Menu UI", LoadSceneMode.Additive);
    }

    static public void LoadGame()
    {
        SceneManager.UnloadSceneAsync("Main Menu UI");
        setActive(player);
        setActive(dotSpawner);
        player.transform.position = Vector2.zero;
        SceneManager.LoadSceneAsync("Gameplay UI", LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName("Dot Spawner"));
        Debug.Log("All scenes loaded");
    }
    
    static void setActive(GameObject name)
    {
        name.SetActive(true);
    }
}
