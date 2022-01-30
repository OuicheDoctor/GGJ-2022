using System;
using UnityEngine;

[Serializable]
public class RadioJingle
{
    public int Hour => _hour;
    public AudioClip Audio => _audio;

    [SerializeField] private int _hour;
    [SerializeField] private AudioClip _audio;
}
