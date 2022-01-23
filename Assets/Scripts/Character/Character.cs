using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Characters
{
    [Serializable]
    public class Character : ICharacter
    {
        #region Exposed API

        public string Name { get; set; }
        public Race Race { get; set; }
        public bool TraitEI { get; set; }
        public bool TraitSN { get; set; }
        public bool TraitTF { get; set; }
        public bool TraitJP { get; set; }

        #endregion
    }
}

