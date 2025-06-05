using UnityEngine;
using UnityEngine.UI;



public class ColorPaletteBehavior : MonoBehaviour
{
    [Header("Attributes")]
    public ColorType MainColor;
    [SerializeField] private AudioClip UseSfx;

    private ColorMixManager CMix;
    private SpriteRenderer SpR;



    private void Start()
    {
       CMix = FindAnyObjectByType<ColorMixManager>();
        SpR = GetComponent<SpriteRenderer>();

        //SpR.color = CMix.SetPaletteColor(MainColor);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            CMix.OnColorMixed(MainColor);
            SoundFXManager.instance.PlaySoundFXClip(UseSfx);
        }
    }
}
