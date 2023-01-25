using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D spawnArea;

    private void Start()
    {
        SpawnFood();
    }
    void SpawnFood()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.GetComponent<Snake_Controller>() != null)
       {
            SpawnFood();
       }
       if(collision.GetComponent<Snake2_Controller>() != null)
       {
            SpawnFood();
       }
    }
}
