using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Races
{
    public class RaceNamesData: ScriptableObject
    {
        #region Exposed API

        public List<string> Names { get => _names; set => _names = value; }

        #endregion


        #region Inspector Fields

        [SerializeReference] List<string> _names;

        #endregion
    }
}
