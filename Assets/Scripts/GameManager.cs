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
        // SceneManager.sceneLoaded += OnSceneLoaded;
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
        // Pobierz aktualną nazwę sceny
        string currentSceneName = SceneManager.GetActiveScene().name;
    
        // Znajdź indeks aktualnej sceny w liście
        int currentIndex = _scenesNames.IndexOf(currentSceneName);

        if (currentIndex == -1)
        {
            Debug.LogWarning("Aktualna scena nie znajduje się na liście scen!");
            return;
        }

        // Jeśli to ostatnia scena, zakończ grę
        if (currentIndex == _scenesNames.Count - 1)
        {
            WinGame();
            return;
        }

        // Załaduj następną scenę
        string nextSceneName = _scenesNames[currentIndex + 1];
        SceneManager.LoadScene(nextSceneName);
    }
    private void WinGame()
    {
        Debug.Log("Gratulacje, wygrałeś grę!");
    }
}