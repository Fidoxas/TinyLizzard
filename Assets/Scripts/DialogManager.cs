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
    [SerializeField] private GameObject cardsPanel;
    [SerializeField] private GameObject dialogWindow;
    [SerializeField] private float writingSlower = 0.05f;
    [SerializeField] private GameObject enterMarker;

    public bool waitingForAnswer;
    private bool isDialogActive;
    private LizzardNpcSo _npcSo;
    private CardSo _answer;
    private LizzardNpc lizzardNpc;
    private Coroutine blinkingCoroutine;

    private void Start()
    {
        waitingForAnswer = false;
        isDialogActive = false;
        _npcSo = null;
        _answer = null;
        _gameManager = FindObjectOfType<GameManager>();

        Debug.Log("Subscribed");
        Subscribe();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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

        npcSplash.sprite = _npcSo.splashArt;
        npcSplash.gameObject.SetActive(true);
        StartCoroutine(DialogSequence());
    }

    private IEnumerator DialogSequence()
    {
        yield return ShowSplashAndSound();
        yield return WaitForEnter();
        
        yield return Dialog1();
        yield return WaitForEnter();
        
        // 3. Cards panel
        ShowCardsPanel(true);
        yield return WaitForAnswer();
        yield return new WaitForSeconds(1f);
        ShowCardsPanel(false);
        
        if (_answer == _npcSo.expectedCard)
        {
            yield return GoodAnswer();
        }
        else
        {
            yield return BadAnswer();
        }
        yield return WaitForEnter();
        //
        // // End dialog
        EndDialogSequence();
    }

    private IEnumerator ShowSplashAndSound()
    {
        npcSplash.sprite = _npcSo.splashArt;
        // Play sound here if necessary
        yield return null;
    }

    private IEnumerator Dialog1()
    {
        tmpDialogWindow.text = string.Empty;
        dialogWindow.SetActive(true);
        foreach (var character in _npcSo.monolog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
    }

    private void ShowCardsPanel(bool state)
    {
        dialogWindow.SetActive(!state);
        cardsPanel.SetActive(state);
    }

      private IEnumerator GoodAnswer()
    {
        tmpDialogWindow.text = string.Empty;
        npcSplash.sprite = _npcSo.goodReactionArt;
        foreach (var character in _npcSo.goodAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }
        _gameManager.GetAutograph();
        // yield return new WaitForSeconds(2f);
        yield return null;
    }

    private IEnumerator BadAnswer()
    {
        tmpDialogWindow.text = string.Empty;
        npcSplash.sprite = _npcSo.badReactionArt;
        foreach (var character in _npcSo.badAnswerLog)
        {
            tmpDialogWindow.text += character;
            yield return new WaitForSeconds(writingSlower);
        }

        if (lizzardNpc != null)
        {
            lizzardNpc.OnDialogStarted -= StartDialogSequence;
        }
        _gameManager.DefeatLv();
    }

    private IEnumerator Blinking(GameObject marker, float interval)
    {
        bool isVisible = false;

        while (true)
        {
            isVisible = !isVisible;
            marker.SetActive(isVisible);
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator WaitForEnter()
    {
        Debug.Log("czekam na enter");
        blinkingCoroutine = StartCoroutine(Blinking(enterMarker, 0.5f));

        while (!Input.GetButtonDown("Submit"))
        {
            yield return null; 
        }
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
        }

        // Ukryj marker
        enterMarker.SetActive(false);

        Debug.Log("Wciśnięto klawisz Submit!");
    }
    
    private IEnumerator WaitForAnswer()
    {
        waitingForAnswer = true;
        while (_answer == null)
        {
            yield return null;
        }
        waitingForAnswer = false;
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
