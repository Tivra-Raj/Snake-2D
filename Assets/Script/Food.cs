using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] BoxCollider2D spawnArea;

    public enum FoodType
    {
        None,
        MassGainer,
        MassBurner
    }

    [SerializeField] FoodType type;

    public FoodType GetFoodType()
    {
        return type;
    }

    void SpawnFood()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position  = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Snake_Controller>() != null)
        {
            SpawnFood();
        }
    }
}
