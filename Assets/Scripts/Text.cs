using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;


/*public enum Language
{
    russian,
    english
};*/

public class Text : MonoBehaviour
{
    public string path;
    //public Language language;
    [TextArea(1, 10)]
    public List<string> quotes;
    public TextMeshProUGUI TextBox;
    string[] text = new string[10];
    int i, t;

    void Awake()
    {
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (var v in lines)
            {
                if (v != "")
                {
                    quotes.Add(v);
                    text[i] = v;
                    i++;

                }
            }
        }

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TextBox.text = "";
            StartCoroutine(ShowText());
        }
    }


    IEnumerator ShowText()
    {
        if (t < i)
        {
            foreach (char letter in text[t])
            {
                TextBox.text += letter;
                yield return new WaitForSeconds(0.02f);

            }

        }
        t++;
    }
}
