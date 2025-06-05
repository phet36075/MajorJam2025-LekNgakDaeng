using UnityEngine;

public class move4Test : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public bool isLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.linearVelocity = new Vector2(moveX, moveY) * speed;

        // Flip the sprite based on direction
        if (moveX < 0 && !isLeft)
        {
            Flip(true);
        }
        else if (moveX > 0 && isLeft)
        {
            Flip(false);
        }
    }

    void Flip(bool faceLeft)
    {
        isLeft = faceLeft;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (isLeft ? -1 : 1);
        transform.localScale = scale;
    }
}
