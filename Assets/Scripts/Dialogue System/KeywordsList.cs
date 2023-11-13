using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordsList : ScriptableObject
{
    [SerializedDictionary ("Keyword", "Meaning")]
    public SerializedDictionary<string, string> keywords;
}
