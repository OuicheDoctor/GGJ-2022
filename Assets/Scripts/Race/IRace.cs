using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Races
{
    public interface IRace
    {
        public string Name { get; set; }
        public List<RaceSkin> Skins { get; set; }
        public List<string> Names { get; set; }
    }
}