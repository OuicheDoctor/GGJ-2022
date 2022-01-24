using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using GGJ.Characters;
using GGJ.Hobbies;
using GGJ.Matchmaking;
using GGJ.Races;
using UnityEngine;

public class PairMatchingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RunATest();
    }

    public static PairMatchingManager Instance { get; private set; }

    private void RunATest2()
    {
        PartenerCollection parteners = new PartenerCollection(2);
        List<ICharacter> characters = new List<ICharacter>();
        var charA = new Character()
        {
            Name = "User 1",
            TraitEI = true,
            TraitSN = true,
            TraitTF = true,
            TraitJP = true,
        };
        var charB = new Character()
        {
            Name = "User 2",
            TraitEI = true,
            TraitSN = true,
            TraitTF = true,
            TraitJP = true,
        };
        characters.Add(charA);
        characters.Add(charB);

        Tuple<ICharacter, ICharacter> key = new Tuple<ICharacter, ICharacter>(charA, charB);
        parteners.Add(key, 200);
        UnityEngine.Debug.Log("Number of pair: " + parteners.Count);

        bool result = parteners.RemoveCharacter(charA);
        UnityEngine.Debug.Log("Removed: " + result);
        if (result)
        {
            parteners.Add(key, 200);
        }
        UnityEngine.Debug.Log("Number of pair: " + parteners.Count);

        result = parteners.RemoveCharacter(charA);
        UnityEngine.Debug.Log("Removed: " + result);
        if (result)
        {
            parteners.Add(key, 200);
        }
        UnityEngine.Debug.Log("Number of pair: " + parteners.Count);

        result = parteners.RemoveCharacter(charA);
        UnityEngine.Debug.Log("Removed: " + result);
        if (result)
        {
            parteners.Add(key, 200);
        }
        UnityEngine.Debug.Log("Number of pair: " + parteners.Count);

        result = parteners.RemoveCharacter(charA);
        UnityEngine.Debug.Log("Removed: " + result);
        if (result)
        {
            parteners.Add(key, 200);
        }
        UnityEngine.Debug.Log("Number of pair: " + parteners.Count);

    }

    private void RunATest()
    {
        System.Random rng = new System.Random();
        List<ICharacter> characters = new List<ICharacter>();
        for (int i = 1; i <= 20; i++)
        {
            characters.Add(new Character()
            {
                Name = "User " + i,
                TraitEI = (rng.Next() % 2) == 0 ? true : false,
                TraitSN = (rng.Next() % 2) == 0 ? true : false,
                TraitTF = (rng.Next() % 2) == 0 ? true : false,
                TraitJP = (rng.Next() % 2) == 0 ? true : false,
            });
        }
        var parteners = BruteForcePairMatching.Instance.Process(characters);

        UnityEngine.Debug.Log("RESULTS:");
        UnityEngine.Debug.Log(String.Format("Number of pair: {0}", parteners.Count));

        foreach (KeyValuePair<Tuple<ICharacter, ICharacter>, int> couple in parteners)
        {
            UnityEngine.Debug.Log(couple.Key.Item1.Name + " x " + couple.Key.Item2.Name + " : " + couple.Value);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
