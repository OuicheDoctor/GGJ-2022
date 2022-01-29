using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PostIt : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private DragAndDroppable _dndComp;
    [SerializeField] private DropZone _dropZone;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private CanvasGroup _canvasGroup;

    public bool Detached { get; set; } = false;
    public Image Background => _background;
    public DragAndDroppable DndComp => _dndComp;

    private void Start()
    {
        _dndComp.OnPointerUpCallback += wasDragged =>
        {
            if (!wasDragged)
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
                _inputField.Select();
                _inputField.ActivateInputField();
                _canvasGroup.interactable = false;
                _canvasGroup.blocksRaycasts = false;
            }
        };

        _dropZone.OnDropCallback += go =>
        {
            if (!Detached)
                return;

            var postIt = go.GetComponent<PostIt>();
            if (postIt != null)
            {
                postIt.DndComp.ValidDrop = true;
                postIt.DndComp.TargetGraphic.raycastTarget = true;
                postIt.transform.SetParent(transform.parent);
            }
        };
    }
}