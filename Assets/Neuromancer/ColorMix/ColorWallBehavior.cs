using UnityEngine;

public class ColorWallBehavior : MonoBehaviour
{
    private SpriteRenderer SpR;
    private ColorMixManager CMix;
    private Collider2D Collider;
    void Start()
    {
        SpR = GetComponent<SpriteRenderer>();
        CMix = FindAnyObjectByType<ColorMixManager>();
        Collider = GetComponent<Collider2D>();

        SpR.color = CMix.ColorWallRandomized();
    }
    private void Update()
    {
        if (SpR.color == CMix.PlayerColorRef())
        {
            Collider.enabled = false;
        }
        else
        {
            Collider.enabled = true;
        }
    }


}
