using UnityEngine;

public class ColorMixManager : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    private SpriteRenderer Player_SpR;
    private int ColorMixCount = 0;
    private string Color_1, Color_2;

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
    }


    void Update()
    {
        
        
    }

    public void OnColorMixed(string ColorName)
    {
        /*
        if (ColorMixCount >= 2)
        {
            Player_SpR.color = defaultColor;
            ColorMixCount = 0;
            Color_1 = null;
            Color_2 = null;
        }
        */
        

        if (ColorMixCount == 0)
        {
            Color_1 = ColorName;
            Player_SpR.color = ColorMix();
            ColorMixCount+=1;
        }
        else if(ColorMixCount == 1)
        {
            Color_2 = ColorName;
            Player_SpR.color = ColorMix();
            ColorMixCount+=1;
        }
        else if(ColorMixCount > 1)
        {
            Player_SpR.color = defaultColor;
            ColorMixCount = 0;
            Color_1 = null;
            Color_2 = null;
        }

        Debug.Log(ColorMixCount);
    }

    public Color ColorMix()
    {
        Color outputColor = defaultColor;
        if(Color_1 != null && Color_2 == null)
        {
            switch (Color_1)
            {
                case "Red": outputColor = Red; break;
                case "Blue": outputColor = Blue; break;
                case "Yellow": outputColor = Yellow; break;
            }
        }
        else if(Color_1 != null && Color_2 != null)
        {
            if(Color_1 == "Red" && Color_2 == "Yellow" ||  Color_1 == "Yellow" && Color_2 == "Red")
            {
                outputColor = Orange;
            }
            else if(Color_1 == "Red" && Color_2 == "Blue" || Color_1 == "Blue" && Color_2 == "Red")
            {
                outputColor = Purple;
            }
            else if(Color_1 == "Yellow" && Color_2 == "Blue" || Color_1 == "Blue" && Color_2 == "Yellow")
            {
                outputColor = Green;
            }
        }

        return outputColor;
    }

    public Color SetPaletteColor(string ColorName)
    {
        Color SetColor = defaultColor;
        switch (ColorName)
        {
            case "Red": SetColor = Red; break;
            case "Yellow": SetColor = Yellow; break;
            case "Blue": SetColor = Blue; break;
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
}
