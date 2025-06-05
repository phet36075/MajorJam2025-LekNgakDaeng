using UnityEngine;

public class SacrificeEnemy : MonoBehaviour
{
    Player player => FindAnyObjectByType<Player>();
    [SerializeField] LayerMask playerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCollide();
    }

    void PlayerCollide()
    {
        if (Physics2D.OverlapCircle(transform.position, .52f, playerMask))
        {

        }
    }
}
