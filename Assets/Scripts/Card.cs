using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Dodajemy tę przestrzeń nazw

public class Card : MonoBehaviour
{
    [SerializeField] public CardSo cardSo;
    [SerializeField] private DialogManager dialogManager;
    private Animator animator;
    private Image _image;

    private void Start()
    {
        if (dialogManager == null)
        {
            dialogManager = FindObjectOfType<DialogManager>();
        }

        animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        Debug.Log("Powiązano");
    }

    public void GiveAnswer()
    {
        if (dialogManager.waitingForAnswer && dialogManager != null)
        {
            dialogManager.GetAnswer(cardSo);
            StartCoroutine(Disappear());

        }
    }

    private IEnumerator Disappear()
    {
       animator.SetTrigger("Poof");
       yield return new WaitForSeconds(1f);
       this.transform.parent.gameObject.SetActive(false);
       gameObject.SetActive(false); 
    }
    private void Dezactivate()
    {
        this.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false); 
    }
}