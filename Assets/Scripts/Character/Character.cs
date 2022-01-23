using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Character
{
    [Serializable]
    public class Character : ICharacter
    {
        public string Name { get => _name; set => _name = value; }
        public Race Race { get => _race; set => _race = value; }
        public bool TraitEI { get => _traitEI; set => _traitEI = value; }
        public bool TraitSN { get => _traitSN; set => _traitSN = value; }
        public bool TraitTF { get => _traitTF; set => _traitTF = value; }
        public bool TraitJP { get => _traitJP; set => _traitJP = value; }

        [SerializeReference] private string _name;
        [SerializeReference] private Race _race;
        [SerializeReference] private bool _traitEI;
        [SerializeReference] private bool _traitSN;
        [SerializeReference] private bool _traitTF;
        [SerializeReference] private bool _traitJP;
    }
}

