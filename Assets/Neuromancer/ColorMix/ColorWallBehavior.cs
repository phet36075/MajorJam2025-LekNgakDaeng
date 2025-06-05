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
    [SerializeField]private bool IsSingleRandomized;
    [SerializeField]private bool IsBatchRandomized;
    [SerializeField]private ColorType WallColor;

    [SerializeField] bool is11 = false;

    void Start()
    {
        SpR = GetComponent<SpriteRenderer>();
        CMix = FindAnyObjectByType<ColorMixManager>();
        NavMeshMod = GetComponent<NavMeshModifier>();

        if(is11)
        {
            if (IsSingleRandomized)
            {
                do
                    SpR.color = CMix.ColorWallRandomized();
                while (SpR.color == CMix.Green);
                    
            }
        }
        else
        {
            if (IsSingleRandomized)
            {
               SpR.color = CMix.ColorWallRandomized();
            }
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

    public void ColorTypeOverride(ColorType OverrideColor)
    {
        Debug.Log("Color Wall Override");
        //WallColor = OverrideColor;
        SpR.color = CMix.ColorWallManual(OverrideColor);
    }

}
