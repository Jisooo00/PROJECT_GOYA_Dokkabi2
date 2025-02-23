using System;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "New LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [Serializable]
    public class localizationData
    {
        public string Key;
        public string KOR;
        public string ENG;
    }
    public List<localizationData> localizationList;
}