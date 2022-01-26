using UnityEngine;

public class MatchFolder : MonoBehaviour
{
    [SerializeField] private int _folderIndex;
    [SerializeField] private DropZone _dropZone;

    private void Start()
    {
        _dropZone.OnDropCallback += go =>
        {
            // var form = Go.GetComponent<UIFormDoc>();
            // PlayerActionsManager.Instance.StoreCharacterInFile(form.character, _folderIndex);
        };
    }
}