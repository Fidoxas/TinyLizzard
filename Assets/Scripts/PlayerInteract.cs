using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmpWriting; 
    private bool isPlayerInRange = false; 
    private Coroutine waitForInputCoroutine;
    private LizzardNpc currentNpc; 
    private SceneDoors currentDoors;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out LizzardNpc lizzardNpc))
        {
            if (!_gameManager.InteractedNpcs.Contains(lizzardNpc.lizzardNpcSo))
            {
                Debug.Log("interraction");
                SetInteraction("Interact", lizzardNpc, null);
            }
        }
        else if (other.TryGetComponent(out SceneDoors doors))
        {
            Debug.Log("interraction");
            SetInteraction("Open Door", null, doors);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<LizzardNpc>() == currentNpc || other.GetComponent<SceneDoors>() == currentDoors)
        {
            ClearInteraction();
        }
    }

    private void SetInteraction(string message, LizzardNpc npc, SceneDoors doors)
    {
        isPlayerInRange = true;
        currentNpc = npc;
        currentDoors = doors;
        tmpWriting.gameObject.SetActive(true);
        tmpWriting.text = message;
        Debug.Log("Writed");

        if (waitForInputCoroutine == null)
        {
            waitForInputCoroutine = StartCoroutine(WaitForInput());
        }
    }

    private void ClearInteraction()
    {
        isPlayerInRange = false;
        currentNpc = null;
        currentDoors = null;
        tmpWriting.gameObject.SetActive(false);
        tmpWriting.text = "";

        if (waitForInputCoroutine != null)
        {
            StopCoroutine(waitForInputCoroutine);
            waitForInputCoroutine = null;
        }
    }

    private IEnumerator WaitForInput()
    {
        while (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                if (currentNpc != null)
                {
                    Debug.Log("Starting conversation!");
                    currentNpc.StartDialog(); 
                    _gameManager.AddNpcToList(currentNpc);
                    ClearInteraction();
                }
                else if (currentDoors != null)
                {
                    Debug.Log("Opening door!");
                    currentDoors.OpenDoors(); 
                }

                tmpWriting.gameObject.SetActive(false); 
                yield break; 
            }
            yield return null; 
        }
    }
}
