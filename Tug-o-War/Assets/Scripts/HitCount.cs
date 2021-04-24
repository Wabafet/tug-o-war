using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCount : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<TMP_Text>().text = "You were hit " + Player.Hits.ToString() + " times";
    }
}
