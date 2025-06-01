using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    GameObject[] coord => GameObject.FindGameObjectsWithTag("Coord");
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    [Header("Resource")]
    [SerializeField] int actionPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tmp = 1000000;

        foreach(GameObject c in coord)
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPos = coordinate.UpperTile;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPos = coordinate.LowerTile;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPos = coordinate.LeftTile;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPos = coordinate.RightTile;
        }
    }


    public void SetCoord(CoordScript cs) => coordinate = cs;

    public void SetAP() => actionPoint++;
}
