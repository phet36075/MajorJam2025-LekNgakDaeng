using UnityEngine;

public class SacrificeManager : MonoBehaviour
{
    public float killAmount = 0;
    [SerializeField] GameObject goalPrefab;
    [SerializeField] Transform spawnPoint;
    bool isSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(killAmount >= 3 && !isSpawned)
        {
            SpawnGoal();
            isSpawned = true;
        }
    }

    void SpawnGoal()
    {
        Instantiate(goalPrefab, spawnPoint.position, Quaternion.identity);
    }
}
