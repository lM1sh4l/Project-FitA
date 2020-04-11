using UnityEngine;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System;

public enum Language
{
    russian,
    english
};

public class ReadFile : MonoBehaviour
{
    public string path;
    public Language language;
    [TextArea(1, 10)]
    public string[] quotes;

    void OnValidate()
    {
        TextAsset text = Resources.Load(path) as TextAsset;
        XDocument document = XDocument.Parse(text.text);
        List<XElement> elements = document.Root.Elements("object").ToList();
        XElement element = elements.Find(i => i.Attribute("name").Value == Enum.GetName(typeof(Language), language));
        quotes = element.Value.Split('\n');
        for (int i = 0; i < quotes.Length; ++i)
        {
            int remove = 0;
            for (int j = 0; j < quotes[i].Length; ++j)
            {
                if (char.IsWhiteSpace(quotes[i][j]))
                {
                    ++remove;
                }
                else break;
            }
            quotes[i] = quotes[i].Remove(0, remove);
        }
        quotes = quotes.Where(i => i != "").ToArray();
    }
}