using UnityEngine;

namespace GGJ.Hobbies
{
    [CreateAssetMenu(fileName = "HobbyData", menuName = "GGJ/Hobbies/Hobby Data")]
    public class HobbyData : ScriptableObject
    {
        public string hobbyName = "";
        public string description = "";
        public CategoryData cetegory;
    }
}
