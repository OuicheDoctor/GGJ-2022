using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIFormQuestion : MonoBehaviour
{

    [Header("Question Elements")]
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _value0ResponseText;
    [SerializeField] private TextMeshProUGUI _value1ResponseText;
    [SerializeField] private TextMeshProUGUI _neutralResponseText;
    [SerializeField] private Image _response0CheckImage;
    [SerializeField] private Image _response1CheckImage;
    [SerializeField] private Image _responseNeutralCheckImage;

    // Fill all fields of the form question item
    public void FillFields(FormResponse formResponse)
    {
        _questionText.text = formResponse.QuestionReference.Question;
        _value0ResponseText.text = formResponse.QuestionReference.Value0Response;
        _value1ResponseText.text = formResponse.QuestionReference.Value1Response;
        _neutralResponseText.text = formResponse.QuestionReference.NeutralResponse;

        // TODO: Change UI visual return later
        _response0CheckImage.color = formResponse.Response == formResponse.QuestionReference.Value0Response ? Color.green : Color.gray;
        _response1CheckImage.color = formResponse.Response == formResponse.QuestionReference.Value1Response ? Color.green : Color.gray;
        _responseNeutralCheckImage.color = formResponse.Response == formResponse.QuestionReference.NeutralResponse ? Color.green : Color.gray;
    }
}
