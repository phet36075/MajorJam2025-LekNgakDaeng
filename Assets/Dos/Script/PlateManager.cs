using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class PlateManager : MonoBehaviour
{
    public List<PlateChecker> plateCheckers = new List<PlateChecker>();

    [Header("Level 5")]
    public GameObject objToSpawn;
    public Transform spawnPos;

    [Header("Level 14")]
    public GameObject objToRemove;
    [SerializeField] private Level curLevel;

    private NavMeshSurface[] surface => FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);
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
                AddObj();
                RemoveObj();
                isSpawn = true;
            }
        }
    }

    private void RemoveObj()
    {
        if (curLevel == Level.RemoveObj)
            Destroy(objToRemove);
        BakeMap();
    }

    private void AddObj()
    {
        if (curLevel == Level.AddObj)
        {
            Instantiate(objToSpawn, spawnPos.position, Quaternion.identity);
        }
        BakeMap();

    }
    private void BakeMap()
    {
        foreach (NavMeshSurface checker in surface)
        {
            checker.BuildNavMesh();
        }
    }
    enum Level
    {
        AddObj,
        RemoveObj
    }
}
