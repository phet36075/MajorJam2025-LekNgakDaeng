using UnityEngine;

public class CoordScript : MonoBehaviour
{
    Player player => FindAnyObjectByType<Player>();

    [SerializeField] LayerMask mask;

    [Header("Coordinate")]
    [SerializeField] int x;
    [SerializeField] int y;

    #region AdjacentTile
    public Vector2 UpperTile = Vector2.zero;
    public Vector2 LowerTile = Vector2.zero;
    public Vector2 LeftTile = Vector2.zero;
    public Vector2 RightTile = Vector2.zero;
    #endregion

    // Update is called once per frame
    void Update()
    {
        PlayerCollide();
        UpperTile = GetUpperTile();
        LowerTile = GetLowerTile();
        LeftTile = GetLeftTile();
        RightTile = GetRightTile();
    }

    void PlayerCollide()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.1f, mask))
        {
            Debug.Log("Player is on Coord: " + x + ", " + y);
            player.SetCoord(this);
        }
    }

    public void SetCoord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    Vector2 GetUpperTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x && c.y == y + 1)
            {
                return c.transform.position;
            }
        }
        return this.transform.position;
    }

    Vector2 GetLowerTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x && c.y == y - 1)
            {
                return c.transform.position;
            }
        }
        return this.transform.position;
    }

    Vector2 GetLeftTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x - 1 && c.y == y)
            {
                return c.transform.position;
            }
        }
        return this.transform.position;
    }

    Vector2 GetRightTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x + 1 && c.y == y)
            {
                return c.transform.position;
            }
        }
        return this.transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
