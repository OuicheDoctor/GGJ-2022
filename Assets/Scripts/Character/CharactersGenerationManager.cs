using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGJ.Hobbies;
using GGJ.Races;
using System.Linq;

namespace GGJ.Characters
{

    public class CharactersGenerationManager : MonoBehaviour
    {
        public static CharactersGenerationManager Instance { get; private set; }

        [SerializeField] private GameplaySettings gameplaySettings;

        public List<Character> GenerateCharacters(int count) {
            var races = gameplaySettings.Races;

            var characters = Enumerable.Range(1, count).Select(i => {
                return new Character
                {
                    Hobbies = GetRandomHobbies()
                };
            }).ToList();

            return characters;
        }

        // Get random hobbies respecting the following rules :
        // Max 3 hobbies per character
        // Max 1 hobby from the same category
        private List<HobbyData> GetRandomHobbies()
        {
            var hobbies = gameplaySettings.Hobbies;

            System.Random random = new System.Random();
            var maxHobbies = random.Next(1, 3);

            var remainingHobbies = hobbies;
            var randomHobbies = Enumerable.Range(1, maxHobbies).Select(i => {
                if (remainingHobbies.Count == 0) return null;
                var randomHobbyIndex = random.Next(0, hobbies.Count);
                var selectedHobby = hobbies[randomHobbyIndex];
                remainingHobbies = remainingHobbies.Where(h => h.category != selectedHobby.category).ToList();
                return selectedHobby;
            }).ToList();
            randomHobbies = randomHobbies.Where(h => h != null).ToList();

            return randomHobbies;
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

