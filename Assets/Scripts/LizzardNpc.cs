using System;
using SO;
using UnityEngine;
using UnityEngine.Serialization;

public class LizzardNpc : MonoBehaviour
{
    [SerializeField] private LizzardNPCSo lizzardNpcSo;

    public event Action<LizzardNPCSo> OnDialogStarted;
        public void StartDialog()
    {
        OnDialogStarted?.Invoke(lizzardNpcSo);
    }
}
