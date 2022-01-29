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
            Dictionary<string, int> _raceCounter = new Dictionary<string, int>();
            for (int i = 0; i < count; i++)
            {
                var (traitEI, traitSN, traitTF, traitJP) = GetRandomTraits();
                var race = GetRandomRace(_raceCounter);
                if (!_raceCounter.ContainsKey(race.Name))
                {
                    _raceCounter.Add(race.Name, 0);
                }
                _raceCounter[race.Name] += 1;
                var region = GetRandomRegion();
                var name = GetRandomNameFromRace(race, ref availableRaceNames, usedNames);
                usedNames.Add(name);

                var hobbies = GetRandomHobbies();

                var character = new Character()
                {
                    Race = race,
                    Name = name,
                    Region = region,
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
            var remainingHobbies = new List<HobbyData>();
            _gameplaySettings.Hobbies
                .Where(h => !_gameplaySettings.StressLess || (!h.stressful && !h.category.stressful))
                .ToList()
                .ForEach(h => remainingHobbies.Add(h));

            var randomHobbies = new List<HobbyData>();
            for (int i = 0; i < _gameplaySettings.HobbiesCountPerCharacter; i++)
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
        private RaceData GetRandomRace(Dictionary<string, int> raceCounter)
        {
            var excludedRaces = raceCounter.Where(e => e.Value >= _gameplaySettings.MaxByRace).Select(e => e.Key).ToList();
            var races = _gameplaySettings.Races.Where(e => !excludedRaces.Contains(e.name)).ToList();
            var randomRaceIndex = random.Next(0, races.Count);
            return races[randomRaceIndex];
        }

        private string GetRandomRegion()
        {
            var regions = _gameplaySettings.Regions;
            var randomRegionIndex = random.Next(0, regions.Count);
            return regions[randomRegionIndex];
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