using System;
using System.Collections;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Image npcSplash;
    [SerializeField] private TextMeshProUGUI tmpDialogWindow;
    [SerializeField] private float writingSlower = 0.05f;
    public bool waitingForAnswer;
    private bool isDialogActive; 
    private LizzardNPCSo _npcSo;
    private CardSo _answer;
    private LizzardNpc lizzardNpc;

    private void Start()
    {
        waitingForAnswer = false;
        isDialogActive = false;
        _npcSo = null;
        _answer = null;
    }

    private void OnEnable()
    {
        lizzardNpc = FindObjectOfType<LizzardNpc>();
        if (lizzardNpc != null)
        {
            lizzardNpc.OnDialogStarted += StartDialogSequence;
        }
    }

    private void OnDisable()
    {
        if (lizzardNpc != null)
        {
            lizzardNpc.OnDialogStarted -= StartDialogSequence;
        }
    }

    private void StartDialogSequence(LizzardNPCSo obj)
    {
        if (isDialogActive) return; 
        isDialogActive = true;     

        _npcSo = obj;

        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out CanvasRenderer renderer))
            {
                child.gameObject.SetActive(true);
            }
        }

        StartCoroutine(DialogSequence());
    }

    private IEnumerator DialogSequence()
    {
        yield return Dialog1();
        waitingForAnswer = true;

        while (_answer == null)
        {
            yield return null;
        }

        if (_answer == _npcSo.expectedCard)
        {
            yield return GoodDialog();
        }
        else
        {
            yield return BadDialog();
        }

        yield return new WaitForSeconds(2f);
        EndDialogSequence();
    }

    private IEnumerator GoodDialog()
    {
        tmpDialogWindow.text = string.Empty;
        foreach (var character in _npcSo.goodAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
    }

    private IEnumerator BadDialog()
    {
        tmpDialogWindow.text = string.Empty;
        foreach (var character in _npcSo.badAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
    }

    private IEnumerator Dialog1()
    {
        tmpDialogWindow.text = string.Empty;
        foreach (var character in _npcSo.monolog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
    }

    public void GetAnswer(CardSo cardSo)
    {
        if (waitingForAnswer && cardSo != null)
        {
            _answer = cardSo;
        }
    }

    private void EndDialogSequence()
    {
        waitingForAnswer = false;
        _npcSo = null;
        _answer = null;
        isDialogActive = false; 
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out CanvasRenderer renderer))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
