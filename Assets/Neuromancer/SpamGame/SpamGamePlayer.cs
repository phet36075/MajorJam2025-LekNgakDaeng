using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpamGamePlayer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float PlayerSpeedDecrement = 0.1f;
    [SerializeField] private float PlayerInterval;
    [SerializeField] private float PlayerCountdownInterval;

    private bool CoroutineStarted = false;
    private float PlayerIntervalBonus 
    { 
        get => PlayerInterval;
        set 
        {
            PlayerInterval = Mathf.Clamp(value, spamGameManager.MinIntervalBonusCap, spamGameManager.DefaultInterval);
        }
    }
    

    [Header("Components")]
    [SerializeField] private GridManager PlayerGrid; 
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
        //StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(PlayerIntervalBonus);
        TargetPosition = PlayerGrid.coord[Coordinate].transform.position;
        Coordinate++;
        if (Coordinate >= PlayerGrid.coord.Count)
            StopCoroutine(Move());
        else
            StartCoroutine(Move());
    }

    void Initialization()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        PlayerIntervalBonus = spamGameManager.DefaultInterval;
        
    }

    void SnapToGrid()
    {
        float tmp = 1000000;

        foreach (CoordScript c in PlayerGrid.coord)
        {
            float dis = Vector2.Distance(transform.position, c.transform.position);

            if (tmp > dis)
            {
                tmp = dis;
                TargetPosition = c.transform.position;
            }
        }
    }

    void PlayerSpeedControl()
    {
        if (PlayerIntervalBonus > 0)
        {
            PlayerIntervalBonus += 0.25f * Time.deltaTime;
        }

        if (Input.GetKeyDown(spamGameManager.SpamKey) || Input.GetKeyDown(spamGameManager.AltSpamKey))
        {
            if (PlayerIntervalBonus > spamGameManager.MinIntervalBonusCap)
            {
                PlayerIntervalBonus -= PlayerSpeedDecrement;
            }
        }
    }

    void Update()
    {
        navMeshAgent.SetDestination(TargetPosition);
        PlayerSpeedControl();

        if (PlayerCountdownInterval > 0)
        {
            PlayerCountdownInterval -= 1f * Time.deltaTime;
            
        }
        else
        {
            if (!CoroutineStarted)
            {
                
                StartCoroutine(Move());
                CoroutineStarted = true;
            }
        }
    }
}
