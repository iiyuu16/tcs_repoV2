using UnityEngine;

public class survivalSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject itemHP;
    [SerializeField] private Transform spawnPoint;
    public float spawnInterval = 10f;
    public float itemDespawnTime = 10f;

    private void Start()
    {
        InvokeRepeating("SpawnItem", 0f, spawnInterval);
    }

    private void SpawnItem()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("No spawn point assigned.");
            return;
        }

        if (itemHP != null)
        {
            GameObject coreHP = Instantiate(itemHP, spawnPoint.position, Quaternion.identity);
            Destroy(coreHP, itemDespawnTime);
        }
        else
        {
            Debug.LogWarning("No item prefab assigned.");
        }
    }
}