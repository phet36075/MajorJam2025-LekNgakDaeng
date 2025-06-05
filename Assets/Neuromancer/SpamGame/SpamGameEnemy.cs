using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;

public class SpamGameEnemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float enemyInterval = .2f;
    [SerializeField] private float EnemyCountdownInterval;
    private bool CoroutineStarted = false;

    [Header("Components")]
    [SerializeField] private GridManager enemyGrid;
    [SerializeField] private TextMeshProUGUI CountDownText;
    [SerializeField] private GameObject SpamButtonVisual;
    private SpamGameManager spamGameManager;
    private NavMeshAgent navMeshAgent;

    [Header("Coordinate")]
    [SerializeField] private int Coordinate;
    [SerializeField] private Vector2 TargetPosition;

    void Start()
    {
        spamGameManager = FindAnyObjectByType<SpamGameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        SpamButtonVisual.SetActive(false);
        Initialization();
        SnapToGrid();
        //StartCoroutine(Move());
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

        if(EnemyCountdownInterval > 0)
        {
            EnemyCountdownInterval -= 1f * Time.deltaTime;
            CountDownText.text = Mathf.RoundToInt(EnemyCountdownInterval).ToString();
        }
        else
        {
            if (!CoroutineStarted)
            {
                CountDownText.gameObject.SetActive(false);
                SpamButtonVisual.gameObject.SetActive(true);
                StartCoroutine(Move());
                CoroutineStarted = true;
            }
        }
    }
}
