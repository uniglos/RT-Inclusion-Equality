using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour {
    public TMP_Text textMeshPro;
    public string wordToHighlight;
    public Color highlightColor = Color.red;

    void Start() {
        if (textMeshPro == null) {
            textMeshPro = GetComponent<TMP_Text>();
        }

        HighlightWord();
    }

    void Update() {
    }

    void HighlightWord() {
        string text = textMeshPro.text;
        string[] words = text.Split(' ');

        int startIndex = 0;
        foreach (string word in words) {
            if (word.Equals(wordToHighlight)) // Check if the current word matches the wordToHighlight
            {
                int wordIndex = text.IndexOf(word, startIndex);
                if (wordIndex >= 0) {
                    int wordLength = word.Length;

                    for (int j = wordIndex; j < wordIndex + wordLength; j++) {
                        Debug.Log(j);
                        if (j < textMeshPro.textInfo.characterCount) // Check if j is a valid character index
                        {
                            ChangeColor(textMeshPro, j, highlightColor);
                        }
                    }

                    startIndex = wordIndex + wordLength;
                }
            } else {
                startIndex += word.Length + 1; // Move startIndex to the end of the current word and space
            }
        }
    }

    private void ChangeColor(TMP_Text textMeshPro, int characterIndex, Color color) {
        TMP_CharacterInfo[] charInfos = textMeshPro.textInfo.characterInfo;
        if (characterIndex < 0 || characterIndex >= charInfos.Length) {
            Debug.LogError("Invalid character index");
            return;
        }

        int materialIndex = charInfos[characterIndex].materialReferenceIndex;
        int vertexIndex = charInfos[characterIndex].vertexIndex;
        Color32[] colors = textMeshPro.textInfo.meshInfo[materialIndex].colors32;
        colors[vertexIndex + 0] = color;
        colors[vertexIndex + 1] = color;
        colors[vertexIndex + 2] = color;
        colors[vertexIndex + 3] = color;
        textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}
