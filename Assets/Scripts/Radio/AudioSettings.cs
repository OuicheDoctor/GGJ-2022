using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Audio Settings", fileName = "NewAudioSettings")]
public class AudioSettings: ScriptableObject
{
    public List<RadioChannel> RadioChannels => _radioChannels;

    [SerializeReference] private List<RadioChannel> _radioChannels; 
}
