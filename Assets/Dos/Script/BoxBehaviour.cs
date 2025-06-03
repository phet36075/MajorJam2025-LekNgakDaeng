using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class BoxBehaviour : MonoBehaviour
{
    CoordScript coordinate;
    NavMeshAgent agent => GetComponent<NavMeshAgent>();
    NavMeshSurface[] surface => FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);
    [SerializeField] Vector2 targetPos = Vector2.zero;

    private void Start()
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

    }
    public void MovingBox(CoordScript coor)
    {
        targetPos = coor.transform.position;
        Debug.Log($"{coor.transform.position}");
        agent.SetDestination(targetPos);
    }
    private void Update()
    {
    }
    public void SetCoord(CoordScript cs) => coordinate = cs;

}
