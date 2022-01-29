using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class LovePolaroidData
{
    [SerializeField] private string _date;
    [SerializeField] private Sprite _couple;
    [SerializeField] private string _nameA;
    [SerializeField] private string _nameB;
    [SerializeField] private LoveStatus _status;

    public LovePolaroidData(string nameA, string nameB, LoveStatus status)
    {
        _date = DateTime.Now.ToShortDateString();
        _nameA = nameA;
        _nameB = nameB;
        _status = status;
        _couple = null;
    }

    public string Date => _date;
    public Sprite Couple => _couple;
    public string NameA => _nameA;
    public string NameB => _nameB;

    public LoveStatus Status => _status;
}

public class LovePolaroidCollection
{
    private int _size;
    private List<LovePolaroidData> _polaroids;
    private int _offset;
    private LoveStatus _filter;

    public int Count
    {
        get { return WithFilter.Count; }
    }

    public bool FirstPage
    {
        get { return _offset <= 0; }
    }

    public LoveStatus Filter
    {
        get { return _filter; }
        set { _filter = value; }
    }

    public int Total
    {
        get { return (int)Math.Ceiling((decimal)(Count / _size)); }
    }


    public bool LastPage
    {
        get
        {
            return _offset == Total;
        }
    }

    public bool contains(LoveStatus status)
    {
        return _polaroids.Any(e => e.Status == status);
    }

    public int Offset => _offset;

    public int Size => _size;

    public LovePolaroidCollection(List<LovePolaroidData> data)
    {
        _size = 18;
        _polaroids = data;
        _offset = 0;
        _filter = LoveStatus.All;
    }

    public void Clear()
    {
        _polaroids.Clear();
        _offset = 0;
    }

    public void Next()
    {
        _offset++;
        if (Count == 0)
        {
            _offset = 0;
        }
        else if (_offset > Total)
        {
            _offset = Total;
        }
    }

    public void Previous()
    {
        _offset--;
        if (_offset < 0)
        {
            _offset = 0;
        }
    }

    public void GotFirstPage()
    {
        _offset = 0;
    }

    private List<LovePolaroidData> WithFilter
    {
        get
        {
            if (_filter == LoveStatus.All)
            {
                return _polaroids;
            }
            return _polaroids.Where(p => p.Status == _filter).ToList();
        }
    }

    public List<LovePolaroidData> GetProlaroids()
    {
        var count = WithFilter.Count;
        if (count < _size)
        {
            return WithFilter;
        }
        var currentSize = count - (_offset * _size);
        return WithFilter.GetRange(_offset * _size, currentSize < _size ? currentSize : _size);
    }

    public void Add(LovePolaroidData data)
    {
        _polaroids.Add(data);
    }

}

public class AchivementManager : MonoBehaviour
{

    public static AchivementManager Instance { get; private set; }
    private LovePolaroidCollection _collection;

    [SerializeField] private GameplaySettings _settings;


    public LovePolaroidCollection Collection => _collection;


    // Start is called before the first frame update
    void Start()
    {
        _collection = new LovePolaroidCollection(_settings.LovePolaroids);
        Refresh();
    }

    public void Refresh()
    {
        UIManager.Instance.EnableAchivementsButtons(
            !_collection.FirstPage,
            !_collection.LastPage,
            _collection.contains(LoveStatus.MegaHeart) || _collection.Filter == LoveStatus.MegaHeart,
            _collection.contains(LoveStatus.Heart) || _collection.Filter == LoveStatus.Heart,
            _collection.contains(LoveStatus.HeartBrock) || _collection.Filter == LoveStatus.HeartBrock,
            _collection.contains(LoveStatus.Skull) || _collection.Filter == LoveStatus.Skull
        );
        UpdatePolaroids();
    }


    public void OnButtonPreviousClick()
    {
        _collection.Previous();
        Refresh();
    }

    public void OnButtonNextClick()
    {
        _collection.Next();
        Refresh();
    }

    private void ToggleFilter(LoveStatus status)
    {
        _collection.Filter = _collection.Filter == status ? LoveStatus.All : status;
    }

    public void OnButtonHighScoreClick()
    {

    }

    public void OnButtonMegaHeartFilterClick()
    {
        ToggleFilter(LoveStatus.MegaHeart);
        Refresh();
    }


    public void OnButtonHeartFilterClick()
    {
        ToggleFilter(LoveStatus.Heart);
        Refresh();
    }

    public void OnButtonBrokenHeartFilterClick()
    {
        ToggleFilter(LoveStatus.HeartBrock);
        Refresh();
    }

    public void OnButtonSkullFilterClick()
    {
        ToggleFilter(LoveStatus.Skull);
        Refresh();
    }

    public void UpdatePolaroids()
    {
        var polaroids = _collection.GetProlaroids();
        foreach (var polaroid in polaroids)
        {
            UpdatePolaroid(polaroids.IndexOf(polaroid), polaroid);
        }
        for (var index = polaroids.Count; index < _collection.Size; index++)
        {
            DeactivatePolaroid(index);
        }
    }

    private void UpdatePolaroid(int index, LovePolaroidData data)
    {
        var polaroid = UIManager.Instance.LovePolaroids[index];
        polaroid.Unlock = true;
        polaroid.Visible = true;
        polaroid.CurrentStatus = data.Status;
        polaroid.Label = $"{data.NameA} & {data.NameB}\n{data.Date}";
    }

    private void DeactivatePolaroid(int index)
    {
        UIManager.Instance.LovePolaroids[index].Visible = false;
    }

    public void ClearAchivements()
    {
        _collection.Clear();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
