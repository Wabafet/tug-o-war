using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayPhase : Phase
{
    public float shellArc;
    public int connectingCount;
    public float shellSpeed;
    public float pelletSpeed;
    public int pelletCount;

    float connectingArc;
    float pelletArc;
    float[] shellStart;
    int[] shellCount;

    void Awake()
    {
        shellStart = new float[] { -90 + 90 % shellArc, -90 + (90 - shellArc * 0.5f) % shellArc };
        shellCount = new int[] { Mathf.FloorToInt((90 - shellStart[0]) / shellArc) + 1, Mathf.FloorToInt((90 - shellStart[1]) / shellArc) + 1 };
        connectingArc = shellArc / (connectingCount + 1);
        pelletArc = 180f / (pelletCount + 1);
    }

    protected override IEnumerator SequenceA()
    {
        for (int j = 0; j < 2; j++)
        {
            FireShells(j);
            yield return new WaitForSeconds(5);
        }
    }

    protected override IEnumerator SequenceB()
    {
        for (int j = 0; j < 2; j++)
        {
            FireShells(j);
            yield return new WaitForSeconds(0.4f);
            FireConnectingCircles(j, Random.value < 0.5f);
            yield return new WaitForSeconds(4);
        }
    }

    protected override IEnumerator SequenceC()
    {
        for (int j = 0; j < 2; j++)
        {
            FirePellets();
            yield return new WaitForSeconds(3);
            FireShells(j);
            yield return new WaitForSeconds(4);
        }
    }

    void FireShells(int config)
    {
        for (int i = 0; i < shellCount[config]; i++)
            ShootShell(shellStart[config] + i * shellArc);
    }

    void FireConnectingCircles(int shellConfig, bool config)
    {
        int shell = 0;
        if (config)
            shell += 1;
        for (; shell < shellCount[shellConfig]; shell += 2)
        {
            float angle = shellStart[shellConfig] + shell * shellArc;
            for (int i = 1; i <= connectingCount; i++)
            {
                ShootCircle(angle + i * connectingArc);
            }
        }
    }

    void FirePellets()
    {
        for (int i = 0; i <= pelletCount; i++)
            ShootPellet(-90 + i * pelletArc);
    }



    void ShootShell(float angle) => Pirate.ShootBullet(Bullet.Shell, angle, shellSpeed);
    void ShootCircle(float angle) => Pirate.ShootBullet(Bullet.Circle, angle, shellSpeed);
    void ShootPellet(float angle) => Pirate.ShootBullet(Bullet.Pellet, angle, pelletSpeed);
}
