using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

enum EnemyType
{
    Snake,
    Fly,
    Slime
}

public class Enemy : MonoBehaviour
{
    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    SacrificeManager sm => FindAnyObjectByType<SacrificeManager>();
    WinLoseManager wlm => FindAnyObjectByType<WinLoseManager>();
    GridManager gm => FindAnyObjectByType<GridManager>();
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    Player player => FindAnyObjectByType<Player>();

    [Header("Reference")]
    [SerializeField] LayerMask playerMask;

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    [Header("Move Parameter")]
    [SerializeField] bool canUp = true;
    [SerializeField] bool canDown = true;
    [SerializeField] bool canLeft = true;
    [SerializeField] bool canRight = true;

    [Header("Enemy Type")]
    [SerializeField] EnemyType type;

    [Header("Sound")]
    [SerializeField] AudioClip flySound;
    [SerializeField] AudioClip snakeSound;
    [SerializeField] AudioClip slimeClip;
    [SerializeField] AudioClip killSound;

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
        Debug.Log(coordinate);
        agent.SetDestination(targetPos);
        PlayerCollide();
    }

    void OnPlayerMove()
    {
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
        var options = new List<(float distance, bool canMove, Vector2 position)>()
        {
            (Vector2.Distance(coordinate.UpperTile.transform.position, player.transform.position), !coordinate.UpperTile.isWall, coordinate.UpperTile.transform.position),
            (Vector2.Distance(coordinate.LowerTile.transform.position, player.transform.position), !coordinate.LowerTile.isWall, coordinate.LowerTile.transform.position),
            (Vector2.Distance(coordinate.LeftTile.transform.position, player.transform.position), !coordinate.LeftTile.isWall, coordinate.LeftTile.transform.position),
            (Vector2.Distance(coordinate.RightTile.transform.position, player.transform.position), !coordinate.RightTile.isWall, coordinate.RightTile.transform.position)
        };

        options.Sort((a, b) => a.distance.CompareTo(b.distance));

        foreach (var (distance, canMove, position) in options)
        {
            if (canMove)
            {
                targetPos = position;

                float deltaX = targetPos.x - transform.position.x;

                if (Mathf.Abs(deltaX) > 0.01f)
                {
                    if (deltaX > 0)
                        sr.flipX = true;
                    else
                        sr.flipX = false;
                }

                PlaySoundCondition();

                return;
            }
        }
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

                float deltaX = targetPos.x - transform.position.x;

                if (Mathf.Abs(deltaX) > 0.01f)
                {
                    if (deltaX > 0)
                        sr.flipX = true;
                    else
                        sr.flipX = false;
                }

                PlaySoundCondition();

                return;
            }
        }
    }
    void PlayerCollide()
    {
        if(Physics2D.OverlapCircle(transform.position, .52f, playerMask))
        {
            if(player.havWeapon)
            {
                SoundFXManager.instance.PlaySoundFXClip(killSound);
                if (sm != null)
                    sm.killAmount++;
                gameObject.SetActive(false);
            }
            else
            {
                wlm.OnLose();
                player.gameObject.SetActive(false);
            }
        }
    }

    void PlaySoundCondition()
    {
        if(Physics2D.OverlapBox(transform.position, Vector3.one * 3f, 0f, playerMask))
        {
            if(gameObject.activeSelf != false)
            {
                switch(type)
                {
                    case EnemyType.Snake:
                        SoundFXManager.instance.PlaySoundFXClip(snakeSound);
                        break;
                    case EnemyType.Fly:
                        SoundFXManager.instance.PlaySoundFXClip(flySound);
                        break;
                    case EnemyType.Slime:
                        SoundFXManager.instance.PlaySoundFXClip(slimeClip);
                        break;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .52f);

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 3f);
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;
}
