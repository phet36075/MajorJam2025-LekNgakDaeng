using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpamGamePlayer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float PlayerSpeedDecrement = 0.1f;
    [SerializeField] private float PlayerInterval;
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
    [SerializeField] private CoordScript Coordinate;
    [SerializeField] private Vector2 TargetPosition;

    void Start()
    {
        spamGameManager = FindAnyObjectByType<SpamGameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        Initialization();
        SnapToGrid();
        StartCoroutine(Move(PlayerIntervalBonus));
    }

    IEnumerator Move(float playerIntervalBonus)
    {
        while (true)
        {
            yield return new WaitForSeconds(playerIntervalBonus);
            TargetPosition = Coordinate.RightTile.transform.position;
        }
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

        if (spamGameManager.IsGameEnded)
        {
            StopCoroutine(Move(PlayerIntervalBonus));
        }
    }

    public void SetCoord(CoordScript cs) => Coordinate = cs;
}
