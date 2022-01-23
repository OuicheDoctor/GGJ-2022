using System;
using UnityEngine;

namespace GGJ.Character
{
    [Serializable]
    public class TraitCategory : ITraitCategory
    {
        public string Name { get => _name; set => _name = value; }
        public string PositiveTrait { get => _positiveTrait; set => _positiveTrait = value; }
        public string NegativeTrait { get => _negativeTrait; set => _negativeTrait = value; }

        [SerializeReference] private string _name;
        [SerializeReference] private string _positiveTrait;
        [SerializeReference] private string _negativeTrait;
    }
}
