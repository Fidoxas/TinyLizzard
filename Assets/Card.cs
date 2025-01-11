using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public CardSo cardSo;
    [SerializeField] private DialogManager dialogManager;

    private void Awake()
    {
        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }
    }

    public void GiveAnswer()
    {
        if (dialogManager != null)
        {
            dialogManager.GetAnswer(cardSo);
        }
    }
}