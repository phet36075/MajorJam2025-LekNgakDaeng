using UnityEngine;

public class KeyPresser : MonoBehaviour
{
    public Vector2 moveDir;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            smolPlayer.instance.moveDir = moveDir;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            smolPlayer.instance.moveDir = Vector2.zero;
        }
    }
}
