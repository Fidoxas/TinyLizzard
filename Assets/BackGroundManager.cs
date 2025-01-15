using System;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    private void Start()
    {
         LizzardNpc lizzardNpc = FindObjectOfType<LizzardNpc>();
         GetComponent<Image>().sprite = lizzardNpc.lizzardNpcSo.roomArt;
    }
}
