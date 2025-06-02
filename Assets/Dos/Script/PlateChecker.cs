using UnityEngine;

public class PlateChecker : MonoBehaviour
{
    public bool isOn;
    public LayerMask targetLayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer.value) != 0)
        {
            isOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & targetLayer.value) != 0)
        {
            isOn = false;
        }
    }
    private void Update()
    {
        GetComponent<SpriteRenderer>().color = isOn ? Color.green : Color.red;
    }
}
