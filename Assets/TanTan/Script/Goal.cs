using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Goal : MonoBehaviour
{
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    Player player => FindAnyObjectByType<Player>();

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    [Header("Resource")]
    [SerializeField] int actionPoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tmp = 1000000;

        foreach (CoordScript c in GridManager.coord)
        {
            float dis = Vector2.Distance(transform.position, c.transform.position);

            if (tmp > dis)
            {
                tmp = dis;
                targetPos = c.transform.position;
            }
        }

        OnPlayerMoveSubscription.Instance.OnPlayerMove += this.OnPlayerMove;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetPos);
    }

    void OnPlayerMove()
    {
        Debug.Log("Player Moved");
        MoveAwayFromPlayer();
    }

    void MoveAwayFromPlayer()
    {
        float upperTileDistanceFromPlayer = Vector2.Distance(coordinate.UpperTile, player.transform.position);
        float lowerTileDistanceFromPlayer = Vector2.Distance(coordinate.LowerTile, player.transform.position);
        float leftTileDistanceFromPlayer = Vector2.Distance(coordinate.LeftTile, player.transform.position);
        float rightTileDistanceFromPlayer = Vector2.Distance(coordinate.RightTile, player.transform.position);

        float maxDistance = Mathf.Max(upperTileDistanceFromPlayer, lowerTileDistanceFromPlayer, leftTileDistanceFromPlayer, rightTileDistanceFromPlayer);
        if (maxDistance == upperTileDistanceFromPlayer)
        {
            targetPos = coordinate.UpperTile;
        }
        else if (maxDistance == lowerTileDistanceFromPlayer)
        {
            targetPos = coordinate.LowerTile;
        }
        else if (maxDistance == leftTileDistanceFromPlayer)
        {
            targetPos = coordinate.LeftTile;
        }
        else if (maxDistance == rightTileDistanceFromPlayer)
        {
            targetPos = coordinate.RightTile;
        }
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;

    public void ResetAP() => actionPoint = 1;
}
