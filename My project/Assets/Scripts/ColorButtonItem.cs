using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_colorCounterText;
    [SerializeField] private TextMeshProUGUI m_colorBoardNumberText;
    [SerializeField] private Button m_colorButton;

    private int m_colorCounter = 0;
    
    public void SetColorButton(ColorItem p_colorItem, ColorDivision p_colorDivision, bool isFirst = false)
    {
        
        m_colorButton.gameObject.SetActive(p_colorItem.ColorQuantityCount > 0);

        m_colorButton.image.color = p_colorItem.Color;
        m_colorCounterText.text = p_colorItem.ColorQuantityCount.ToString();
        m_colorBoardNumberText.text = p_colorItem.BoardMapCount.ToString();

        m_colorButton.onClick.RemoveAllListeners();

        m_colorButton.onClick.AddListener(() =>
        {
            TouchInputDragController.Instance.UpdateColorMaterials(p_colorItem.Color, p_colorItem.BoardMapCount,
                () =>
                {
                    p_colorItem.ColorQuantityCount--;
                    p_colorDivision.TotalColorCount--;

                    if (p_colorDivision.TotalColorCount <= 0)
                    {
                        CameraMainController.Instance.CurrentCameraMinimizeButton.IsDone = true;
                        CameraMainController.Instance.UpdateIsDoneCounter();
                    }

                    m_colorCounterText.text = p_colorItem.ColorQuantityCount.ToString();

                    if (p_colorItem.ColorQuantityCount <= 0)
                    {
                        m_colorButton.gameObject.SetActive(false);
                    }
                });
        });

        if (isFirst)
        {
            m_colorButton.onClick.Invoke();
        }
    }
}
