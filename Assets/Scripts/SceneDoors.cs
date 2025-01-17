using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SceneDoors : MonoBehaviour
{
    public bool nextScene;
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OpenDoors()
    {
        if (nextScene)
        {
            GoToNextScene();
        }
        else
        {
            GoToPreviousScene();
        }
    }
    void GoToNextScene()
    {
        gameManager.NextLv();
    }
    void GoToPreviousScene()
    {
        gameManager.PreviousLv();
    }
}
