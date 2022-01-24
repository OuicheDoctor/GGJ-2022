using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Races {

    [CreateAssetMenu(menuName = "GGJ/Race/Race Data", fileName = "NewRace")]
    public class RaceData : ScriptableObject, IRace
    {
        #region Exposed API

        public string Name { get => _name; set => _name = value; }
        public Sprite Drawing { get => _drawing; set => _drawing = value; }
        public List<string> Names { get => _names.Names; set => _names = new RaceNamesData { Names = value }; }

        #endregion

        #region Inspector Fields

        [SerializeReference] private string _name;
        [SerializeReference] private Sprite _drawing;
        [SerializeReference] private RaceNamesData _names;

        #endregion
    }
}