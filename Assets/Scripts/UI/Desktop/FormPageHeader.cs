using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormPageHeader : MonoBehaviour
{
    public Image pic;
    public TextMeshProUGUI nameField;
    public TextMeshProUGUI regionField;
    public TextMeshProUGUI raceField;
    public TextMeshProUGUI hobbysField;

    public void Setup(Sprite image, string name, string region, string rage, string hobbys)
    {
        pic.sprite = image;
        nameField.text = name;
        regionField.text = region;
        raceField.text = rage;
        hobbysField.text = hobbys;
    }
}