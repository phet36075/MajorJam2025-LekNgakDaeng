using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    SpriteRenderer sr => GetComponent<SpriteRenderer>();
    GridManager gm => FindAnyObjectByType<GridManager>();
    NavMeshAgent agent => GetComponent<NavMeshAgent>();

    [Header("Coordinate")]
    [SerializeField] CoordScript coordinate;
    [SerializeField] Vector2 targetPos = Vector2.zero;

    [Header("Parameter")]
    [SerializeField] float walkInterval = 0.5f;

    [Header("Resource")]
    public bool havWeapon = false;
    [SerializeField] GameObject rightKnife;
    [SerializeField] GameObject leftKnife;

    [Header("Sound")]
    [SerializeField] AudioClip walkSound;

    bool isCooldown = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        float tmp = 1000000;

        foreach(CoordScript c in gm.coord)
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
        if (sr.flipX)
            WeaponVisualization(leftKnife);
        else
            WeaponVisualization(rightKnife);
    }

    void PlayerController()
    {
        if(isCooldown) return;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (coordinate.UpperTile.isWall || (coordinate.UpperTile.isBox && coordinate.UpperTile.UpperTile.isWall)) return;
            if (coordinate.UpperTile.isBox)
                coordinate.UpperTile.boxBehaviour.MovingBox(coordinate.UpperTile.UpperTile);
            SoundFXManager.instance.PlaySoundFXClip(walkSound);
            targetPos = coordinate.UpperTile.transform.position;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (coordinate.LowerTile.isWall || (coordinate.LowerTile.isBox && coordinate.LowerTile.LowerTile.isWall)) return;
            if (coordinate.LowerTile.isBox)
                coordinate.LowerTile.boxBehaviour.MovingBox(coordinate.LowerTile.LowerTile);
            SoundFXManager.instance.PlaySoundFXClip(walkSound);
            targetPos = coordinate.LowerTile.transform.position;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (coordinate.LeftTile.isWall || (coordinate.LeftTile.isBox && coordinate.LeftTile.LeftTile.isWall)) return;
            if (coordinate.LeftTile.isBox)
                coordinate.LeftTile.boxBehaviour.MovingBox(coordinate.LeftTile.LeftTile);
            sr.flipX = true;    
            SoundFXManager.instance.PlaySoundFXClip(walkSound);
            targetPos = coordinate.LeftTile.transform.position;
            StartCoroutine(WalkInterval(walkInterval));
            OnPlayerMoveSubscription.Instance.CheckPlayerMove();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (coordinate.RightTile.isWall || (coordinate.RightTile.isBox && coordinate.RightTile.RightTile.isWall)) return;
            if (coordinate.RightTile.isBox)
                coordinate.RightTile.boxBehaviour.MovingBox(coordinate.RightTile.RightTile);
            sr.flipX = false;   
            SoundFXManager.instance.PlaySoundFXClip(walkSound);
            targetPos = coordinate.RightTile.transform.position;
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

    void WeaponVisualization(GameObject knife)
    {
        if (havWeapon)
        {
            knife.SetActive(true);
        }
        else
            knife.SetActive(false);
    }

    public void SetWeapon(bool havAPao) => havWeapon = havAPao;
}