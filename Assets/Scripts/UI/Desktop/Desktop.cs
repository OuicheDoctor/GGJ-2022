using UnityEngine;

public class Desktop : MonoBehaviour
{
    [SerializeField] private DropZone _dropZone;

    private void Start()
    {
        _dropZone.OnDropCallback += go =>
        {
            if (go.GetComponent<PostIt>() != null)
            {
                var dndComp = go.GetComponent<DragAndDroppable>();
                dndComp.ValidDrop = true;
                go.transform.SetParent(transform);
                dndComp.TargetGraphic.raycastTarget = true;
            }
        };
    }
}