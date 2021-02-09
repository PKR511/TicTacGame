using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Dictionary Storage Object")]
public class DictionaryScriptableObject : ScriptableObject
{
    [SerializeField]
    List<string> keys = new List<string>();
    [SerializeField]
    List<TheamData> values = new List<TheamData>();

    public List<string> Keys { get => keys; set => keys = value; }
    public List<TheamData> Values { get => values; set => values = value; }
}//class

[Serializable]
public struct TheamData
{
    public Color backGroundColor;
    public Color boardColor;
}

