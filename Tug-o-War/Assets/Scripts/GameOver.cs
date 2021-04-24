using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public float sinkDelay;

    public Animator animator;

    public GameObject panel;


    void Start()
    {
        StartCoroutine(SinkSequence());
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("Main");
        }
    }

    IEnumerator SinkSequence()
    {
        yield return new WaitForSeconds(sinkDelay);
        animator.SetBool("Sink", true);
        yield return new WaitForSeconds(2);
    }

    public void Sunk()
    {
        panel.SetActive(true);
    }
}
