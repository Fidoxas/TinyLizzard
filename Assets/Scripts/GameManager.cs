using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<string> _scenesNames; 
    [SerializeField] private GameObject cardsPanelParent;
    private Scene _currentScene;

    void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        // Możesz dodać logikę w Update, jeśli jest potrzebna
    }

    public void DefeatLv()
    {
        foreach (Transform child in cardsPanelParent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void PassLv()
    {
        // Załadowanie następnej sceny z listy
        int currentIndex = _scenesNames.IndexOf(_currentScene.name); // Używamy nazwy sceny
        if (currentIndex == _scenesNames.Count - 1)
        {
            WinGame();
            return;
        }

        if (currentIndex >= 0 && currentIndex + 1 < _scenesNames.Count)
        {
            string nextSceneName = _scenesNames[currentIndex + 1];
            SceneManager.LoadScene(nextSceneName); 
            _currentScene = SceneManager.GetActiveScene(); 
        }
        else
        {
            Debug.LogWarning("Brak kolejnej sceny do załadowania.");
        }
    }

    private void WinGame()
    {
        Debug.Log("Gratulacje, wygrałeś grę!");
    }
}