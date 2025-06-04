using UnityEngine;

public class Wall : MonoBehaviour
{
    GridManager gm => FindAnyObjectByType<GridManager>();
    [Header("Coordinate")]
    [SerializeField] Vector2 targetPos;

    void Start()
    {
        float tmp = 1000000;
        CoordScript cs = null;

        foreach (CoordScript c in gm.coord)
        {
            float dis = Vector2.Distance(transform.position, c.transform.position);

            if (tmp > dis)
            {
                tmp = dis;
                cs = c;
                targetPos = c.transform.position;
            }
        }
        transform.position = targetPos;
        cs.wall = this;
    }
}
