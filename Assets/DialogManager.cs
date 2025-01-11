using System.Collections;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Image npcSplash;
    [SerializeField] private TextMeshProUGUI tmpDialogWindow;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float writingSlower = 0.05f;
    public bool waitingForAnswer;
    private bool isDialogActive; 
    private LizzardNpcSo _npcSo;
    private CardSo _answer;
    private LizzardNpc lizzardNpc;

    private void Start()
    {
        waitingForAnswer = false;
        isDialogActive = false;
        _npcSo = null;
        _answer = null;
        _gameManager = FindObjectOfType<GameManager>();
        
        // Subskrypcja eventu przy starcie
        Subscribe();

        // Dodanie listenera na załadowanie nowej sceny
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Usuwanie listenera przy zniszczeniu obiektu
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ponowna subskrypcja eventów po załadowaniu nowej sceny
        Subscribe();
    }

    private void Subscribe()
    {
        lizzardNpc = FindObjectOfType<LizzardNpc>();
        if (lizzardNpc != null)
        {
            lizzardNpc.OnDialogStarted += StartDialogSequence;
            Debug.Log("Subscribed to OnDialogStarted");
        }
    }

    private void StartDialogSequence(LizzardNpcSo obj)
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

        npcSplash.sprite = _npcSo.splashArt;

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
        waitingForAnswer = false;
        if (_answer == _npcSo.expectedCard)
        {
            yield return GoodAnswer();
        }
        else
        {
            yield return BadAnswer();
        }

        yield return new WaitForSeconds(2f);
        EndDialogSequence();
    }

    private IEnumerator GoodAnswer()
    {
        tmpDialogWindow.text = string.Empty;
        npcSplash.sprite = _npcSo.goodReaction;
        foreach (var character in _npcSo.goodAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
        yield return new WaitForSeconds(2f);
        _gameManager.PassLv();
    }

    private IEnumerator BadAnswer()
    {
        tmpDialogWindow.text = string.Empty;
        npcSplash.sprite = _npcSo.badReaction;
        foreach (var character in _npcSo.badAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }

        if (lizzardNpc != null)
        {
            lizzardNpc.OnDialogStarted -= StartDialogSequence;
        }
        yield return new WaitForSeconds(2f);
        _gameManager.DefeatLv();
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
