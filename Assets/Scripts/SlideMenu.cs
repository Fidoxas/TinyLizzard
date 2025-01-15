using UnityEngine;
using UnityEngine.EventSystems;

public class SlideMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform menuPanel;
    public float slideSpeed = 300f;

    [SerializeField] private Vector2 hiddenPosition;
    private Vector2 visiblePosition;
    [SerializeField] private float range;

    private bool isMouseOver = false;

    void Start()
    {
        visiblePosition = hiddenPosition;
        visiblePosition.y = hiddenPosition.y + range; 
    }

    void Update()
    {
        if (isMouseOver)
        {
            menuPanel.anchoredPosition = new Vector2(menuPanel.anchoredPosition.x, 
                Mathf.Lerp(menuPanel.anchoredPosition.y, visiblePosition.y, Time.deltaTime * slideSpeed));
        }
        else
        {
            menuPanel.anchoredPosition = new Vector2(menuPanel.anchoredPosition.x, 
                Mathf.Lerp(menuPanel.anchoredPosition.y, hiddenPosition.y, Time.deltaTime * slideSpeed));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }
}