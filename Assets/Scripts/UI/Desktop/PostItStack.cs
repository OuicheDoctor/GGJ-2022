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

    private PostIt _lastDraggedPostIt;

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
            //if (ActivePostIt == postIt)
            //    postIt.transform.SetParent(_dragContainer);
        };

        postIt.DndComp.OnPointerUpCallback += wasDragged =>
        {
            if (ActivePostIt == postIt && !wasDragged)
                postIt.transform.SetParent(transform);
        };

        postIt.DndComp.OnBeginDragCallback += () =>
        {
            postIt.transform.SetParent(_dragContainer);

            if (postIt.transform.localScale.x > 1f)
            {
                postIt.DndComp.MouseDeltaOnGrab = Vector3.zero;
                postIt.transform.localScale = Vector3.one;
            }

            if (ActivePostIt == postIt)
            {
                _lastDraggedPostIt = postIt;
                SpawnPostIt();
            }
            else
                _lastDraggedPostIt = null;
        };

        postIt.DndComp.OnEndDragCallback += validDrop =>
        {
            if (!validDrop)
            {
                postIt.transform.DOMove(postIt.DndComp.StartPosDrag, _returnDuration)
                .SetEase(_returnEasing)
                .OnComplete(() =>
                {
                    postIt.transform.SetParent(postIt.DndComp.StartParent);
                    if (_lastDraggedPostIt == postIt)
                    {
                        if (ActivePostIt != postIt)
                            Destroy(ActivePostIt.gameObject);
                        ActivePostIt = postIt;
                    }

                    var zoomable = postIt.DndComp.StartParent?.GetComponent<Zoomable>();
                    if (zoomable != null && zoomable.Zoomed)
                    {
                        postIt.transform.DOScale(2f, .1f);
                    }
                });
            }
            else if (!postIt.Detached)
            {
                postIt.Detached = true;
            }
        };
        ActivePostIt = postIt;
    }
}