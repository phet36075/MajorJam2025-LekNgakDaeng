using System.Collections.Generic;
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
        var options = new List<(float distance, bool canMove, Vector2 position)>
        {
            (Vector2.Distance(coordinate.UpperTile.transform.position, player.transform.position), !coordinate.UpperTile.isWall, coordinate.UpperTile.transform.position),
            (Vector2.Distance(coordinate.LowerTile.transform.position, player.transform.position), !coordinate.LowerTile.isWall, coordinate.LowerTile.transform.position),
            (Vector2.Distance(coordinate.LeftTile.transform.position, player.transform.position), !coordinate.LeftTile.isWall, coordinate.LeftTile.transform.position),
            (Vector2.Distance(coordinate.RightTile.transform.position, player.transform.position), !coordinate.RightTile.isWall, coordinate.RightTile.transform.position)
        };

        options.Sort((b, a) => a.distance.CompareTo(b.distance));

        foreach (var (distance, canMove, position) in options)
        {
            if (canMove)
            {
                targetPos = position;
                return;
            }
        }
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;
}
