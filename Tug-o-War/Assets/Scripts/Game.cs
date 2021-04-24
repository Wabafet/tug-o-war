using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Game : MonoBehaviour
{
    Player player;
    Pirate pirate;
    public GameObject[] fog;
    bool fogEnabled = true;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        pirate = FindObjectOfType<Pirate>();
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            fogEnabled = !fogEnabled;
            for (int i = 0; i < fog.Length; i++)
                fog[i].SetActive(fogEnabled);
        }
    }
}
