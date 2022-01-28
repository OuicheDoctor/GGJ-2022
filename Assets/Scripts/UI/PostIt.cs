using UnityEngine;
using UnityEngine.UI;

public class PostIt : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private DragAndDroppable _dndComp;

    public Image Background => _background;
    public DragAndDroppable DndComp => _dndComp;
}