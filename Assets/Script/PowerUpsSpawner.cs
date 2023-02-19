using TMPro;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] BoxCollider2D spawnArea;
    [SerializeField] float powerUpShowTime = 5;
    [SerializeField] float powerUpRemaningTime = 5;
    [SerializeField] GameObject[] powerUps;

    GameObject powerUp;
    Vector2 randomPos;

    float totalTime;
    float currenttime;

    bool powerUpSpwaned;

    private void Start()
    {
        totalTime = powerUpShowTime + powerUpRemaningTime;
        currenttime = totalTime;
    }

    private void Update()
    {
        currenttime -= 1 * Time.deltaTime;

        if (currenttime <= (totalTime - powerUpShowTime) && !powerUpSpwaned)
        {
            powerUpSpwaned = true;
            SpawnPowerUp();
        }
        if (currenttime <= 0)
        {
            powerUpSpwaned = false;
            DeSpawnPowerUp();
            currenttime = totalTime;
        }
    }

    Vector2 GetRandomPos()
    {
        Bounds bounds = spawnArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        randomPos = new Vector3(Mathf.Round(x), Mathf.Round(y), 0f);

        return randomPos;
    }

    void SpawnPowerUp()
    {
        int randomPowerUp = Random.Range(0, powerUps.Length);

        powerUp = powerUps[randomPowerUp];
        powerUp.transform.position = GetRandomPos();
        powerUp.SetActive(true);
    }

    void DeSpawnPowerUp()
    {
        if (powerUp)
        {
            powerUp.SetActive(false);
        }
    }
}
