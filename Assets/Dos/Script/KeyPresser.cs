using UnityEngine;

public class KeyPresser : MonoBehaviour
{
    public Sprite origin, pressed;
    public Vector2 moveDir;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            smolPlayer.instance.moveDir = moveDir;
            GetComponent<SpriteRenderer>().sprite = pressed;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            smolPlayer.instance.moveDir = Vector2.zero;
            GetComponent<SpriteRenderer>().sprite = origin;
        }
    }
}
