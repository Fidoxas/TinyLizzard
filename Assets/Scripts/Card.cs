using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] public CardSo cardSo;
    [SerializeField] private DialogManager dialogManager;

    private void Start()
    {
        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }
        Debug.Log("Powiązano");
    }

    public void GiveAnswer()
    {
        if (dialogManager.waitingForAnswer && dialogManager!= null)
        {
            dialogManager.GetAnswer(cardSo);
            this.gameObject.SetActive(false);
        }
    }
}