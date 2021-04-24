using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcPhase : Phase
{
    public float bulletSpeed;
    public float bulletArc;
    public int waves;
    public float timeBetweenWaves;
    [SerializeField] ArcData arcA, arcB, arcC;

    WaitForSeconds waveTime;

    void Awake()
    {
        arcA.Setup();
        arcB.Setup();
        arcC.Setup();
        waveTime = new WaitForSeconds(timeBetweenWaves);
    }

    protected override IEnumerator SequenceA()
    {
        yield return StartCoroutine(ArcSequence(arcA));
    }

    protected override IEnumerator SequenceB()
    {
        yield return StartCoroutine(ArcSequence(arcB));
    }

    protected override IEnumerator SequenceC()
    {
        Coroutine arcSequnce = StartCoroutine(ArcSequence(arcC));
        float halfGap = arcC.gap * 0.5f;
        float halfArc = arcC.arc * 0.5f;
        for (int i = 0; i < waves; i++)
        {
            float angle = arcC.start + arcC.arc + halfGap;
            for (int j = 0; j < 2; j++)
            {
                while (angle > arcC.minStart)
                    angle -= arcC.span;
                for (; angle <= 90; angle += arcC.span)
                    ShootDiamond(angle);
                angle = arcC.start + halfArc;
                yield return waveTime;
            }
        }
        yield return arcSequnce;
    }

    /* IEnumerator ArcSequence(float arc)
    {
        float start = (180 - Mathf.CeilToInt(180 / arc) * arc) * 0.5f - 90;
        for (int i = 0; i < 3; i++)
        {
            float angle = start;
            for (int j = 0; j < 2; j++)
            {
                for (; angle <= 90; angle += 2 * arc)
                    ShootArc(angle, arc);
                angle = start + arc;
                yield return waveTime;
            }
        }
    } */

    IEnumerator ArcSequence(ArcData data)
    {
        float halfSpan = data.span * 0.5f;
        for (int i = 0; i < waves; i++)
        {
            float angle = data.start;
            for (int j = 0; j < 2; j++)
            {
                for (; angle <= 90; angle += data.span)
                    ShootArc(angle, data.arc);
                angle = data.start + halfSpan;
                while (angle > data.minStart)
                    angle -= data.span;
                yield return waveTime;
            }
        }
    }

    void ShootArc(float startAngle, float arc)
    {
        for (float a = 0; a <= arc; a += bulletArc)
            Pirate.ShootBullet(Bullet.Circle, startAngle + a, bulletSpeed);
    }

    void ShootDiamond(float angle) => Pirate.ShootBullet(Bullet.Diamond, angle, bulletSpeed);

    [System.Serializable]
    class ArcData
    {
        public float arc, gap;

        public float span { get; private set; }
        public float start { get; private set; }
        public float minStart { get; private set; }

        public void Setup()
        {
            span = arc + gap;
            start = (180 - Mathf.FloorToInt(180 / span) * span - arc) * 0.5f - 90;
            minStart = span - 90;
        }
    }
}
