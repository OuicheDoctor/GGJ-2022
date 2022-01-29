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
        HiddenOverlay.gameObject.SetActive(_hidden);
        _icon.sprite = _settings.GetSprite(_currentStatus);
        if (_currentStatus == LoveStatus.None)
        {
            _icon.gameObject.SetActive(false);
        }

    }

}
