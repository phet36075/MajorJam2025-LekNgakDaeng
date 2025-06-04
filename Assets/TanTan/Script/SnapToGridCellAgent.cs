using UnityEngine;
using UnityEngine.AI;

public class SnapToGridCellAgent : MonoBehaviour
{
    GridManager gm => FindAnyObjectByType<GridManager>();
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    [Header("Coordinate")]
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
    }

    private void Update()
    {
        agent.SetDestination(targetPos);
    }
}
