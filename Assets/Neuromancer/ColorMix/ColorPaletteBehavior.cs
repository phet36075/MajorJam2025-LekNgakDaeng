using UnityEngine;

public class ColorPaletteBehavior : MonoBehaviour
{
    [Header("Attributes")]
    public string ColorName;

    private ColorMixManager CMix;
    private SpriteRenderer SpR;

    private void Start()
    {
       CMix = FindAnyObjectByType<ColorMixManager>();
        SpR = GetComponent<SpriteRenderer>();

        SpR.color = CMix.SetPaletteColor(ColorName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CMix.OnColorMixed(ColorName);
        }
    }
}
