using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraMainController : MonoBehaviour
{
    public static CameraMainController Instance;
    [SerializeField] private GameObject m_mainCamera;
    [SerializeField] private Button m_maximizeButton;
    [SerializeField] private CameraMinimizeButton[] m_cameraMinimizeButtons;
    [SerializeField] private Button m_restartButton;

    private GameObject m_minimizeCamera;
    private CameraMinimizeButton m_currentCameraMinimizeButton;
    public CameraMinimizeButton CurrentCameraMinimizeButton => m_currentCameraMinimizeButton;

    private int m_isDoneCounter;

    public void UpdateIsDoneCounter()
    {
        m_isDoneCounter++;
        if (m_isDoneCounter >= m_cameraMinimizeButtons.Length)
        {
            m_restartButton.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        m_maximizeButton.onClick.AddListener(OpenMainCamera);
        Instance = this;

        m_maximizeButton.image.DOFade(0f, 0f);
    }

    public void InitColorDivisions()
    {
        for(int i=0; i<TestTexture2DDetector.Instance.ColorDivisions.Count; i++)
        {
            m_cameraMinimizeButtons[i].AssignedColorDivision = TestTexture2DDetector.Instance.ColorDivisions[i];
        }
    }

    public void ClickMinimizeController(GameObject p_minimizeCamera, Vector2 p_respawnPoint, CameraMinimizeButton p_cameraMinimizeButton)
    {
        m_currentCameraMinimizeButton = p_cameraMinimizeButton;
        m_mainCamera.SetActive(false);
        m_minimizeCamera = p_minimizeCamera;
        
        foreach (CameraMinimizeButton cameraMinimizeButton in m_cameraMinimizeButtons)
        {
            cameraMinimizeButton.ShowOff();
        }
        
        TouchInputDragController.Instance.ResetAnimator();
        TouchInputDragController.Instance.gameObject.SetActive(true);
        TouchInputDragController.Instance.transform.position = new Vector3(p_respawnPoint.x, p_respawnPoint.y, -2f);
        
        m_maximizeButton.image.DOFade(1f, 0.5f);
    }

    public void OpenMainCamera()
    {
        if (m_minimizeCamera != null)
        {
            m_minimizeCamera.SetActive(false);
        }

        m_mainCamera.SetActive(true);

        foreach (CameraMinimizeButton cameraMinimizeButton in m_cameraMinimizeButtons)
        {
            cameraMinimizeButton.ShowOn();
        }
        
        ColorButtonsController.Instance.gameObject.SetActive(false);
        TouchInputDragController.Instance.gameObject.SetActive(false);
        m_maximizeButton.image.DOFade(0f, 0.5f);
    }
}
