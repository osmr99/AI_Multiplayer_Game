using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotSpawner : MonoBehaviour
{

    [SerializeField] GameObject dot;
    public Queue<GameObject> dotPool = new Queue<GameObject>();
    int x;
    int y;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(dot, new Vector2(0, 0), Quaternion.identity);
        //StartCoroutine(Spawn());
        Debug.Log(1);
        for (int i = 0; i < 50; i++)
        {
            Debug.Log(3);
            var e = Instantiate(dot);
            dotPool.Enqueue(e);
            e.SetActive(false);
        }
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        if (dotPool.Count > 0)
        {
            Debug.Log(2);
            x = UnityEngine.Random.Range(-6, 6);
            y = UnityEngine.Random.Range(-4, 4);
            var current = dotPool.Dequeue();
            current.gameObject.SetActive(true);
            current.gameObject.transform.position = new Vector2(x, y);
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(Spawn());
        }

    }
}
