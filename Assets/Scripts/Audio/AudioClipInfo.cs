using UnityEngine;
using System.Collections;

public class AudioClipInfo : ScriptableObject
{
    public string Name => _name;
    public AudioClip AudioFile => _audioFile;

    [SerializeReference] string _name;
    [SerializeReference] AudioClip _audioFile;
}
