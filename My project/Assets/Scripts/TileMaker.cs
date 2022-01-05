using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMaker : MonoBehaviour
{
    public static TileMaker Instance;
    [SerializeField] private int m_rows = 5;
    [SerializeField] private int m_columns = 10;
    [SerializeField] private float m_tileSize = 0.1f;

    [SerializeField] private GameObject m_referenceTile;

    [SerializeField] private List<TileItem> m_tileItems = new List<TileItem>();
    public List<TileItem> TileItems => m_tileItems;

    private int m_count;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateGrid()
    {
        m_rows = TestTexture2DDetector.Instance.Width;
        m_columns = TestTexture2DDetector.Instance.Height;

        ProcessGenerateGrid();
        CameraMainController.Instance.InitColorDivisions();
    }

    private void ProcessGenerateGrid()
    {
        int counter = 0;
        for (int i = 0; i < m_rows; i++)
        {
            for (int j = 0; j < m_columns; j++)
            {
                GameObject tile = Instantiate(m_referenceTile, transform);
                tile.GetComponent<TileItem>().UpdateTextCount(TestTexture2DDetector.Instance.BoardNumbers[counter], 
                    TestTexture2DDetector.Instance.Color[TestTexture2DDetector.Instance.BoardNumbers[counter] - 1]);
                counter++;

                float posX = i * m_tileSize;
                float posY = j * -m_tileSize;
                
                tile.transform.localPosition = new Vector3(posX, posY, 0f);
                m_tileItems.Add(tile.GetComponent<TileItem>());
            }
        }
        
        CameraMainController.Instance.OpenMainCamera();
        // float gridW = m_columns * m_tileSize;
        // float gridH = m_rows * m_tileSize;
        // transform.position = new Vector2(-gridW / 2 + m_tileSize / 2, gridH / 2 - m_tileSize / 2);
    }
}
