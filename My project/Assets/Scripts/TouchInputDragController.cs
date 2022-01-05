using System;
using DG.Tweening;
using UnityEngine;

public class TouchInputDragController : MonoBehaviour
{
    public static TouchInputDragController Instance;
    [SerializeField] private SpriteRenderer m_tipShadow;
    [SerializeField] private Material m_colorMaterials;
    [SerializeField] private Animator m_animator;
    [SerializeField] private BoxCollider m_boxCollider;
    private float m_speedModifier = 0.03f;
    private Vector3 m_mouseDownCurrentPosition;

    public int TileNumber { get; private set; }

    public Color TileColor { get; private set; }

    private bool m_canMove = true;

    private float mZCoord;
    private Vector3 moffset;


    private Action m_colorCallback;
    private Action m_deductCallback;

    public void UpdateColorMaterials(Color p_color, int p_tileNumber, Action p_callback)
    {
        TileColor = p_color;
        m_colorMaterials.DOColor(TileColor, 0.3f); 
        TileNumber = p_tileNumber;
        m_tipShadow.DOColor(TileColor, 0.3f);

        m_deductCallback = p_callback;
    }

    public void AnimateCrayon(Action p_callback)
    {
        m_colorCallback = p_callback;
        m_canMove = false;
        m_boxCollider.enabled = false;
        m_animator.Play("JumpCrayon");

        Invoke(nameof(CanMoveNow), 0.35f);
    }

    public void ResetAnimator()
    {
        m_animator.Play("JumpCrayon", -1, 0f);
    }

    public void DoColorCallback()
    {
        m_colorCallback.Invoke();
        m_deductCallback.Invoke();
    }


    private void CanMoveNow()
    {
        m_boxCollider.enabled = true;
        m_canMove = true;
    }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // void OnMouseDown()
    // {
    //     Debug.Log("ON MOUSE DOWN");
    //     screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    //     offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    // }
    //
    // void OnMouseDrag()
    // {
    //     Debug.Log("ON MOUSE DRAG");
    //
    //     Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //
    //     Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    //     transform.position = curPosition;
    //
    // }
    
    void Update()
    {
        if (m_canMove)
        {
            //Simple: replace Input.GetMouseButton(0) to Input.touchCount > 0 and Input.mousePosition to Input.GetTouch(0).position.
            if(Input.touchCount > 0) // 0 for left and 1 for right click
            {
                Touch touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(
                        transform.position.x + touch.deltaPosition.x * m_speedModifier, transform.position.y + touch.deltaPosition.y * m_speedModifier, -2f);
                }
            
            }
            
            
            //MOUSE MODE
            // if(Input.GetMouseButtonDown(0))
            // {
            //    // m_mouseDownCurrentPosition = transform.position;
            //    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            //    // Store offset = gameobject world pos - mouse world pos
            //    moffset = gameObject.transform.position - GetMouseAsWorldPoint();
            // }
            //
            // if (Input.GetMouseButton(0)) // 0 for left and 1 for right click
            // {
            //     if (Input.GetMouseButton(0)) // 0 for left and 1 for right click
            //     {
            //         transform.position = new Vector3(GetMouseAsWorldPoint().x + moffset.x, GetMouseAsWorldPoint().y + moffset.y, -2f);
            //     }
            // }
            
            //TOUCH MODE
            return;
            if (Input.touchCount > 0)
            {
                Debug.Log("1");
                Touch touch = Input.GetTouch(0);
                
                Debug.Log("2");

                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                moffset = gameObject.transform.position - GetTouchAsWorldPoint(touch.position);
                
                if (touch.phase == TouchPhase.Moved) // 0 for left and 1 for right click
                {
                    Debug.Log("3");

                    Vector3 touchWorldPoint = GetTouchAsWorldPoint(touch.position);
                    transform.position = new Vector3(touchWorldPoint.x + moffset.x, touchWorldPoint.y + moffset.y, -2f);
                }
            }
        }


        Vector3 GetMouseAsWorldPoint()
        {
            // Pixel coordinates of mouse (x,y)
            Vector3 mousePoint = Input.mousePosition;
 
            // z coordinate of game object on screen
            mousePoint.z = mZCoord;
 
            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        Vector3 GetTouchAsWorldPoint(Vector3 p_touchPosition)
        {
            // z coordinate of game object on screen
            p_touchPosition.z = mZCoord;
 
            // Convert it to world points
            return Camera.main.ScreenToWorldPoint(p_touchPosition);
        }

    }
}
