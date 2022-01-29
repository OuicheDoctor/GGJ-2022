using GGJ.Hobbies;
using GGJ.Races;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "GGJ/Gameplay Settings", fileName = "NewGameplaySettings")]
public class GameplaySettings : ScriptableObject
{
    #region Exposed API

    public List<RaceData> Races => _races;
    public int MaxByRace => _maxByRace;
    public int HobbiesCountPerCharacter => _hobbiesCountPerCharacter;
    public List<HobbyData> Hobbies => _hobbies;
    public List<string> Regions => _regions;
    public bool StressLess => _stressLess;

    #endregion

    #region Inspector Fields

    [SerializeReference] private List<RaceData> _races;
    [SerializeField] int _hobbiesCountPerCharacter;
    [SerializeReference] private List<HobbyData> _hobbies;
    [SerializeField] private List<string> _regions;
    [SerializeField] int _maxByRace;
    [SerializeField] bool _stressLess;

    #endregion
}
