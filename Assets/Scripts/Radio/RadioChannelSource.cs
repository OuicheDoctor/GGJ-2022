using System;
using System.Collections.Generic;
using UnityEngine;

public struct RadioChannelSource
{
    public RadioChannel RadioChannel { get; }
    public AudioSource AudioSource { get; }
    public List<AudioClip> RemainingClips { get; }


    public RadioChannelSource(RadioChannel radioChannel, AudioSource audioSource, List<AudioClip> remainingClips)
    {
        this.RadioChannel = radioChannel;
        this.AudioSource = audioSource;
        this.RemainingClips = remainingClips;
    }
}
