using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "LizzardNPCSo", menuName = "Scriptable Objects/LizzardNPCSo")]
    public class LizzardNPCSo : ScriptableObject
    {
        [SerializeField] public string monolog;
        [SerializeField] public string badAnswerLog;
        [SerializeField] public string goodAnswerLog;

        [SerializeField] public CardSo expectedCard;
        [SerializeField] public Image badReaction;
        [SerializeField] public Image goodReaction;
        [SerializeField] public Image splashArt;
    }
}