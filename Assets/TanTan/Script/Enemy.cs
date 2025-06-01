using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    Player player => FindAnyObjectByType<Player>();

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

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
        EnemyMovement();
    }

    void EnemyMovement()
    {
        if(player.havWeapon)
            MoveAwayFromPlayer();
        else
            MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        float upperTileDistanceFromPlayer = Vector2.Distance(coordinate.UpperTile.transform.position, player.transform.position);
        float lowerTileDistanceFromPlayer = Vector2.Distance(coordinate.LowerTile.transform.position, player.transform.position);
        float leftTileDistanceFromPlayer = Vector2.Distance(coordinate.LeftTile.transform.position, player.transform.position);
        float rightTileDistanceFromPlayer = Vector2.Distance(coordinate.RightTile.transform.position, player.transform.position);

        float minDistance = Mathf.Min(upperTileDistanceFromPlayer, lowerTileDistanceFromPlayer, leftTileDistanceFromPlayer, rightTileDistanceFromPlayer);
        if (minDistance == upperTileDistanceFromPlayer)
        {
            if(!coordinate.UpperTile.isWall)
                targetPos = coordinate.UpperTile.transform.position;
        }
        else if (minDistance == lowerTileDistanceFromPlayer)
        {
            if (!coordinate.LowerTile.isWall)
                targetPos = coordinate.LowerTile.transform.position;
        }
        else if (minDistance == leftTileDistanceFromPlayer)
        {
            if (!coordinate.LeftTile.isWall)
                targetPos = coordinate.LeftTile.transform.position;
        }
        else if (minDistance == rightTileDistanceFromPlayer)
        {
            if (!coordinate.RightTile.isWall)
                targetPos = coordinate.RightTile.transform.position;
        }
    }

    void MoveAwayFromPlayer()
    {
        float upperTileDistanceFromPlayer = Vector2.Distance(coordinate.UpperTile.transform.position, player.transform.position);
        float lowerTileDistanceFromPlayer = Vector2.Distance(coordinate.LowerTile.transform.position, player.transform.position);
        float leftTileDistanceFromPlayer = Vector2.Distance(coordinate.LeftTile.transform.position, player.transform.position);
        float rightTileDistanceFromPlayer = Vector2.Distance(coordinate.RightTile.transform.position, player.transform.position);

        float maxDistance = Mathf.Max(upperTileDistanceFromPlayer, lowerTileDistanceFromPlayer, leftTileDistanceFromPlayer, rightTileDistanceFromPlayer);
        if (maxDistance == upperTileDistanceFromPlayer)
        {
            if (!coordinate.UpperTile.isWall)
                targetPos = coordinate.UpperTile.transform.position;
        }
        else if (maxDistance == lowerTileDistanceFromPlayer)
        {
            if (!coordinate.LowerTile.isWall)
                targetPos = coordinate.LowerTile.transform.position;
        }
        else if (maxDistance == leftTileDistanceFromPlayer)
        {
            if (!coordinate.LeftTile.isWall)
                targetPos = coordinate.LeftTile.transform.position;
        }
        else if (maxDistance == rightTileDistanceFromPlayer)
        {
            if (!coordinate.RightTile.isWall)
                targetPos = coordinate.RightTile.transform.position;
        }
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;
}
