using System;
using System.Collections;
using System.Collections.Generic;
using GGJ.Hobbies;
using GGJ.Races;
using UnityEngine;

namespace GGJ.Characters
{
    [Serializable]
    public class Character : ICharacter
    {
        #region Exposed API

        public string Name { get; set; }
        public RaceData Race { get; set; }
        public bool TraitEI { get; set; }
        public bool TraitSN { get; set; }
        public bool TraitTF { get; set; }
        public bool TraitJP { get; set; }
        public IList<HobbyData> Hobbies { get; private set; }
        public string Region { get; set; }


        public Character()
        {
            Hobbies = new List<HobbyData>();
        }

        public bool GetMBTITrait(MBTITrait axis)
        {
            switch (axis)
            {
                case MBTITrait.ExtravertiIntraverti:
                    return TraitEI;

                case MBTITrait.SensationIntuition:
                    return TraitSN;

                case MBTITrait.PenseeSentiments:
                    return TraitTF;

                case MBTITrait.JugementPerception:
                    return TraitTF;

                default:
                    throw new Exception("Unkown trait");
            }
        }

        #endregion Exposed API
    }
}