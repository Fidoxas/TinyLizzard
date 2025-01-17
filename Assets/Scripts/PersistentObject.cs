using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            Destroy(this.gameObject);
        }
    }
}