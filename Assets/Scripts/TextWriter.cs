using UnityEngine;
using System.Collections;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI TextBox;

    FileReader rf;
    ImageManipulator im;

    int i;

    void Awake()
    {
        rf = GetComponent<FileReader>();
        im = GetComponent<ImageManipulator>();

        TextBox.text = "";
        rf.Read();
        im.Initialize();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && i < rf.quotes.Length)
        {
            string nextQuote = rf.quotes[i];
            if (!nextQuote.Contains("["))
            {
                TextBox.text = "";
                StartCoroutine(ShowText(nextQuote));
            }
            else
            {
                switch (nextQuote)
                {
                    case "[continue]":
                        StartCoroutine(ShowText(" " + rf.quotes[i + 1]));
                        break;
                    default:
                        break;
                }
                ++i;
            }
        }
    }


    IEnumerator ShowText(string quote)
    {
        foreach (char c in quote)
        {
            TextBox.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        ++i;
    }
}