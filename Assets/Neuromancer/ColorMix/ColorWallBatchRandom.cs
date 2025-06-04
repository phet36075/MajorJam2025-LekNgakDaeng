using UnityEngine;

public class ColorWallBatchRandom : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool IsOverrideAll;
    [SerializeField] private ColorWallBehavior[] ColorWalls;

    [Header("Debug Display")]
    [SerializeField]private ColorType outputColor;
    void Start()
    {

        if (IsOverrideAll)
        {
            ColorWalls = FindObjectsByType<ColorWallBehavior>(FindObjectsSortMode.None);
        }

        int RandomNum = Random.Range(0, 3);
        switch (RandomNum)
        {
            case 0: outputColor = ColorType.Orange; ColorOverride(outputColor); return;
            case 1: outputColor = ColorType.Green; ColorOverride(outputColor); return;
            case 2: outputColor = ColorType.Purple; ColorOverride(outputColor); return;
            default: outputColor = ColorType.Orange; ColorOverride(outputColor); return;
        }

      

    }

    void ColorOverride(ColorType color)
    {
        foreach(ColorWallBehavior CWalls in ColorWalls)
        {
            CWalls.ColorTypeOverride(color);
        }
    }

    
}
