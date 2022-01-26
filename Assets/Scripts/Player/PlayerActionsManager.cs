using GGJ.Characters;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsManager : MonoBehaviour
{
    [SerializeField] private int _folderCount = 4;

    public static PlayerActionsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _storedMatches = new List<ICharacter>[_folderCount];
        for (var i = 0; i < _folderCount; i++)
            _storedMatches[i] = new List<ICharacter>();
    }

    private List<ICharacter>[] _storedMatches;

    public List<ICharacter>[] StoredMatches => _storedMatches;

    public void StoreCharacterInFile(ICharacter character, int folderIndex)
    {
        if (!_storedMatches[folderIndex].Contains(character))
            _storedMatches[folderIndex].Add(character);
    }

    public void RemoveCharacterFromFile(ICharacter character, int folderIndex)
    {
        _storedMatches[folderIndex].Remove(character);
    }

    public void Clear()
    {
        for (var i = 0; i < _folderCount; i++)
            _storedMatches[i].Clear();
    }
}