using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Characters
{
    [Serializable]
    public class Character : ICharacter
    {

        #region Exposed API

        public string Name { get => name; set => name = value; }
        public Sprite Drawing { get => drawing; set => drawing = value; }
        public Race Race { get => race; set => race = value; }
        public bool TraitEI { get => traitEI; set => traitEI = value; }
        public bool TraitSN { get => traitSN; set => traitSN = value; }
        public bool TraitTF { get => traitTF; set => traitTF = value; }
        public bool TraitJP { get => traitJP; set => traitJP = value; }

        #endregion

        #region Inspector fields

        [SerializeReference] private string name;
        [SerializeReference] private Sprite drawing;
        [SerializeReference] private Race race;
        [SerializeReference] private bool traitEI;
        [SerializeReference] private bool traitSN;
        [SerializeReference] private bool traitTF;
        [SerializeReference] private bool traitJP;

        #endregion
    }
}

