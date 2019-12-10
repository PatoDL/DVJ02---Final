using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int amount;
    public Transform t;
    public Transform tLimit;

    public float maxTime;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<amount;i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(t.position.x,tLimit.position.x),25f,
                Random.Range(t.position.z,tLimit.position.z)), Quaternion.identity,this.transform);
        }
        timer = maxTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            Instantiate(prefab, new Vector3(Random.Range(t.position.x, tLimit.position.x), 25f,
                    Random.Range(t.position.z, tLimit.position.z)), Quaternion.identity, this.transform);
            timer = maxTime;
        }
    }
}
