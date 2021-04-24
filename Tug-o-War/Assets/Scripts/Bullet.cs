using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject circle, diamond, oval, shell, pellet;
    GameObject[] sprites;
    [HideInInspector] public float speed;
    public int type = 0;

    Rigidbody2D body;



    public const float Range = 100;
    public const int Circle = 0;
    public const int Diamond = 1;
    public const int Oval = 2;
    public const int Shell = 3;
    public const int Pellet = 4;

    void Awake()
    {
        sprites = new GameObject[] { circle, diamond, oval, shell, pellet };
        body = GetComponent<Rigidbody2D>();
    }

    public static int LiveCount = 0;
    public static int MaxLiveCount = 0;
    void OnEnable()
    {
        LiveCount += 1;
        /* if (LiveCount > MaxLiveCount)
        {
            MaxLiveCount = LiveCount;
            Helper.Trace("New max live count", MaxLiveCount);
        } */
    }

    void OnDisable()
    {
        LiveCount -= 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer.Equals("Player"))
        {
            other.GetComponent<Player>()?.Damage();
            Recycle();
        }
        else if (layer.Equals("Shell"))
        {
            Recycle();
        }
        else if (layer.Equals("Boundary"))
        {
            Recycle();
        }
    }

    public void Setup(int type, Vector3 position, Quaternion orientation, float speed)
    {
        SetType(type);
        transform.rotation = orientation;
        transform.position = position + transform.forward * 5;
        body.velocity = transform.forward * speed;
    }

    public void SetType(int type)
    {
        gameObject.SetActive(true);
        if (type != this.type && sprites[this.type].activeSelf)
            sprites[this.type].SetActive(false);
        this.type = type;
        sprites[type].SetActive(true);
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }
}
