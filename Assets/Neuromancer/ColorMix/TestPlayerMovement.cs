using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(Horizontal * MoveSpeed * Time.deltaTime, Vertical * MoveSpeed * Time.deltaTime));

    }
}
