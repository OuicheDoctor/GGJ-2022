using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ.Races
{
    public interface IRace
    {
        public string Name { get; set; }
        public Sprite Drawing { get; set; }
        public List<string> Names { get; set; }
    }
}