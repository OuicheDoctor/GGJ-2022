using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Character
{
    [CreateAssetMenu(menuName = "GGJ 2022 Game", fileName = "Races")]
    public class RaceData : ScriptableObject
    {
        public List<Race> races = new List<Race>();
    }
}
