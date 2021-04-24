using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinePhase : Phase
{
    public float bulletCount;
    public float arc;
    public float cycleTime;
    public float streamArc;
    public float bulletSpeed;

    float bulletTick;
    WaitForSeconds bulletTime, ovalTime;
    int halfBulletCount;
    float halfStreamArc;

    float[] streams;

    const float halfPI = Mathf.PI * 0.5f;

    void Awake()
    {
        bulletTick = 2 * Mathf.PI / bulletCount;
        bulletTime = new WaitForSeconds(cycleTime / bulletCount);
        ovalTime = new WaitForSeconds(cycleTime * 0.25f);
        halfBulletCount = Mathf.CeilToInt(bulletCount / 2f);
        halfStreamArc = streamArc * 0.5f;

        int count = Mathf.FloorToInt(90 / streamArc);
        float start = -count * streamArc;
        count = 2 * count + 1;
        streams = new float[count];
        for (int i = 0; i < count; i++)
            streams[i] = start + i * streamArc;
    }

    protected override IEnumerator SequenceA()
    {
        int bullet = 0;
        for (; bullet < halfBulletCount; bullet++)
        {
            ShootDiamond(bullet);
            yield return bulletTime;
        }
        yield return new WaitForSeconds(3);
        for (; bullet < bulletCount; bullet++)
        {
            ShootDiamond(bullet);
            yield return bulletTime;
        }
        yield return new WaitForSeconds(3);
    }

    protected override IEnumerator SequenceB()
    {
        for (int bullet = 0; bullet < bulletCount; bullet++)
        {
            ShootDiamond(bullet);
            yield return bulletTime;
        }
    }

    protected override IEnumerator SequenceC()
    {
        StartCoroutine(OvalSequence());
        for (int bullet = 0; bullet < bulletCount; bullet++)
        {
            ShootDiamond(bullet);
            yield return bulletTime;
        }
    }

    IEnumerator OvalSequence()
    {
        for (int i = 1; i <= 4; i++)
        {
            ShootOval(streams[0] + TriangleSine(halfPI * i) * arc - halfStreamArc);
            yield return ovalTime;
        }
    }

    void ShootDiamond(int bullet)
    {
        float angle = TriangleSine(halfPI + bullet * bulletTick) * arc;
        for (int i = 0; i < streams.Length; i++)
            Pirate.ShootBullet(Bullet.Diamond, angle + streams[i], bulletSpeed);
    }

    void ShootOval(float angle)
    {
        for (int i = 0; i <= streams.Length; i++)
        {
            Pirate.ShootBullet(Bullet.Oval, angle, bulletSpeed);
            angle += streamArc;
        }
    }

    const float a = 1 / 9;
    const float b = 1 / 25;
    const float c = 1 / 49;
    const float d = 1 / 81;
    float TriangleSine(float x)
    {
        return Mathf.Sin(x) - a * Mathf.Sin(3 * x) + b * Mathf.Sin(5 * x) - c * Mathf.Sin(7 * x) + d * Mathf.Sin(9 * x);
    }
}
