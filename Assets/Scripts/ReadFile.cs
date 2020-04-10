using UnityEngine;
using System.IO;
using System.Collections.Generic;

/*public enum Language
{
    russian,
    english
};*/

public class ReadFile : MonoBehaviour
{
    public string path;
    //public Language language;
    [TextArea(1, 10)]
    public List<string> quotes;

    void Awake()
    {
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach(var v in lines)
            {
                if (v != "") quotes.Add(v);
            }
        }
    }
}
