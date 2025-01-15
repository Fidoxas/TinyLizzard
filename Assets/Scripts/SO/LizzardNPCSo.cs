using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "LizzardNPCSo", menuName = "Scriptable Objects/LizzardNPCSo")]
    public class LizzardNpcSo : ScriptableObject
    {
        [SerializeField] public CardSo expectedCard;
        [SerializeField] public Sprite roomArt;

        
        [SerializeField] public string monolog;
        [SerializeField] public string badAnswerLog;
        [SerializeField] public string goodAnswerLog;

        [SerializeField] public Sprite splashArt;
        [SerializeField] public Sprite badReactionArt;
        [SerializeField] public Sprite goodReactionArt;
        
        [SerializeField] public AudioClip neutralSfx;
        [SerializeField] public AudioClip badReactionSfx;
        [SerializeField] public AudioClip goodReactionSfx;
    }
}

//2 przejscie miedzy scenami poprzez drzwi
//3 timer(2min) i zarzadznie warunkami wygranej (gra sie konczy po rozmowie z każdym lub na koniec czasu)

