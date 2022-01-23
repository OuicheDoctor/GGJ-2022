using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Hobbies;
using GGJ.Races;

[CreateAssetMenu(menuName = "GGJ/Gameplay Settings", fileName = "NewGameplaySettings")]
public class GameplaySettings : ScriptableObject
{
    #region Exposed API

    public List<RaceData> Races { get => _races; set => _races = value; }
    public List<HobbyData> Hobbies { get => _hobbies; set => _hobbies = value; }

    #endregion

    #region Inspector Fields

    [SerializeReference] private List<RaceData> _races;
    [SerializeReference] private List<HobbyData> _hobbies;

    #endregion
}
