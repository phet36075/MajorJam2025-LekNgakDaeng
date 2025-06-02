using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public List<PlateChecker> plateCheckers = new List<PlateChecker>();
    public GameObject objToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int amountOfPlate = plateCheckers.Count;
        int checkCount = 0;
        foreach (PlateChecker checker in plateCheckers)
        {
            if(checker.isOn)
                checkCount++;
            if (checkCount == amountOfPlate)
            {
                Debug.Log("Yay");
            }
        }
    }
}
