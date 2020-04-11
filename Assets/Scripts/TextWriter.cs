using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI TextBox;

    FileReader fr;
    ImageManipulator im;

    int i;
    bool clear = true, delayed, writing, auto;
    float delayTimer;

    void Awake()
    {
        fr = GetComponent<FileReader>();
        im = GetComponent<ImageManipulator>();

        TextBox.text = "";
        fr.Read();
        im.Initialize();
    }


    void Update()
    {
        if (i < fr.quotes.Length)
        {
            auto = fr.quotes[i].Contains("[");

            if ((Input.GetMouseButtonDown(0) || delayed || auto))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    delayTimer = 0;
                    delayed = false;
                }

                string nextQuote = fr.quotes[i];
                if (!nextQuote.Contains("["))
                {
                    if (!writing) StartCoroutine(ShowText(nextQuote));
                    clear = true;
                    delayed = false;
                }
                else
                {
                    ExecuteKeys(nextQuote);
                }

                auto = false;
            }
        }
    }

    void ExecuteKeys(string quote)
    {
        List<string> attributes = quote.Replace("[", "").Replace(" ", "").Split(']').ToList();
        attributes.ForEach(i => i.Replace("]", ""));
        foreach(var s in attributes)
        {
            if (s.Contains("continue")) clear = false;
            else if (s.Contains("delay"))
            {
                delayTimer = float.Parse(s.Split('=')[1]);
                StartCoroutine(Delay());
            }
            else if (s.Contains("char"))
            {
                im.Manipulate(s);
            }
        }
        ++i;
    }

    IEnumerator Delay()
    {
        while (delayTimer > 0)
        {
            delayTimer -= Time.deltaTime;
            yield return null;
        }
        delayed = true;
    }

    IEnumerator ShowText(string quote)
    {
        ++i;
        if (clear) TextBox.text = "";
        else TextBox.text += " ";

        writing = true;

        foreach (char c in quote)
        {
            TextBox.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        writing = false;
    }
}