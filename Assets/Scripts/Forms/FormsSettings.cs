using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FormsSettings", menuName = "GGJ/FormsSettings")]
public class FormsSettings : ScriptableObject
{
    [SerializeField] private int _questionCountPerAxis = 3;
    [SerializeField] private List<FormQuestion> _formQuestions;

    public int QuestionCountPerAxis => _questionCountPerAxis;
    public List<FormQuestion> FormQuestions => _formQuestions;
}

[Serializable]
public class FormQuestion
{
    [SerializeField] private MBTITrait _associatedTrait;
    [SerializeField] private string _question;
    [SerializeField] private string _value0Response;
    [SerializeField] private string _value1Response;
    [SerializeField] private string _neutralResponse;

    public MBTITrait AssociatedTrait => _associatedTrait;
    public string Question => _question;
    public string Value0Response => _value0Response;
    public string Value1Response => _value1Response;
    public string NeutralResponse => _neutralResponse;
    public string[] AllResponses => new string[] { _value0Response, _value1Response, _neutralResponse };
}

public enum MBTITrait
{
    ExtravertiIntraverti,
    SensationIntuition,
    PenseeSentiments,
    JugementPerception
}