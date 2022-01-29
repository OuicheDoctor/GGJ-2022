using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Radio/Radio Channel", fileName = "NewRadioChannel")]
public class RadioChannel: ScriptableObject
{
    public List<AudioClip> Musics => _musics;
    public List<RadioJingle> jingles => _jingles;

    [SerializeReference] private List<AudioClip> _musics;
    [SerializeField] private List<RadioJingle> _jingles;
}