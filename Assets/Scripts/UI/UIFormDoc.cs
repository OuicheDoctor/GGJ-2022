using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GGJ.Characters;
using TMPro;

public class UIFormDoc : MonoBehaviour
{
    [Header("Forms Elements")]
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private TextMeshProUGUI _characterRaceText;

    public void FillForm(Character character, GeneratedForm form)
    {
        _characterImage.sprite = character.Race.Drawing;
        _characterNameText.text = character.Name;
        _characterRaceText.text = character.Race.Name;
    }
}
