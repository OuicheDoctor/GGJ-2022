using System.Collections.Generic;

namespace GGJ.Character
{
    public interface ICharacter
    {
        public string Name { get; set; }
        public Race Race { get; set; }
        public bool TraitEI { get; set; }
        public bool TraitSN { get; set; }
        public bool TraitTF { get; set; }
        public bool TraitJP { get; set; }
    }
}