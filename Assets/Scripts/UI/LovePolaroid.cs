using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum LoveStatus
{
    MegaHeart = 0x0,
    Heart = 0x1,
    None = 0x2,
    HeartBrock = 0x3,
    Skull = 0x4,
    All = 0x5,
}

public class LovePolaroid : MonoBehaviour
{
    [SerializeField] private LoveIconSettings _settings;
    [SerializeField] private LoveStatus _currentStatus;
    [SerializeReference] private Image _icon;
    [SerializeReference] private Image _couple;
    [SerializeReference] private Image _hiddenOverlay;
    [SerializeReference] private TextMeshProUGUI _label;
    [SerializeField] private bool _unlock;
    [SerializeField] private bool _visible;

    public Image Icon => _icon;
    public LoveStatus CurrentStatus { get { return _currentStatus; } set { SetStatus(value); } }
    public bool Unlock { get { return _unlock; } set { SetUnlock(value); } }
    public bool Visible { get { return _visible; } set { SetVisible(value); } }
    public Image HiddenOverlay => _hiddenOverlay;
    public Image Couple { get { return _couple; } set { _couple = value; } }
    public string Label { get { return _label.text; } set { _label.text = value; } }

    void Start()
    {
        SetStatus(LoveStatus.All);
        SetVisible(true);
        SetUnlock(false);
    }

    private void SetStatus(LoveStatus status)
    {
        _currentStatus = status;
        _icon.sprite = _settings.GetSprite(_currentStatus);
        if (_currentStatus == LoveStatus.None)
        {
            _icon.gameObject.SetActive(false);
        }
    }

    private void SetUnlock(bool value)
    {
        _unlock = value;
        HiddenOverlay.gameObject.SetActive(!_unlock);
    }

    private void SetVisible(bool value)
    {
        _visible = value;
        gameObject.SetActive(_visible);
    }

}
