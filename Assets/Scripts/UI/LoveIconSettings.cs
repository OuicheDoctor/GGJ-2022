using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class LoveIconData
{
    [SerializeField] LoveStatus _loveStatus = LoveStatus.None;
    [SerializeField] Sprite _icon;

    public LoveStatus LoveStatus => _loveStatus;
    public Sprite Icon => _icon;
}

[CreateAssetMenu(fileName = "LoveIconSettings", menuName = "GGJ/LoveIconSettings")]
public class LoveIconSettings : ScriptableObject
{

    [SerializeField] List<LoveIconData> _icons;

    public List<LoveIconData> Icons => _icons;


    public Sprite GetSprite(LoveStatus status)
    {
        return _icons.Find(e => e.LoveStatus == status).Icon;
    }
}
