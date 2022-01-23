using GGJ.Characters;
using System;
using System.Linq;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    public static FormManager Instance { get; private set; }

    [SerializeField] private FormsSettings _settings;

    public GeneratedForm GenerateFormFor(ICharacter character)
    {
        var form = new GeneratedForm();
        FormQuestion pickedQuestion;
        FormResponse currentResponse;
        bool canBeDecoy = false;
        foreach (MBTITrait axis in Enum.GetValues(typeof(MBTITrait)))
        {
            var availableQuestions = _settings.FormQuestions.Where(q => q.AssociatedTrait == axis).ToList();
            for (var i = 0; i < _settings.QuestionCountPerAxis; i++)
            {
                // If more than half have the correct answer, then the rest can be random
                canBeDecoy = i > Mathf.CeilToInt(_settings.QuestionCountPerAxis / 2f);
                pickedQuestion = availableQuestions.PickOneAndRemove();
                currentResponse = new FormResponse();
                currentResponse.QuestionReference = pickedQuestion;

                if (canBeDecoy)
                    currentResponse.Response = pickedQuestion.AllResponses.PickOne();
                else
                    currentResponse.Response = character.GetMBTITrait(axis) ? pickedQuestion.Value0Response : pickedQuestion.Value1Response;

                form.Responses.Add(currentResponse);
            }
        }

        form.Responses.Shuffle();

        return form;
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