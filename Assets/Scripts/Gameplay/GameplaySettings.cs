using GGJ.Hobbies;
using GGJ.Races;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "GGJ/Gameplay Settings", fileName = "NewGameplaySettings")]
public class GameplaySettings : ScriptableObject
{
    #region Exposed API

    public List<RaceData> Races => _races;
    public List<HobbyData> Hobbies => _hobbies;
    public List<string> Regions => _regions;
    public bool StressLess => _stressLess;

    #endregion

    #region Inspector Fields

    [SerializeReference] private List<RaceData> _races;
    [SerializeReference] private List<HobbyData> _hobbies;
    [SerializeField] private List<string> _regions;

    [SerializeField] bool _stressLess;

    #endregion
}
