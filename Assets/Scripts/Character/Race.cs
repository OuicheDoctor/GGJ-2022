using System;
using UnityEngine;

namespace GGJ.Characters {

    [Serializable]
    public class Race: IRace
    {

        #region Exposed API

        public string Name { get { return _name; } set { _name = value; } }

        #endregion

        #region Inspector Fields

        [SerializeReference] private string _name;

        #endregion
    }
}
