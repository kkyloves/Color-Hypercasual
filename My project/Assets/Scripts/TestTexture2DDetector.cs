using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorItem
{
    public string ColorItemId;
    public Color Color;
    public int ColorQuantityCount;
    public int BoardMapCount;
}

[System.Serializable]
public class ColorDivision
{
    public List<ColorItem> ColorItems = new List<ColorItem>();
    public List<int> BoardNumbers = new List<int>();
    public int TotalColorCount;

    public ColorItem GetExistingColorItem(Color p_color)
    {
        foreach (ColorItem item in ColorItems)
        {
            if (item.Color.Equals(p_color))
            {
                return item;
            }
        }

        return null;
    }
}

public class TestTexture2DDetector : MonoBehaviour
{
    [SerializeField] private List<Color> m_colors = new List<Color>();
    [SerializeField] private List<int>  m_boardNumbers = new List<int>();

    [SerializeField] private List<ColorDivision> m_colorDivisions = new List<ColorDivision>();

    [SerializeField] private int m_width;
    [SerializeField] private int m_height;

    public int Width => m_width;
    public int Height => m_height;
    
    public static TestTexture2DDetector Instance;
    public System.Drawing.Bitmap Bitmap;

    public List<ColorDivision> ColorDivisions => m_colorDivisions;

    public List<int> BoardNumbers => m_boardNumbers;
    public List<Color> Color => m_colors;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        return;
        Bitmap = new System.Drawing.Bitmap("Assets/Bille E Aseprite.png");
        
        Debug.Log("Width: " + Bitmap.Width);
        Debug.Log("Height: " + Bitmap.Height);

        int boardMaxCheckerCounter = 0;
        int widthDivisionCounter = 0;

        int boardCounter = 0;

        List<int> widthDivision = new List<int>();
        int divide = Bitmap.Width / 3;

        for (int i = 1; i < 4; i++)
        {
            widthDivision.Add(divide * i);
        }

        widthDivision[widthDivision.Count - 1] = Bitmap.Width;

        foreach (var logs in widthDivision)
        {
            Debug.Log(logs);
        }

        for (int i = 0; i < 6; i++)
        {
            ColorDivision colorDivision = new ColorDivision();
            m_colorDivisions.Add(colorDivision);
        }
        

        // return;
        for (int x = 0; x < Bitmap.Width; x++)
        {
            for (int y = 0; y < Bitmap.Height; y++)
            {
                float r = Normalize(Bitmap.GetPixel(x, y).R, 0f, 255f);
                float g = Normalize(Bitmap.GetPixel(x, y).G, 0f, 255f);
                float b = Normalize(Bitmap.GetPixel(x, y).B, 0f, 255f);
                float a = Normalize(Bitmap.GetPixel(x, y).A, 0f, 255f);
                Color nColor = new Color(r, g, b, a);
                
                // set the board , set the colors
                int colorBoardNumber = 0;
                if (!m_colors.Contains(nColor))
                {
                    m_colors.Add(nColor);
                    colorBoardNumber = m_colors.Count;
                    m_boardNumbers.Add(m_colors.Count);

                }
                else
                {
                    for (int i = 0; i < m_colors.Count; i++)
                    {
                        if (m_colors[i].Equals(nColor))
                        {
                            colorBoardNumber = i + 1;
                            m_boardNumbers.Add(i + 1);
                            break;
                        }
                    }
                }
                
                //set color per divisions
                if (x >= widthDivision[widthDivisionCounter])
                {
                    widthDivisionCounter++;
                }

                int colorDivisionCounter;
                if (boardMaxCheckerCounter >= 10)
                {
                    colorDivisionCounter = widthDivisionCounter + 3;
                }
                else
                {
                    colorDivisionCounter = widthDivisionCounter;
                }
                
                ColorItem colorItem = new ColorItem {Color = nColor, BoardMapCount = colorBoardNumber};
                
                if (m_colorDivisions[colorDivisionCounter].ColorItems.Count > 0)
                {
                    ColorItem existingColorItem = m_colorDivisions[colorDivisionCounter].GetExistingColorItem(nColor);
                    if (existingColorItem != null)
                    {
                        existingColorItem.ColorQuantityCount++;
                    }
                    else
                    {
                        colorItem.ColorQuantityCount++;
                        m_colorDivisions[colorDivisionCounter].ColorItems.Add(colorItem);
                    }
                }
                else
                {
                    colorItem.ColorQuantityCount++;
                    m_colorDivisions[colorDivisionCounter].ColorItems.Add(colorItem);
                }

                m_colorDivisions[colorDivisionCounter].TotalColorCount++;
                m_colorDivisions[colorDivisionCounter].BoardNumbers.Add(m_boardNumbers.Count - 1);
                
                boardMaxCheckerCounter++;
                boardCounter++;
                if (boardMaxCheckerCounter >= Bitmap.Height)
                {
                    boardMaxCheckerCounter = 0;
                } 
            }
        }
    }
    
    private static float Normalize(float current, float min, float max)
    {
        return (current - min) / (max - min);
    }
}
