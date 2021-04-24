using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public float speed;
    Animator animator;

    void Start()
    {
        GetComponent<Animator>().speed = speed;
    }
}
