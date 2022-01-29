using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Radio/Radio Channel", fileName = "NewRadioChannel")]
public class RadioChannel: ScriptableObject
{
    public List<AudioClip> musics;
}