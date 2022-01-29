using UnityEngine;
using UnityEngine.UI;
using GGJ.Characters;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using GGJ.Hobbies;
using DG.Tweening;

public class UIFormDoc : MonoBehaviour
{
    [Header("Forms Elements")]
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterNameText;
    [SerializeField] private TextMeshProUGUI _characterRaceText;
    [SerializeField] private TextMeshProUGUI _characterRegionText;
    [SerializeField] private TextMeshProUGUI _characterHobbiesText;
    [SerializeField] private RectTransform _questionsContainer;
    [SerializeField] private DropZone _dropzone;
    [SerializeField] private Zoomable _zoomable;

    [Header("Prefabs")]
    [SerializeField] private GameObject _questionItemPrefab;

    public Character Character { get; set; }
    public GeneratedForm Form { get; set; }

    public void FillForm(Character character, GeneratedForm form)
    {
        Character = character;
        Form = form;

        _characterImage.sprite = character.Race.Drawing;
        _characterNameText.text = character.Name;
        _characterRegionText.text = character.Region;
        _characterRaceText.text = character.Race.Name;

        var hobbiesName = (character.Hobbies as List<HobbyData>).ConvertAll(h => h.hobbyName);
        _characterHobbiesText.text = string.Join(", ", hobbiesName);

        foreach (FormResponse formResponse in form.Responses)
        {
            var questionItem = Instantiate(_questionItemPrefab);
            questionItem.GetComponent<RectTransform>().SetParent(_questionsContainer, false);
            questionItem.GetComponent<UIFormQuestion>().FillFields(formResponse);
        }
    }

    private void Start()
    {
        _dropzone.OnDropCallback += ProcessDrop;
    }

    private void ProcessDrop(GameObject go)
    {
        var postIt = go.GetComponent<PostIt>();
        if (postIt != null)
        {
            postIt.DndComp.ValidDrop = true;
            postIt.transform.SetParent(transform);
            postIt.DndComp.TargetGraphic.raycastTarget = true;
            if (_zoomable.Zoomed)
                postIt.transform.DOScale(2f, .1f);
        }
    }
}