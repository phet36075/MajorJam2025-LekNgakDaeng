using UnityEngine;

public class smolPlayer : MonoBehaviour
{
    public static smolPlayer instance;
    public float speed;
    public Rigidbody2D rb;
    public Vector2 moveDir;

    private bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null)
            instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
            rb.linearVelocity = moveDir * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isDead = true;
            Debug.Log("Dead");
        }
    }
}
