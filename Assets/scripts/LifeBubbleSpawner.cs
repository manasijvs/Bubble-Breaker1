using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class LifeBubbleSpawner : MonoBehaviour
{
    public GameObject LifeBubbleprefab;
    public float spawnIntervalMin = 5f;
    public float spawnIntervalMax = 10f;

    
    private void Start()
    {
        StartCoroutine(SpawnLifeBubbleRoutine());
       
    }

    private IEnumerator SpawnLifeBubbleRoutine()
    {
        while (true)
        {
            float spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(spawnInterval);
            SpawnLifeBubble();
        }
    }

    private void SpawnLifeBubble()
    {
        gamemanager gameManager = FindFirstObjectByType<gamemanager>();
        switch(gameManager.GetCurrentLevel())
        {
            case gamemanager.Level.level1:
                float xPoslevel1 = Random.Range(-8.5f, 43.3f); // Adjust range based on your game area
                float yPoslevel1 = 1.6f;
                Vector2 spawnPositionlevel1 = new Vector2(xPoslevel1, yPoslevel1); // Adjust Y position based on where you want to spawn
                Instantiate(LifeBubbleprefab, spawnPositionlevel1, Quaternion.identity);
                break;
            case gamemanager.Level.level2:
                float xPos = Random.Range(-8.5f, 43.3f); // Adjust range based on your game area
                float yPos = 1.6f;
                Vector2 spawnPosition = new Vector2(xPos, yPos); // Adjust Y position based on where you want to spawn
                Instantiate(LifeBubbleprefab, spawnPosition, Quaternion.identity);
                break;
            case gamemanager.Level.level3:
                float xPoslevel3 = Random.Range(-4.6f, 47.2f); // Adjust range based on your game area
                float yPoslevel3 = 9.7f;
                Vector2 spawnPositionlevel3 = new Vector2(xPoslevel3, yPoslevel3); // Adjust Y position based on where you want to spawn
                Instantiate(LifeBubbleprefab, spawnPositionlevel3, Quaternion.identity);
                break;
            case gamemanager.Level.level4:
                float xPoslevel4 = Random.Range(-5.1f, 46.6f); // Adjust range based on your game area
                float yPoslevel4 = 10.4f;
                Vector2 spawnPositionlevel4 = new Vector2(xPoslevel4, yPoslevel4); // Adjust Y position based on where you want to spawn
                Instantiate(LifeBubbleprefab, spawnPositionlevel4, Quaternion.identity);
                break;
        }
        
    }
}
