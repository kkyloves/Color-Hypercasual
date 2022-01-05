using DG.Tweening;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class CameraMinimizeButton : MonoBehaviour
{

    [SerializeField] private Button m_cameraMinimizeButton;
    [SerializeField] private GameObject m_cameraObj;
    [SerializeField] private Transform m_respawnPoint;

    public ColorDivision AssignedColorDivision { get; set; }
    public bool IsDone { get; set; }

    private void Awake()
    {
        m_cameraObj.SetActive(false);
        m_cameraMinimizeButton.onClick.AddListener(() =>
        {
            m_cameraObj.SetActive(true);
            CameraMainController.Instance.ClickMinimizeController(m_cameraObj, m_respawnPoint.position, this);
            ColorButtonsController.Instance.SetColorButtons(AssignedColorDivision);

            foreach (int tileNumber in AssignedColorDivision.BoardNumbers)
            {
                TileMaker.Instance.TileItems[tileNumber].ShowBoardNumber();
            }
        });
        
        m_cameraMinimizeButton.image.DOFade(0f, 0f);
        m_cameraMinimizeButton.interactable = false;
    }
    
    
    private void FadePlay()
    {
        if (!IsDone)
        {
            m_cameraMinimizeButton.image.DOFade(0.3f, 0.8f).OnComplete(() =>
            {
                m_cameraMinimizeButton.image.DOFade(0.5f, 0.8f).OnComplete(FadePlay);
            });
        }
    }

    public void ShowOff()
    {
        m_cameraMinimizeButton.image.DOKill();

        m_cameraMinimizeButton.image.DOFade(0f, 0.3f);
        m_cameraMinimizeButton.interactable = false;
    }

    public void ShowOn()
    {
        m_cameraMinimizeButton.image.DOKill();
        
        Invoke(nameof(InvokeFadePlay), 1.5f);

        if (AssignedColorDivision != null)
        {
            foreach (int tileNumber in AssignedColorDivision.BoardNumbers)
            {
                TileMaker.Instance.TileItems[tileNumber].HideBoardNumbers();
            }
        }
    }
    

    private void InvokeFadePlay()
    {
        FadePlay();
        m_cameraMinimizeButton.interactable = true;
    }
}
