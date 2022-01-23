﻿using System;
using UnityEngine;

namespace GGJ.Races
{
    public interface IRace
    {
        public string Name { get; set; }
        public Sprite Drawing { get; set; }
    }
}