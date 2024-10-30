using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawner2 : MonoBehaviour
{

    [SerializeField] GameObject dot;
    public Queue<GameObject> dotPool = new Queue<GameObject>();
    [SerializeField] float x;
    [SerializeField] float y;
    float myX;
    float myY;
    [SerializeField] DotCount dotCount;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(dot, new Vector2(0, 0), Quaternion.identity);
        //StartCoroutine(Spawn());
        for (dotCount.currentCount = 0; dotCount.currentCount < dotCount.maxCount; dotCount.currentCount++)
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
            myX = Random.Range(-x, x + 1);
            myY = Random.Range(-y, y + 1);
            var current = dotPool.Dequeue();
            current.gameObject.SetActive(true);
            current.gameObject.transform.position = new Vector2(myX, myY);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Spawn());
    }
}
