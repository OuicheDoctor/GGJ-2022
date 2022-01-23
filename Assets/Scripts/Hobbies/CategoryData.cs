using UnityEngine;

namespace GGJ.Hobbies
{
    [CreateAssetMenu(fileName = "CategoryData", menuName = "GGJ/Hobbies/Category Data")]
    public class CategoryData : ScriptableObject
    {
        public string categoryName = "";
        public string description = "";
    }
}
