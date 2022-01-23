using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Character
{
    [Serializable]
    public class Character : ICharacter
    {
        public string Name { get => _name; set => _name = value; }
        public IRace Race { get => _race; set => _race = value; }
        public Gender Gender { get => _gender; set => _gender = value; }
        public List<ITrait> Traits { get => _traits; set => _traits = value; }

        [SerializeReference] private string _name;
        [SerializeReference] private IRace _race;
        [SerializeReference] private Gender _gender;
        [SerializeReference] private List<ITrait> _traits;
    }
}

