using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class PlateManager : MonoBehaviour
{
    public List<PlateChecker> plateCheckers = new List<PlateChecker>();

    [Header("AddObj")]
    public GameObject objToSpawn;
    public Transform spawnPos;

    [Header("RemoveObj")]
    public Animator objToRemove;
    [SerializeField] private Level curLevel;

    [SerializeField] private NavMeshSurface[] surface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        surface = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);
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
            objToRemove.Play("RockDisappear");
    }

    private void AddObj()
    {
        if (curLevel == Level.AddObj)
        {
            GameObject gm = Instantiate(objToSpawn, spawnPos.position, Quaternion.identity);
            gm.GetComponent<Animator>().Play("AppearRock");
        }


    }
    enum Level
    {
        AddObj,
        RemoveObj
    }
}
