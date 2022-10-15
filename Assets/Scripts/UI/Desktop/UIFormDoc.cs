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
    [SerializeField] private RectTransform _questionsAContainer;
    [SerializeField] private RectTransform _questionsBContainer;
    [SerializeField] private RectTransform _questionsCContainer;
    [SerializeField] private RectTransform _questionsDContainer;
    [SerializeField] private DropZone[] _pages;
    [SerializeField] private Zoomable _zoomable;
    [SerializeField] private DragAndDroppable _dndComp;
    [SerializeField] private CanvasGroup _canvasGroup;
    public FormPageHeader[] Headers;

    [Header("Prefabs")]
    [SerializeField] private GameObject _questionItemPrefab;

    public Character Character { get; set; }
    public GeneratedForm Form { get; set; }

    private int _pageIndex = 0;
    private List<Image> _pageGraphics;
    private Dictionary<MBTITrait, RectTransform> _containerPerTrait = new Dictionary<MBTITrait, RectTransform>();

    public void FillForm(Character character, GeneratedForm form)
    {
        _containerPerTrait.Add(MBTITrait.ExtravertiIntraverti, _questionsAContainer);
        _containerPerTrait.Add(MBTITrait.JugementPerception, _questionsBContainer);
        _containerPerTrait.Add(MBTITrait.PenseeSentiments, _questionsCContainer);
        _containerPerTrait.Add(MBTITrait.SensationIntuition, _questionsDContainer);

        Character = character;
        Form = form;

        _characterImage.sprite = character.Skin.Body;
        _characterNameText.text = character.Name;
        _characterRegionText.text = character.Region;
        _characterRaceText.text = character.Race.Name;

        var hobbiesName = (character.Hobbies as List<HobbyData>).ConvertAll(h => h.hobbyName);
        _characterHobbiesText.text = string.Join(", ", hobbiesName);

        foreach (var he in Headers)
        {
            he.Setup(character.Skin.Avatar, character.Name, character.Region, character.Race.Name, _characterHobbiesText.text);
        }

        foreach (FormResponse formResponse in form.Responses)
        {
            var questionItem = Instantiate(_questionItemPrefab);
            questionItem.GetComponent<RectTransform>().SetParent(_containerPerTrait[formResponse.QuestionReference.AssociatedTrait], false);
            questionItem.GetComponent<UIFormQuestion>().FillFields(formResponse);
        }
    }

    public void NextPage()
    {
        if (_pageIndex < _pages.Length - 1)
        {
            _pages[_pageIndex].gameObject.SetActive(false);
            _pageIndex++;
            _pages[_pageIndex].gameObject.SetActive(true);
        }
    }

    public void PreviousPage()
    {
        if (_pageIndex > 0)
        {
            _pages[_pageIndex].gameObject.SetActive(false);
            _pageIndex--;
            _pages[_pageIndex].gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        foreach (var page in _pages)
        {
            page.OnDropCallback += go => ProcessDrop(go, page.transform);
        }
        _pageGraphics = _pages.Select(p => p.GetComponent<Image>()).ToList();

        _dndComp.OnBeginDragCallback += () =>
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        };

        _dndComp.OnEndDragCallback += _ =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        };
    }

    private void ProcessDrop(GameObject go, Transform page)
    {
        var postIt = go.GetComponent<PostIt>();
        if (postIt != null)
        {
            postIt.DndComp.ValidDrop = true;
            postIt.transform.SetParent(page);
            postIt.DndComp.TargetGraphic.raycastTarget = true;
            if (_zoomable.Zoomed)
                postIt.transform.DOScale(2f, .1f);
        }
    }
}