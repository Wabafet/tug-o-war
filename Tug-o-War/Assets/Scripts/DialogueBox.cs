using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public float borderSize;
    TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string str)
    {
        text.text = str;
    }
}
