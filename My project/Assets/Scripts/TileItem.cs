using System;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;

public class TileItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_tileNumberText;
    [SerializeField] private SpriteRenderer m_tileColored;
    [SerializeField] private SpriteRenderer m_grayscaleColor;
    [SerializeField] private BoxCollider m_boxCollider;

    [SerializeField] private HapticTypes m_hapticTypes;

    private int m_tileNumber;
    public int TileNumber => m_tileNumber;

    private void Start()
    {
        //MMVibrationManager.SetHapticsActive();
    }

    public void UpdateTextCount(int p_tileNumber, Color p_tileColor)
    {
        m_tileNumber = p_tileNumber;
        m_tileNumberText.text = p_tileNumber.ToString();
        Color newColor = new Color(p_tileColor.r, p_tileColor.g, p_tileColor.b, 0.1f);
        m_grayscaleColor.color = newColor;
        m_tileNumberText.DOFade(0f,0f);
        m_boxCollider.enabled = false;
    }

    public void SetTileColors(Color p_color)
    {
        m_tileColored.DOColor(p_color, 0.5f);
        m_grayscaleColor.gameObject.SetActive(false);
        m_tileColored.gameObject.SetActive(true);
        m_tileNumberText.enabled = false;
        
        MMVibrationManager.Haptic(m_hapticTypes, false , true , this);
        
        Destroy(m_boxCollider);
    }

    public void ShowBoardNumber()
    {
        if (m_boxCollider != null)
        {
            m_tileNumberText.DOFade(1f, 1f).SetDelay(1f);
            m_boxCollider.enabled = true;
        }
    }
    
    public void HideBoardNumbers()
    {
        if (m_boxCollider != null)
        {
            m_tileNumberText.DOFade(0f, 1f);
            m_boxCollider.enabled = false;
        }
    }

}