using UnityEngine;

public class ColorWallBatchRandom : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private bool IsOverrideAll;
    [SerializeField] private ColorWallBehavior[] ColorWalls;
    [SerializeField] private bool IsTwoColor;
    [SerializeField] private ColorType ExcludedColor;

    [Header("Debug Display")]
    public ColorType outputColor;
    void Start()
    {

        if (IsOverrideAll)
        {
            ColorWalls = FindObjectsByType<ColorWallBehavior>(FindObjectsSortMode.None);
        }

        int RandomNum = Random.Range(0, 3);
        switch (RandomNum)
        {
            case 0: outputColor = ColorType.Orange; ColorOverride(outputColor); break;
            case 1: outputColor = ColorType.Green; ColorOverride(outputColor); break;
            case 2: outputColor = ColorType.Purple; ColorOverride(outputColor); break;
            default: outputColor = ColorType.Orange; ColorOverride(outputColor); break;
        }

        if (IsTwoColor)
        {
            while(outputColor == ExcludedColor)
            {
                RandomNum = Random.Range(0, 3);
                switch (RandomNum)
                {
                    case 0: outputColor = ColorType.Orange; ColorOverride(outputColor); break;
                    case 1: outputColor = ColorType.Green; ColorOverride(outputColor); break;
                    case 2: outputColor = ColorType.Purple; ColorOverride(outputColor); break;
                    default: outputColor = ColorType.Orange; ColorOverride(outputColor); break;
                }
            }
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
