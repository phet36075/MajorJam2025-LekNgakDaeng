using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    [Header("Parameter")]
    [SerializeField] float walkInterval = 0.5f;

    bool isCooldown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tmp = 1000000;

        foreach(CoordScript c in GridManager.coord)
        {
            float dis = Vector2.Distance(transform.position, c.transform.position);

            if(tmp > dis)
            {
                tmp = dis;
                targetPos = c.transform.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetPos);
        PlayerController();
    }

    void PlayerController()
    {
        if(isCooldown) return;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPos = coordinate.UpperTile;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPos = coordinate.LowerTile;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPos = coordinate.LeftTile;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPos = coordinate.RightTile;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
    }

    public void SetCoord(CoordScript cs) => coordinate = cs;

    IEnumerator WalkInterval(float time)
    {
        isCooldown = true;
        yield return new WaitForSeconds(time);
        isCooldown = false;
    }
}