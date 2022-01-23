using System;
using UnityEngine;

namespace GGJ.Character {

    [Serializable]
    public class Race: IRace
    {
        [SerializeReference] private string _name;

        public string Name { get { return _name; } set { _name = value; } }
    }
}
