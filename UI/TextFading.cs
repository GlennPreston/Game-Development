using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFading : MonoBehaviour
{
    private Text text;
    private Color tempColor;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Debug.Log("Fading in");
        while (text.color.a < 1)
        {
            tempColor = text.color;
            tempColor.a += Time.deltaTime / 2;
            text.color = tempColor;
            yield return null;
        }
        yield return null;
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        Debug.Log("Fading out");
        while (text.color.a > 0)
        {
            tempColor = text.color;
            tempColor.a -= Time.deltaTime / 2;
            text.color = tempColor;
            yield return null;
        }
        yield return null;
        StartCoroutine(FadeIn());
    }
}
