using UnityEngine;

public class CoordScript : MonoBehaviour
{
    Player player => FindAnyObjectByType<Player>();
    Goal goal => FindAnyObjectByType<Goal>();
    Enemy enemy => FindAnyObjectByType<Enemy>();
    public Wall wall;

    [Header("Reference")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask goalMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask wallMask;

    [Header("Coordinate")]
    [SerializeField] int x;
    [SerializeField] int y;

    [Header("Condition")]
    public bool isWall = false;

    #region AdjacentTile
    public CoordScript UpperTile = null;
    public CoordScript LowerTile = null;
    public CoordScript LeftTile = null;
    public CoordScript RightTile = null;
    #endregion

    bool isAPAdd =  false;

    // Update is called once per frame
    void Update()
    {
        PlayerCollide();
        GoalCollide();
        EnemyCollide();
        UpperTile = GetUpperTile();
        LowerTile = GetLowerTile();
        LeftTile = GetLeftTile();
        RightTile = GetRightTile();
    }

    void PlayerCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, playerMask))
        {
            player.SetCoord(this);
        }
    }

    void GoalCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, goalMask))
        {
            goal.SetCoord(this);
        }
    }

    void EnemyCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, enemyMask))
        {
            enemy.SetCoord(this);
        }
    }

    void WallCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, wallMask))
        {
            isWall = true;
        }
    }

    public void SetCoord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    #region AdjacentTile
    CoordScript GetUpperTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x && c.y == y + 1)
            {
                return c;
            }
        }
        return this;
    }

    CoordScript GetLowerTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x && c.y == y - 1)
            {
                return c;
            }
        }
        return this;
    }

    CoordScript GetLeftTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x - 1 && c.y == y)
            {
                return c;
            }
        }
        return this;
    }

    CoordScript GetRightTile()
    {
        CoordScript[] cs = FindObjectsByType<CoordScript>(FindObjectsSortMode.None);
        foreach (CoordScript c in cs)
        {
            if (c.x == x + 1 && c.y == y)
            {
                return c;
            }
        }
        return this;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
