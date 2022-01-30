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
    public List<WorldEventData> Events => _events;
    public List<WorldEventData> NonChillEvents => _nonChillEvents;

    public List<LovePolaroidData> LovePolaroids => _lovePolaroids;

    #endregion Exposed API

    #region Inspector Fields

    [SerializeReference] private List<RaceData> _races;
    [SerializeField] int _hobbiesCountPerCharacter;
    [SerializeReference] private List<HobbyData> _hobbies;
    [SerializeReference] private List<string> _regions;
    [SerializeReference] private List<LovePolaroidData> _lovePolaroids;
    [SerializeField] int _maxByRace;
    [SerializeField] bool _stressLess;
    [SerializeField] List<WorldEventData> _events;
    [SerializeField] List<WorldEventData> _nonChillEvents;

    #endregion Inspector Fields
}