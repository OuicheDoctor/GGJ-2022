using GGJ.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    public static FormManager Instance { get; private set; }

    [SerializeField] private FormsSettings _settings;

    public List<FormQuestion> GenerateQuestions()
    {
        var list = new List<FormQuestion>();
        FormQuestion pickedQuestion;
        foreach (MBTITrait axis in Enum.GetValues(typeof(MBTITrait)))
        {
            var availableQuestions = _settings.FormQuestions.Where(q => q.AssociatedTrait == axis).ToList();
            for (var i = 0; i < _settings.QuestionCountPerAxis; i++)
            {
                pickedQuestion = availableQuestions.PickOneAndRemove();
                list.Add(pickedQuestion);
            }
        }
        return list;
    }

    public GeneratedForm GenerateFormFor(ICharacter character, List<FormQuestion> questions)
    {
        var form = new GeneratedForm();
        FormResponse currentResponse;
        foreach (var question in questions)
        {
            currentResponse = new FormResponse();
            currentResponse.QuestionReference = question;
            currentResponse.Response = character.GetMBTITrait(question.AssociatedTrait) ? question.Value0Response : question.Value1Response;
            form.Responses.Add(currentResponse);
        }

        foreach (MBTITrait trait in Enum.GetValues(typeof(MBTITrait)))
        {
            currentResponse = form.Responses.Where(r => r.QuestionReference.AssociatedTrait == trait).PickOne();
            currentResponse.Response = new List<string> {
                currentResponse.QuestionReference.Value0Response,
                currentResponse.QuestionReference.Value1Response,
                currentResponse.QuestionReference.NeutralResponse }.PickOne();
        }

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