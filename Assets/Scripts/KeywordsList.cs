using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class KeywordsList : ScriptableObject
{
    [SerializedDictionary ("Keyword","Meaning")]
    public SerializedDictionary<string, string> keywords;
}
