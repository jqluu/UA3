using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner1 : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public float spawnInterval = 2f; 
    [SerializeField]
    public bool isSpawning = true;


    private void Start()
    {
        // Start the spawning process.
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (isSpawning) // Continue spawning indefinitely.
        {
            
            GameObject newEnemy = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
            
            // Wait for the specified interval before spawning the next object.
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}