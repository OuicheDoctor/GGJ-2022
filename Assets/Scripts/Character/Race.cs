using System;
using UnityEngine;

namespace GGJ.Characters {

    [Serializable]
    public class Race: IRace
    {

        #region Exposed API

        public string Name { get { return name; } set { name = value; } }

        #endregion

        #region Inspector Fields

        [SerializeReference] private string name;

        #endregion
    }
}
