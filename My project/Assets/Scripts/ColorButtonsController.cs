using UnityEngine;

public class ColorButtonsController : MonoBehaviour
{
    public static ColorButtonsController Instance;
    [SerializeField] private ColorButtonItem[] m_colorButtonItems;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    

    public void SetColorButtons(ColorDivision p_colorDivision)
    {
        if (p_colorDivision.TotalColorCount <= 0)
        {
            gameObject.SetActive(false);
            TouchInputDragController.Instance.gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            foreach (ColorButtonItem colorButtonItem in m_colorButtonItems)
            {
                colorButtonItem.gameObject.SetActive(false);
            }

            int colorButtonCounter = 0;
            foreach (ColorItem colorItem in p_colorDivision.ColorItems)
            {
                m_colorButtonItems[colorButtonCounter].SetColorButton(colorItem, p_colorDivision, colorButtonCounter.Equals(0));
                m_colorButtonItems[colorButtonCounter].gameObject.SetActive(true);

                colorButtonCounter++;
            }
        }
    }

}
