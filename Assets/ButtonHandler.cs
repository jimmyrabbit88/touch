using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void setText(string str)
    {
        Text txt = transform.Find("Text").GetComponent<Text>();
        txt.text = str;
    }
    public void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
