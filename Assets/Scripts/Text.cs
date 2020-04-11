using UnityEngine;
using System.Collections;
using TMPro;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI TextBox;
    ReadFile rf;

    int i;

    void Awake()
    {
        TextBox.text = "";
        rf = GetComponent<ReadFile>();
        rf.Read();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && i < rf.quotes.Length)
        {
            TextBox.text = "";
            StartCoroutine(ShowText());
        }
    }


    IEnumerator ShowText()
    {
        foreach (char c in rf.quotes[i])
        {
            TextBox.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        ++i;
    }
}