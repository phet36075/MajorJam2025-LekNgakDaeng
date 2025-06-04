using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine;

public class Goal : MonoBehaviour
{
    WinLoseManager wlm => FindAnyObjectByType<WinLoseManager>();
    GridManager gm => FindAnyObjectByType<GridManager>();
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    Player player => FindAnyObjectByType<Player>();

    [Header("Reference")]
    [SerializeField] LayerMask playerMask;

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tmp = 1000000;

        foreach (CoordScript c in gm.coord)
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
        PlayerCollide();
    }

    void OnPlayerMove()
    {
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

    void PlayerCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, .52f, playerMask))
        {
            wlm.OnWin();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .52f);
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;
}
