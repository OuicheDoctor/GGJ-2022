using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Hobbies;
using GGJ.Races;

[CreateAssetMenu(menuName = "GGJ/Data Settings", fileName = "NewDataSettings")]
public class DataSettings : ScriptableObject
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
