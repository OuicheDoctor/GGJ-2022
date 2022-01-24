using System.Collections.Generic;
using GGJ.Hobbies;
using GGJ.Races;
using UnityEngine;

namespace GGJ.Characters
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public RaceData Race { get; set; }
        public bool TraitEI { get; set; }
        public bool TraitSN { get; set; }
        public bool TraitTF { get; set; }
        public bool TraitJP { get; set; }
        public IList<HobbyData> Hobbies { get; }

        public bool GetMBTITrait(MBTITrait axis);
    }
}