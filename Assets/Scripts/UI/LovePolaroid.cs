using System;
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
}

public class LovePolaroid : MonoBehaviour
{
    [SerializeField] private LoveIconSettings _settings;
    [SerializeField] LoveStatus _currentStatus;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _hiddenOverlay;
    [SerializeField] bool _hidden;

    public Image Icon => _icon;
    public LoveStatus CurrentStatus => _currentStatus;
    public bool Hidden => _hidden = false;
    public Image HiddenOverlay => _hiddenOverlay;

    void Start()
    {
        if (_hidden)
        {
            HiddenOverlay.color = GetColor(false);
        }
        else
        {
            HiddenOverlay.color = GetColor(true);
            _icon.color = GetColor(_currentStatus == LoveStatus.None);
            _icon.sprite = _settings.GetSprite(_currentStatus);
        }
    }

    private Color GetColor(bool transparent)
    {
        var color = Color.white;
        color.a = transparent ? 0f : 1f;
        return color;
    }

}
