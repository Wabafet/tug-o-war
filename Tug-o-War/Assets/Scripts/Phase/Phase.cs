using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phase : MonoBehaviour
{
    public Coroutine StartSequence(int level)
    {
        if (level == 0)
            return StartCoroutine(SequenceA());
        else if (level == 1)
            return StartCoroutine(SequenceB());
        else
            return StartCoroutine(SequenceC());
    }

    protected abstract IEnumerator SequenceA();
    protected abstract IEnumerator SequenceB();
    protected abstract IEnumerator SequenceC();
}
