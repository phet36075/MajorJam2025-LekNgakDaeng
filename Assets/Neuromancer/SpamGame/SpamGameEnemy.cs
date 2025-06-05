using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SpamGameEnemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float enemyInterval = .2f;


    [Header("Components")]
    [SerializeField] private GridManager enemyGrid;
    private SpamGameManager spamGameManager;
    private NavMeshAgent navMeshAgent;

    [Header("Coordinate")]
    [SerializeField] private int Coordinate;
    [SerializeField] private Vector2 TargetPosition;

    void Start()
    {
        spamGameManager = FindAnyObjectByType<SpamGameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Initialization();
        SnapToGrid();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(enemyInterval);
        TargetPosition = enemyGrid.coord[Coordinate].transform.position;
        Coordinate++;
        if (Coordinate >= enemyGrid.coord.Count)
            StopCoroutine(Move());
        else
            StartCoroutine(Move());
    }

    void Initialization()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    void SnapToGrid()
    {
        float tmp = 1000000;

        foreach (CoordScript c in enemyGrid.coord)
        {
            float dis = Vector2.Distance(transform.position, c.transform.position);

            if (tmp > dis)
            {
                tmp = dis;
                TargetPosition = c.transform.position;
            }
        }
    }


    void Update()
    {
        navMeshAgent.SetDestination(TargetPosition);
    }
}
