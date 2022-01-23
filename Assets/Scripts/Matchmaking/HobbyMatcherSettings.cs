using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Matchmaking
{
    [CreateAssetMenu(fileName = "HobbyMatcherSettings", menuName = "GGJ/Matchmaking/Hobby Matcher Settings")]
    public class HobbyMatcherSettings : ScriptableObject
    {
        [SerializeField]
        public int zeroMatch = 0;

        [SerializeField]
        public int oneMatch = 0;

        [SerializeField]
        public int twoMatch = 0;

        [SerializeField]
        public int threeMatch = 0;    
    }
}
