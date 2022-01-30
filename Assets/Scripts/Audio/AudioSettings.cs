﻿using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GGJ/Audio Settings", fileName = "NewAudioSettings")]
public class AudioSettings: ScriptableObject
{
    public List<RadioChannel> RadioChannels => _radioChannels;
    public List<AudioClipInfo> BackgroundMusics => _backgroundMusics;
    public List<AudioClipInfo> SoundEffects => _soundEffects;

    [SerializeReference] private List<RadioChannel> _radioChannels; 
    [SerializeReference] private List<AudioClipInfo> _backgroundMusics;
    [SerializeReference] private List<AudioClipInfo> _soundEffects;
}
