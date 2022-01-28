using GGJ.Hobbies;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ.Characters
{
    public class CharactersGenerationManager : MonoBehaviour
    {
        private readonly System.Random random = new System.Random();

        public static CharactersGenerationManager Instance { get; private set; }

        [SerializeField] private GameplaySettings _gameplaySettings;

        // Get a list of characters of a specified length with an associated GeneratedForm
        public List<(Character character, GeneratedForm form)> GenerateCharactersWithForm(int count)
        {
            var characters = GetRandomCharacters(count);
            var charactersWithForm = characters.Select(character => (
                character,
                FormManager.Instance.GenerateFormFor(character)
            )).ToList();

            return charactersWithForm;
        }

        // Get a list of random characters
        private List<Character> GetRandomCharacters(int count)
        {
            var characters = new List<Character>();
            var availableRaceNames = _gameplaySettings.Races.Select(race => (race.Name, race.Names)).ToList();
            List<string> usedNames = new List<string>();

            for (int i = 0; i < count; i++)
            {
                var (traitEI, traitSN, traitTF, traitJP) = GetRandomTraits();
                var race = GetRandomRace();
                var name = GetRandomNameFromRace(race, ref availableRaceNames, usedNames);
                usedNames.Add(name);

                var hobbies = GetRandomHobbies();

                var character = new Character()
                {
                    Race = race,
                    Name = name,
                    TraitEI = traitEI,
                    TraitJP = traitJP,
                    TraitSN = traitSN,
                    TraitTF = traitTF,
                };
                foreach (var hobby in hobbies)
                {
                    character.Hobbies.Add(hobby);
                }
                characters.Add(character);
            }

            return characters;
        }

        // Get random hobbies respecting the following rules :
        // Max 3 hobbies per character
        // Max 1 hobby from the same category
        // Hobby Nothing always alone
        private List<HobbyData> GetRandomHobbies()
        {
            var maxHobbies = random.Next(2, 4);

            var remainingHobbies = new List<HobbyData>();
            foreach (var hobby in _gameplaySettings.Hobbies) { remainingHobbies.Add(hobby); }

            var randomHobbies = new List<HobbyData>();
            for (int i = 0; i < maxHobbies; i++)
            {
                if (remainingHobbies.Count == 0) return null;

                var randomHobbyIndex = random.Next(0, remainingHobbies.Count);
                var selectedHobby = remainingHobbies[randomHobbyIndex];
                remainingHobbies = remainingHobbies.Where(h => h.category != selectedHobby.category).ToList();
                randomHobbies.Add(selectedHobby);
            }

            randomHobbies = randomHobbies.Where(h => h != null).ToList();
            if (randomHobbies.Any(hobby => hobby.name == "Nothing"))
            {
                randomHobbies = randomHobbies.Where(h => h.name == "Nothing").ToList();
            }

            return randomHobbies;
        }

        // Get one random race
        private RaceData GetRandomRace()
        {
            var races = _gameplaySettings.Races;

            var randomRaceIndex = random.Next(0, races.Count);
            return races[randomRaceIndex];
        }

        // Get random name from available names of the provided race.
        // Modify directly the remaining names
        private string GetRandomNameFromRace(RaceData race, ref List<(string raceName, List<string> names)> availableRaceNames, List<string> usedNames)
        {
            var (raceName, names) = availableRaceNames.Find(el => el.raceName == race.Name);
            var randomIndex = random.Next(0, names.Count);
            var name = names.Where(n => !usedNames.Contains(n)).PickOne();
            return name;
        }

        // Get random traits values
        private (bool traitEI, bool traitSN, bool traitTF, bool traitJP) GetRandomTraits()
        {
            return (
                (random.Next() % 2) == 0,
                (random.Next() % 2) == 0,
                (random.Next() % 2) == 0,
                (random.Next() % 2) == 0
            );
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