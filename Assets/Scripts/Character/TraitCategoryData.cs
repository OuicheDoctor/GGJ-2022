using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Character
{
    [CreateAssetMenu(menuName = "GGJ 2022 Game/List of Trait Category", fileName = "TraitCategories")]
    public class TraitCategoryData : ScriptableObject
    {
        public List<TraitCategory> traitCategories = new List<TraitCategory>();
    }
}
