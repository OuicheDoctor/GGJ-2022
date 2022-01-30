using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class AudioClipInfo
{
    public string Name => _name;
    public AudioClip AudioFile => _audioFile;

    [SerializeField] string _name;
    [SerializeField] AudioClip _audioFile;
}
