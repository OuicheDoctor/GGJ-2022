using System;
using UnityEngine;

namespace GGJ.Characters
{
    public interface IRace
    {
        public string Name { get; set; }
        public Sprite Drawing { get; set; }
    }
}