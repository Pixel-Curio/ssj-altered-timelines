using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollingText : MonoBehaviour
{
    private TMP_Text _textComponent;

    public float RolloverCharacterSpeed = 0.1f;

    void Awake()
    {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space)) StartCoroutine(AnimateVertexColors());
    }

    IEnumerator AnimateVertexColors()
    {
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        _textComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = _textComponent.textInfo;

        //Turn off all characters
        int characterCount = textInfo.characterCount;
        for (int i = 0; i < characterCount; i++)
        {
            SetAlpha(textInfo, i, 0);
        }

        _textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        for (int i = 0; i < characterCount; i++)
        {
            SetAlpha(textInfo, i, 255);
            _textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            yield return new WaitForSeconds(0.1f);
        }
    }

    private static void SetAlpha(TMP_TextInfo textInfo, int index, byte alpha)
    {
        // Get the index of the material used by the current character.
        int materialIndex = textInfo.characterInfo[index].materialReferenceIndex;

        // Get the vertex colors of the mesh used by this text element (character or sprite).
        Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

        // Get the index of the first vertex used by this text element.
        int vertexIndex = textInfo.characterInfo[index].vertexIndex;

        // Set new alpha values.
        newVertexColors[vertexIndex + 0].a = alpha;
        newVertexColors[vertexIndex + 1].a = alpha;
        newVertexColors[vertexIndex + 2].a = alpha;
        newVertexColors[vertexIndex + 3].a = alpha;
    }
}
