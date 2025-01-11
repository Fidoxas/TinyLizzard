using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "LizzardNPCSo", menuName = "Scriptable Objects/LizzardNPCSo")]
    public class LizzardNpcSo : ScriptableObject
    {
        [SerializeField] public string monolog;
        [SerializeField] public string badAnswerLog;
        [SerializeField] public string goodAnswerLog;

        [SerializeField] public CardSo expectedCard;
        [SerializeField] public Sprite badReaction;
        [SerializeField] public Sprite goodReaction;
        [SerializeField] public Sprite splashArt;
    }
}