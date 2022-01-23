using System;

namespace GGJ.Character
{
    public interface ITraitCategory
    {
        public string Name { get; set; }
        public string PositiveTrait { get; set; }
        public string NegativeTrait { get; set; }
    }
}
