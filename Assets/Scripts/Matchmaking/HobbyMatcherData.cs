using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Matchmaking
{
    [CreateAssetMenu(fileName = "HobbyMatcherData", menuName = "GGJ/Matchmaking/Hobby Matcher Data")]
    public class HobbyMatcherData : ScriptableObject
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
