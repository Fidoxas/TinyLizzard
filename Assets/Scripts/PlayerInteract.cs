using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("kontakt");
        var lizzardNpc = other.gameObject.GetComponent<LizzardNpc>();
        if (lizzardNpc!= null)
        {
            lizzardNpc.StartDialog();
        }
        
    }
}
