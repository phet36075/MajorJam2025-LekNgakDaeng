using UnityEngine;
using UnityEngine.AI;

public class Collectible : MonoBehaviour
{
    GridManager gm => FindAnyObjectByType<GridManager>();
    Player player => FindAnyObjectByType<Player>();

    [Header("Reference")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] AudioClip collectSound;

    [Header("Coordinate")]
    [SerializeField] Vector2 targetPos;

    void Start()
    {
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
        transform.position = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCollision();   
    }

    void PlayerCollision()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.1f, playerMask))
        {
            SoundFXManager.instance.PlaySoundFXClip(collectSound);
            player.SetWeapon(true);
            Destroy(gameObject);
        }
    }
}
