using UnityEngine;

public class ColorWallBehavior : MonoBehaviour
{
    private SpriteRenderer SpR;
    private ColorMixManager CMix;
    private Collider2D Collider;

    [Header("Parameters")]
    [SerializeField]private bool IsRandomized;
    [SerializeField]private ColorType WallColor;
    void Start()
    {
        SpR = GetComponent<SpriteRenderer>();
        CMix = FindAnyObjectByType<ColorMixManager>();
        Collider = GetComponent<Collider2D>();

        if (IsRandomized)
        {
            SpR.color = CMix.ColorWallRandomized();
        }
        else
        {
            SpR.color = CMix.ColorWallManual(WallColor);
        }
        
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
