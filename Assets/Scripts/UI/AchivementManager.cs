using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class LovePolaroidData
{
    [SerializeField] private int _timestamp;
    [SerializeField] private Sprite _couple;
    [SerializeField] private string _nameA;
    [SerializeField] private string _nameB;
    [SerializeField] private LoveStatus _status;

    public LovePolaroidData(string nameA, string nameB, LoveStatus status)
    {
        _timestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        _nameA = nameA;
        _nameB = nameB;
        _status = status;
        _couple = null;
    }

    public DateTime At
    {
        get
        {
            return DateTimeOffset.FromUnixTimeSeconds(_timestamp).DateTime;
        }
    }
    public Sprite Couple => _couple;
    public string NameA => _nameA;
    public string NameB => _nameB;

    public LoveStatus Status => _status;

    public new virtual string ToString()
    {
        return $"{NameA} & {NameB} at {At.ToShortDateString()} {At.ToLongTimeString()}";
    }

}

public class LovePolaroidCollection
{
    private int _size;
    [SerializeField] private List<LovePolaroidData> _polaroids;
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

    public LovePolaroidCollection()
    {
        _size = 18;
        Clear();
    }

    public void Clear()
    {
        _polaroids = new List<LovePolaroidData>();
        _filter = LoveStatus.All;
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

    public void Reset()
    {
        _offset = 0;
        _filter = LoveStatus.All;
    }

    private List<LovePolaroidData> WithFilter
    {
        get
        {
            if (_filter == LoveStatus.All)
            {
                return _polaroids.ToList();
            }
            return _polaroids.GroupBy(e => e.Status).FirstOrDefault(e => e.Key == _filter).ToList();
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
        var items = WithFilter.GetRange(_offset * _size, currentSize < _size ? currentSize : _size);
        items.ForEach(i => Debug.Log(i.Status));
        return items;
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
    const string PLAYER_PREF_NAME = "achievements";

    [SerializeField] private GameplaySettings _settings;


    public LovePolaroidCollection Collection => _collection;


    // Start is called before the first frame update
    void Start()
    {
        _collection = new LovePolaroidCollection();
        Load();
        Refresh(true);
    }

    public void Refresh(bool reset = false)
    {
        if (reset)
        {
            _collection.Reset();
        }

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

    public void Load()
    {
        var json = PlayerPrefs.GetString(PLAYER_PREF_NAME);
        if (json != "")
        {
            var data = JsonUtility.FromJson<LovePolaroidCollection>(json);
            _collection = data;
        }
        else
        {
            _collection = new LovePolaroidCollection();
        }
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(_collection);
        PlayerPrefs.SetString(PLAYER_PREF_NAME, json);
        PlayerPrefs.Save();
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
        UIManager.Instance.ResetFilterButtons(false, true, true, true);
        Refresh();
    }


    public void OnButtonHeartFilterClick()
    {
        ToggleFilter(LoveStatus.Heart);
        UIManager.Instance.ResetFilterButtons(true, false, true, true);
        Refresh();
    }

    public void OnButtonBrokenHeartFilterClick()
    {
        ToggleFilter(LoveStatus.HeartBrock);
        UIManager.Instance.ResetFilterButtons(true, true, false, true);
        Refresh();
    }

    public void OnButtonSkullFilterClick()
    {
        ToggleFilter(LoveStatus.Skull);
        UIManager.Instance.ResetFilterButtons(true, true, true, false);
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
        UIManager.Instance.LovePolaroids[index].gameObject.SetActive(true);
        polaroid.CurrentStatus = data.Status;
        polaroid.Label = $"{data.NameA} & {data.NameB}\n{data.At.ToShortDateString()}";
    }

    private void DeactivatePolaroid(int index)
    {
        UIManager.Instance.LovePolaroids[index].gameObject.SetActive(false);
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
