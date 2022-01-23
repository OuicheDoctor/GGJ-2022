using System.Collections.Generic;

namespace GGJ.Character
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public IRace Race { get; set; }
        public List<ITrait> Traits { get; set; }
    }
}