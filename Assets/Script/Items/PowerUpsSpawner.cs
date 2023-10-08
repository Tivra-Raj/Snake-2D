using TMPro;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] private Snake_Controller snake_Controller;
    [SerializeField] private BoxCollider2D spawnArea;
    [SerializeField] private float powerUpShowTime = 5;
    [SerializeField] private float powerUpRemaningTime = 5;
    [SerializeField] private GameObject[] powerUps;

    private GameObject powerUp;
    private Vector2 randomPos;

    private float totalTime;
    private float currenttime;

    private bool powerUpSpwaned;

    private void Start()
    {
        totalTime = powerUpShowTime + powerUpRemaningTime;
        currenttime = totalTime;
    }

    private void Update()
    {
        if(snake_Controller.gameOverOrPaused) { return; }

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
