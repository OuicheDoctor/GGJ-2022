using UnityEngine;

namespace GGJ.Hobbies
{
    [CreateAssetMenu(fileName = "CategoryData", menuName = "GGJ/Hobbies/Category Data")]
    public class CategoryData : ScriptableObject
    {
        [SerializeField]
        public string categoryName = "";

        [SerializeField]
        public string description = "";
    }
}
