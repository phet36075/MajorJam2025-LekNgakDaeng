using UnityEngine;

public class PlateChecker : MonoBehaviour
{
    public bool isOn;
    public LayerMask targetLayer;
    private void Update()
    {
        isOn = Physics2D.OverlapBox(transform.position, Vector2.one * 0.5f, 0, targetLayer);
    }
}
