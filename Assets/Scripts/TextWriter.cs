using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TextWriter : MonoBehaviour
{
    public TextMeshProUGUI TextBox, Teller;

    FileReader fr;
    ImageManipulator im;
    [SerializeField]
    int i;
    bool clear = true, delayed, writing, auto;
    float delayTimer;

    void Awake()
    {
        fr = GetComponent<FileReader>();
        im = GetComponent<ImageManipulator>();

        TextBox.text = "";
        Teller.text = "";
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
            else if (s.Contains("talker"))
            {
                string[] split = s.Replace("_", " ").Split('=');
                if (split.Length > 1)
                {
                    Character c = im.container.characters.Find(i => i.name == split[1]);
                    if (c != null)
                    {
                        Teller.color = c.color;
                        Teller.text = split[1];
                    }
                }
                else Teller.text = "";
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