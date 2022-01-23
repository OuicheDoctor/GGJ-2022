using UnityEngine;

namespace GGJ.Hobbies
{
    [CreateAssetMenu(fileName = "HobbyData", menuName = "GGJ/Hobbies/Hobby Data")]
    public class HobbyData : ScriptableObject
    {
        [SerializeField]
        public string hobbyName = "";
        
        [SerializeField]
        public string description = "";

        [SerializeField]
        public CategoryData category;
    }
}
