using DG.Tweening;
using UnityEngine;

public class PostItStack : MonoBehaviour
{
    [SerializeField] private PostIt _postItPrefab;
    [SerializeField] private Transform _dragContainer;
    [SerializeField] private float _returnDuration = .2f;
    [SerializeField] private Ease _returnEasing = Ease.InOutQuad;
    [SerializeField] private Color _postItColor;
    [SerializeField] private GameObject _placeHolder;

    public PostIt ActivePostIt { get; private set; }

    private void Start()
    {
        Destroy(_placeHolder);
        SpawnPostIt();
    }

    private void SpawnPostIt()
    {
        var postIt = Instantiate(_postItPrefab, transform);
        postIt.Background.color = _postItColor;
        postIt.DndComp.OnPointerDownCallback += () =>
        {
            postIt.transform.SetParent(_dragContainer);
        };

        postIt.DndComp.OnPointerUpCallback += wasDragged =>
        {
            if (!wasDragged)
                postIt.transform.SetParent(transform);
        };

        postIt.DndComp.OnBeginDragCallback += () =>
        {
            SpawnPostIt();
        };

        postIt.DndComp.OnEndDragCallback += validDrop =>
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