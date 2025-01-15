using System;
using SO;
using UnityEngine;
using UnityEngine.Serialization;

public class LizzardNpc : MonoBehaviour
{
    [SerializeField] public LizzardNpcSo lizzardNpcSo;

    public event Action<LizzardNpcSo> OnDialogStarted;
        public void StartDialog()
    {
        OnDialogStarted?.Invoke(lizzardNpcSo);
    }
}
