using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public VolumeProfile postProcessingProfile;
    Vignette vignette = null;
    Coroutine vignetteCoroutine;

    float charge = 0;
    public float chargeTime = 10;
    float invulnerability;
    public float invulnerabilityTime;

    public static int Hits = 0;

    void Awake()
    {
        Hits = 0;
        if (postProcessingProfile.TryGet<Vignette>(out vignette))
            vignette.intensity.value = 0;
    }

    void OnDisable()
    {
        if (vignette != null)
            vignette.intensity.value = 0;
    }

    void Update()
    {
        if (charge > 0)
        {
            charge -= Time.deltaTime;
            if (charge <= 0)
                vignetteCoroutine = StartCoroutine(FadeVignette());
        }
        if (invulnerability > 0)
            invulnerability -= Time.deltaTime;
    }

    public void Damage()
    {
        if (invulnerability > 0)
        {
            // No damage
        }
        else if (charge <= 0)
        {
            if (vignetteCoroutine != null)
                StopCoroutine(vignetteCoroutine);
            vignette.intensity.value = 0.5f;
            invulnerability = invulnerabilityTime;
            charge = chargeTime;
            Hits += 1;
        }
        else
        {
            //Game over
            SceneManager.LoadScene("Lose");
        }
    }

    IEnumerator FadeVignette()
    {
        while (vignette.intensity.value > 0)
        {
            vignette.intensity.value = Mathf.Clamp01(vignette.intensity.value - 0.5f * Time.deltaTime);
            yield return null;
        }
        vignetteCoroutine = null;
    }
}
