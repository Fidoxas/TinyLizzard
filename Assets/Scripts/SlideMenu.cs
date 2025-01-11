using UnityEngine;
using UnityEngine.EventSystems;

public class SlideMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform menuPanel;
    public float slideSpeed = 300f;
    public Vector2 hiddenPosition;
    public Vector2 visiblePosition;

    private bool isMouseOver = false;

    void Start()
    {
        menuPanel.anchoredPosition = hiddenPosition;
    }

    void Update()
    {
        if (isMouseOver)
        {
            menuPanel.anchoredPosition = Vector2.Lerp(menuPanel.anchoredPosition, visiblePosition, Time.deltaTime * slideSpeed);
        }
        else
        {
            menuPanel.anchoredPosition = Vector2.Lerp(menuPanel.anchoredPosition, hiddenPosition, Time.deltaTime * slideSpeed);
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