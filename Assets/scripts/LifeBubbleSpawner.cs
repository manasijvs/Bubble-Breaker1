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
        float xPos = Random.Range(-8.5f, 43.3f); // Adjust range based on your game area
        float yPos = 1.6f;
        Vector2 spawnPosition = new Vector2(xPos, yPos); // Adjust Y position based on where you want to spawn

        Instantiate(LifeBubbleprefab, spawnPosition, Quaternion.identity);
    }
}
