using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Character
{
    [CreateAssetMenu(menuName = "GGJ 2022 Game/List of Character", fileName = "Characters")]
    public class CharacterData: ScriptableObject
    {
        public List<Character> characters = new List<Character>();
    }
}
