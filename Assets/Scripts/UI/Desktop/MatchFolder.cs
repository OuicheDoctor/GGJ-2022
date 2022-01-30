using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchFolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private int _folderIndex;
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private Color _focusedColor;
    [SerializeField] private Color _fullColor;
    [SerializeField] private TextMeshProUGUI _fileCount;
    [SerializeField] private int _maxFiles = 2;

    private Color _defaultColor;
    private PlayerActionsManager _playerMgr;
    private List<UIFormDoc> _storedFormDocs = new List<UIFormDoc>();
    private DragAndDroppable _draggedObj = null;

    private void Start()
    {
        _defaultColor = _background.color;
        _dropZone.OnDropCallback += go =>
        {
            var form = go.GetComponent<UIFormDoc>();
            if (form != null && _storedFormDocs.Count < _maxFiles)
                ProcessFormDrop(form, go);
        };
        _playerMgr = PlayerActionsManager.Instance;
    }

    public void Clear()
    {
        _background.color = _defaultColor;
        _storedFormDocs.Clear();
    }

    private void ProcessFormDrop(UIFormDoc form, GameObject droppedObj)
    {
        droppedObj.GetComponent<DragAndDroppable>().ValidDrop = true;
        _playerMgr.StoreCharacterInFile(form.Character, _folderIndex);
        DOTween.Sequence()
            .Append(droppedObj.transform.DOScale(0f, .3f))
            .AppendCallback(() =>
            {
                droppedObj.SetActive(false);
                droppedObj.transform.SetParent(transform);
            })
            .AppendCallback(() => droppedObj.GetComponent<Zoomable>().ResetZoom());

        _storedFormDocs.Add(form);
        if (_storedFormDocs.Count >= _maxFiles)
            _background.color = _fullColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = _storedFormDocs.Count < _maxFiles ? _focusedColor : _fullColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _background.color = _storedFormDocs.Count < _maxFiles ? _defaultColor : _fullColor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_storedFormDocs.Count > 0)
        {
            var last = _storedFormDocs.Last();
            _storedFormDocs.Remove(last);
            _playerMgr.RemoveCharacterFromFile(last.Character, _folderIndex);
            _draggedObj = last.GetComponent<DragAndDroppable>();
            last.gameObject.SetActive(true);
            _draggedObj.OnBeginDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _draggedObj?.OnEndDrag(eventData);
        _draggedObj = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _draggedObj?.OnDrag(eventData);
    }

    private void Update()
    {
        _fileCount.text = $"{_storedFormDocs.Count}/{_maxFiles}";
    }
}