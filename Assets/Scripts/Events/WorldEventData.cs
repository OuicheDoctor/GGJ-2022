using GGJ.Characters;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "World Event Data", menuName = "GGJ/World Event Data")]
public abstract class WorldEventData : ScriptableObject
{
    [SerializeField] protected float _probability;
    [SerializeField] protected string _headline;
    [SerializeField] protected Sprite _picture;
    [SerializeField] protected AudioClip _announcement;

    public float Probability => _probability;
    public string Headline => _headline;
    public Sprite Picture => _picture;
    public AudioClip Announcement => _announcement;

    public abstract WorldEventType Type { get; }

    public abstract void FixGeneration(List<Character> characters);

    public abstract int ImpactOnScore(ICharacter mateA, ICharacter mateB, int initialScoring);
}

public enum WorldEventType
{
    LutteDeRaces,
    Affrontements,
    Criminalite,
    Natalite,
    AmourInterdit,
    Diversite,
    RegionDesertee,
    Celebrite,
    Ennemi
}