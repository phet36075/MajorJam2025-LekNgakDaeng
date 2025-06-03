using UnityEngine;


public enum ColorType
{
    Red, Yellow,Blue,Green,Orange,Purple,Default
}
public class ColorMixManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    private SpriteRenderer Player_SpR;
    private int ColorMixCount = 0;
    private ColorType Color_1, Color_2;

    [Header("Parameters")]
    [SerializeField] private Color defaultColor = Color.gray;
    public Color Red;
    public Color Blue;
    public Color Yellow;
    public Color Orange;
    public Color Green;
    public Color Purple;
    void Start()
    {
        //Reference from Player Object Sprite Renderer
        Player_SpR = playerObj.GetComponent<SpriteRenderer>();
        Player_SpR.color = defaultColor;
        Color_1 = ColorType.Default;
        Color_2 = ColorType.Default;
    }


    void Update()
    {
        
        
    }

    public void OnColorMixed(ColorType color)
    {

        if (ColorMixCount == 0)
        {
            Color_1 = color;
            Player_SpR.color = ColorMix();
            ColorMixCount+=1;
        }
        else if(ColorMixCount == 1)
        {
            Color_2 = color;
            Player_SpR.color = ColorMix();
            ColorMixCount+=1;
        }
        else if(ColorMixCount > 1)
        {
            Player_SpR.color = defaultColor;
            ColorMixCount = 0;
            Color_1 = ColorType.Default;
            Color_2 = ColorType.Default;
        }

        Debug.Log(ColorMixCount);
    }

    public Color ColorMix()
    {
        Color outputColor = defaultColor;
        if(Color_1 != ColorType.Default && Color_2 == ColorType.Default)
        {
            switch (Color_1)
            {
                case ColorType.Red: outputColor = Red; break;
                case ColorType.Blue: outputColor = Blue; break;
                case ColorType.Yellow: outputColor = Yellow; break;
            }
        }
        else if(Color_1 != ColorType.Default && Color_2 != ColorType.Default)
        {
            if(Color_1 == ColorType.Red && Color_2 == ColorType.Yellow ||  Color_1 == ColorType.Yellow && Color_2 == ColorType.Red)
            {
                outputColor = Orange;
            }
            else if(Color_1 == ColorType.Red && Color_2 == ColorType.Blue || Color_1 == ColorType.Blue && Color_2 == ColorType.Red)
            {
                outputColor = Purple;
            }
            else if(Color_1 == ColorType.Yellow && Color_2 == ColorType.Blue || Color_1 == ColorType.Blue && Color_2 == ColorType.Yellow)
            {
                outputColor = Green;
            }
        }

        return outputColor;
    }

    public Color SetPaletteColor(ColorType color)
    {
        Color SetColor = defaultColor;
        switch (color)
        {
            case ColorType.Red: SetColor = Red; break;
            case ColorType.Yellow: SetColor = Yellow; break;
            case ColorType.Blue: SetColor = Blue; break;
        }
        return SetColor;
    }

    public Color PlayerColorRef()
    {
        return Player_SpR.color;
    }

    public Color ColorWallRandomized()
    {
        int colorselect = Random.Range(0, 3);
        Color OutputColor = defaultColor;

        switch (colorselect)
        {
            case 0: OutputColor = Orange; break;
            case 1: OutputColor = Purple; break;
            case 2: OutputColor = Green; break;
            default: OutputColor = Orange; break;
        }

        return OutputColor;
    }

    public Color ColorWallManual(ColorType color)
    {
        Color OutputColor = defaultColor;
        switch (color)
        {
            case ColorType.Red: OutputColor = Red; break;
            case ColorType.Yellow: OutputColor = Yellow; break;
            case ColorType.Blue: OutputColor = Blue; break;
            case ColorType.Orange: OutputColor = Orange; break;
            case ColorType.Green: OutputColor = Green; break;
            case ColorType.Purple: OutputColor = Purple; break;
            default: OutputColor = Orange; break;
        }

        return OutputColor;
    }
}
