using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Hobbies;
using GGJ.Races;
using System.Linq;
using Newtonsoft.Json;

namespace GGJ.Characters
{

    public class CharactersGenerationManager : MonoBehaviour
    {
        private void Start()
        {
            var characters = GenerateCharacters(12);
            foreach(Character character in characters)
            {
                var hobbiesString = "";
                foreach (var hobby in character.Hobbies) { hobbiesString += hobby.name; };
                Debug.Log($"{character.Race.name} {hobbiesString}");
            }
        }

        public static CharactersGenerationManager Instance { get; private set; }

        [SerializeField] private GameplaySettings gameplaySettings;

        public List<Character> GenerateCharacters(int count) {
            var characters = new List<Character>();
            for(int i = 0; i < count; i++)
            {
                characters.Add(new Character(
                    GetRandomRace(),
                    GetRandomHobbies()
                ));
            }

            return characters;
        }

        // Get random hobbies respecting the following rules :
        // Max 3 hobbies per character
        // Max 1 hobby from the same category
        private List<HobbyData> GetRandomHobbies()
        {
            System.Random random = new System.Random();
            var maxHobbies = random.Next(1, 4);

            var remainingHobbies = new List<HobbyData>();
            foreach (var hobby in gameplaySettings.Hobbies) { remainingHobbies.Add(hobby); }

            var randomHobbies = new List<HobbyData>();
            for (int i = 0; i < maxHobbies; i++)
            {
                System.Random random2 = new System.Random();
                if (remainingHobbies.Count == 0) return null;
                var randomHobbyIndex = random2.Next(0, remainingHobbies.Count);
                var selectedHobby = remainingHobbies[randomHobbyIndex];
                remainingHobbies = remainingHobbies.Where(h => h.category != selectedHobby.category).ToList();
                randomHobbies.Add(selectedHobby);
            }

            randomHobbies = randomHobbies.Where(h => h != null).ToList();

            return randomHobbies;
        }

        // Get one random race
        private RaceData GetRandomRace()
        {
            System.Random random = new System.Random();

            var races = gameplaySettings.Races;

            var randomRaceIndex = random.Next(0, races.Count);
            return races[randomRaceIndex];
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

}

