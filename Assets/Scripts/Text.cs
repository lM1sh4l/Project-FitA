using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class Text : MonoBehaviour
{

    public TextMeshProUGUI TextBox;
    string[] arry = { "HI, Player", "I'm Sally" };
    int i;


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
        foreach (char letter in arry[i])
        {
            TextBox.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        i++;
    }
}
