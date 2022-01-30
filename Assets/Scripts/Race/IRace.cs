using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Races
{
    public interface IRace
    {
        public string Name { get; set; }
        public List<Sprite> Drawings { get; set; }
        public List<string> Names { get; set; }
    }
}