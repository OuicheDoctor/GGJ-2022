using GGJ.Characters;
using GGJ.Matchmaking;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _leftCharName;
    [SerializeField] private TextMeshProUGUI _rightCharName;
    [SerializeField] private Image _leftPicture;
    [SerializeField] private Image _rightPicture;
    [SerializeField] private TextMeshProUGUI _explanation;
    [SerializeField] private Image _matchResult;
    [SerializeField] private TextMeshProUGUI _matchResultText;

    public void Setup(ICharacter partner1, ICharacter partner2, Rating rangeResult, string resultExplanation = null)
    {
        _leftCharName.text = partner1.Name;
        _leftPicture.sprite = partner1.Skin.Avatar;
        _rightCharName.text = partner2.Name;
        _rightPicture.sprite = partner2.Skin.Avatar;
        _matchResultText.text = rangeResult.Result;
        if (rangeResult.ResultIcon != null)
            _matchResult.sprite = rangeResult.ResultIcon;

        if (!string.IsNullOrEmpty(resultExplanation))
        {
            _explanation.text = resultExplanation;
            _explanation.gameObject.SetActive(true);
        }
    }
}

[Serializable]
public class PictureByResult
{
    public string Result;
    public Sprite Icon;
}