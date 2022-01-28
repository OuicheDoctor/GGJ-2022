using DG.Tweening;
using UnityEngine;

public class PostItHolder : MonoBehaviour
{
    [SerializeField] private DragAndDroppable _postItPrefab;
    [SerializeField] private Transform _dragContainer;
    [SerializeField] private float _returnDuration = .2f;
    [SerializeField] private Ease _returnEasing = Ease.InOutQuad;

    public DragAndDroppable ActivePostIt { get; private set; }

    private void Start()
    {
        SpawnPostIt();
    }

    private void SpawnPostIt()
    {
        var postIt = Instantiate(_postItPrefab, transform);
        postIt.OnPointerDownCallback += () =>
        {
            postIt.transform.SetParent(_dragContainer);
        };

        postIt.OnPointerUpCallback += wasDragged =>
        {
            if (!wasDragged)
                postIt.transform.SetParent(transform);
        };

        postIt.OnBeginDragCallback += () =>
        {
            SpawnPostIt();
        };

        postIt.OnEndDragCallback += validDrop =>
        {
            if (!validDrop)
            {
                postIt.transform.DOMove(transform.position, _returnDuration)
                .SetEase(_returnEasing)
                .OnComplete(() =>
                {
                    postIt.transform.SetParent(transform);
                    if (ActivePostIt != postIt)
                        Destroy(ActivePostIt.gameObject);
                    ActivePostIt = postIt;
                });
            }
        };
        ActivePostIt = postIt;
    }
}