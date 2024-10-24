using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotSpawner : MonoBehaviour
{

    [SerializeField] GameObject dot;
    public Queue<GameObject> dotPool = new Queue<GameObject>();
    [SerializeField] float x;
    [SerializeField] float y;
    float myX;
    float myY;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(dot, new Vector2(0, 0), Quaternion.identity);
        //StartCoroutine(Spawn());
        for (int i = 0; i < 50; i++)
        {
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
            myX = UnityEngine.Random.Range(-x,x+1);
            myY = UnityEngine.Random.Range(-y,y+1);
            var current = dotPool.Dequeue();
            current.gameObject.SetActive(true);
            current.gameObject.transform.position = new Vector2(myX, myY);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Spawn());
    }
}
