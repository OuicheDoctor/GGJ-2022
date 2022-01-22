using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public string Name { get; set; }
    public IRace Race { get; set; }
    //public GenderEnum Gender { get; set; }
    public List<ITrait> Traits { get; set; }

    public List<ITrait> GetTrait();
}