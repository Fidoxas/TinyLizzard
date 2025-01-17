using System.Collections.Generic;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> scenesNames; 
    [SerializeField] private GameObject cardsPanelParent;
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private TextMeshProUGUI autogrphCounter;

    private Scene _currentScene;

    public List<LizzardNpcSo> InteractedNpcs = new List<LizzardNpcSo>();
    private int _autographs;
    
    
    
    private float _timeRemaining = 180f; 
    private int _minutes;
    private int _seconds;

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
        _minutes = Mathf.FloorToInt(_timeRemaining / 60);
        _seconds = Mathf.FloorToInt(_timeRemaining % 60);
        UpdateTimerText();
    }

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining < 0)
            {
                _timeRemaining = 0;
            }

            _minutes = Mathf.FloorToInt(_timeRemaining / 60);
            _seconds = Mathf.FloorToInt(_timeRemaining % 60);

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        timer.text = string.Format("{0:D2}:{1:D2}", _minutes, _seconds);
    }

    public void GetAutograph()
    {
        _autographs++;
        autogrphCounter.text = _autographs.ToString();
    }

    private void WinGame()
    {
        Debug.Log("Gratulacje, wygrałeś grę!");
    }

    public void DefeatLv()
    {
        foreach (Transform child in cardsPanelParent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void NextLv()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
    
        int currentIndex = scenesNames.IndexOf(currentSceneName);

        if (currentIndex == -1)
        {
            Debug.LogWarning("Aktualna scena nie znajduje się na liście scen!");
            return;
        }

        if (currentIndex == scenesNames.Count - 1)
        {
            WinGame();
            return;
        }

        string nextSceneName = scenesNames[currentIndex + 1];
        SceneManager.LoadScene(nextSceneName);
    }

    public void PreviousLv()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
    
        int currentIndex = scenesNames.IndexOf(currentSceneName);

        if (currentIndex == -1)
        {
            Debug.LogWarning("Aktualna scena nie znajduje się na liście scen!");
            return;
        }

        if (currentIndex == scenesNames.Count - 1)
        {
            WinGame();
            return;
        }

        string nextSceneName = scenesNames[currentIndex -1];
        SceneManager.LoadScene(nextSceneName);
    }

    public void AddNpcToList(LizzardNpc npc)
    {
        InteractedNpcs.Add(npc.lizzardNpcSo);
    }
}
