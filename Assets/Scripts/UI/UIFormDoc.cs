using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GGJ.Characters;

public class UIFormDoc : MonoBehaviour
{
    [Header("Forms Elements")]
    [SerializeField] private Image _characterImage;

    public void FillForm(Character character, GeneratedForm form)
    {
        _characterImage.sprite = character.Race.Drawing;
    }
}
