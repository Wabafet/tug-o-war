using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pirate : MonoBehaviour
{
    public static Pirate Instance = null;

    public int testLevel;

    [SerializeField] Bullet bulletPrefab;
    public int phaseRepetitions;
    public int bulletStock;
    [SerializeField] Transform muzzle;
    Bullet[] stock;
    int stockIndex;

    [SerializeField] Phase[] phases;

    public static LayerMask playerLayer, shellLayer, boundaryLayer;

    void Awake()
    {
        CreateStock();
        playerLayer = LayerMask.NameToLayer("Player");
        shellLayer = LayerMask.NameToLayer("Shell");
        boundaryLayer = LayerMask.NameToLayer("Boundary");
    }

    void Start()
    {
        StartCoroutine(Attack());
    }

    void OnEnable()
    {
        if (Instance != null)
            Destroy(Instance);
        Instance = this;
    }

    void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    void CreateStock()
    {
        Transform container = new GameObject("Bullet Container").transform;
        stock = new Bullet[bulletStock];
        for (int i = 0; i < bulletStock; i++)
        {
            stock[i] = Instantiate(bulletPrefab, container);
        }
    }

    public void BeginAttack()
    {
        StartCoroutine(Attack());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    IEnumerator Attack()
    {
        WaitForSeconds wait = new WaitForSeconds(3);
        yield return wait;
        for (int level = 0; level < 3; level++)
        {
            for (int phase = 0; phase < phases.Length; phase++)
            {
                yield return StartCoroutine(RunSequence(phases[phase], level));
                yield return wait;
            }
        }
        yield return new WaitWhile(() => Bullet.LiveCount > 0);
        SceneManager.LoadScene("Win");
    }

    IEnumerator RunSequence(Phase phase, int level)
    {
        for (int i = 0; i < phaseRepetitions; i++)
        {
            yield return phase.StartSequence(level);
        }
    }

    public static Bullet ShootBullet(int type, float angle, float speed)
    {
        Bullet bullet = Instance.GetBullet();
        bullet.Setup(type, Instance.muzzle.transform.position, Instance.muzzle.transform.rotation * Quaternion.Euler(0, angle, 0), speed);
        return bullet;
    }

    public Bullet GetBullet()
    {
        Bullet bullet = stock[stockIndex];
        stockIndex = (stockIndex + 1) % stock.Length;
        return bullet;
    }
}
