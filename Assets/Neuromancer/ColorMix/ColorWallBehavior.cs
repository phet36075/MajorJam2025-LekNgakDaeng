using UnityEngine;
using UnityEngine.UI;
using NavMeshPlus.Components;

[RequireComponent(typeof(NavMeshModifier))]
public class ColorWallBehavior : MonoBehaviour
{
    private SpriteRenderer SpR;
    private ColorMixManager CMix;
    private NavMeshModifier NavMeshMod;

    [Header("Parameters")]
    [SerializeField]private bool IsRandomized;
    [SerializeField]private ColorType WallColor;

    void Start()
    {
        SpR = GetComponent<SpriteRenderer>();
        CMix = FindAnyObjectByType<ColorMixManager>();
        NavMeshMod = GetComponent<NavMeshModifier>();
       

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
      //WallConditionCheck();
    }

    public void WallConditionCheck()
    {
        if (SpR.color == CMix.PlayerColorRef())
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.layer = 0;
            NavMeshMod.area = 0;
        }
        else
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = true;
            NavMeshMod.area = 1;
            gameObject.layer = 8;
        }
    }


}
