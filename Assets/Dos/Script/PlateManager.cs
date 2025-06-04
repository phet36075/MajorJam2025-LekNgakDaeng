using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class PlateManager : MonoBehaviour
{
    public List<PlateChecker> plateCheckers = new List<PlateChecker>();

    [Header("Level 5")]
    public GameObject objToSpawn;
    public Transform spawnPos;

    [Header("Level 15")]
    public GameObject objToRemove;
    [SerializeField] private Level curLevel;

    private NavMeshSurface surface => FindAnyObjectByType<NavMeshSurface>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    bool isSpawn;
    // Update is called once per frame
    void Update()
    {
        int amountOfPlate = plateCheckers.Count;
        int checkCount = 0;
        foreach (PlateChecker checker in plateCheckers)
        {
            if(checker.isOn)
                checkCount++;
            if (checkCount == amountOfPlate && !isSpawn)
            {
                Level5();
                Level14();
                isSpawn = true;
            }
        }
    }

    private void Level14()
    {
        if (curLevel == Level.Level14)
            Destroy(objToRemove);
        surface.BuildNavMesh();
    }

    private void Level5()
    {
        if (curLevel == Level.Level5)
        {
            Instantiate(objToSpawn, spawnPos.position, Quaternion.identity);
        }
    }

    enum Level
    {
        Level5,
        Level14
    }
}
