using System;
using UnityEngine;


namespace GGJ.Characters {

    [CreateAssetMenu(menuName = "GGJ/Race", fileName = "NewRace")]
    public class RaceData : ScriptableObject, IRace
    {
        #region Exposed API

        public string Name { get => _name; set => _name = value; }

        #endregion

        #region Inspector Fields

        [SerializeReference] private string _name;

        #endregion
    }
}