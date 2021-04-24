using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float forwardDrag, reverseDrag;
    public float turnRate;

    Rigidbody2D body;
    Vector2 input = Vector2.zero;
    float speed;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();

    }

    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        Turn(Time.fixedDeltaTime);
        ApplyDrag(Time.fixedDeltaTime);
        Throttle(Time.fixedDeltaTime);
        SetVelocity(Time.fixedDeltaTime);
    }

    void Turn(float dt)
    {
        body.rotation -= input.x * turnRate * dt;
        while (body.rotation >= 360)
            body.rotation -= 360;
        while (body.rotation < 0)
            body.rotation += 360;
    }

    void ApplyDrag(float dt)
    {
        speed -= (speed > 0 ? forwardDrag : -reverseDrag) * speed * speed * dt;
    }

    void Throttle(float dt)
    {
        speed = Mathf.Clamp(speed + input.y * acceleration * dt, -maxSpeed, maxSpeed);
    }

    void SetVelocity(float dt)
    {
        body.velocity = Rotate(new Vector2(0, speed), body.rotation);
    }



    static Vector2 Rotate(Vector2 v, float angle)
    {
        angle = Mathf.Deg2Rad * angle;
        return new Vector2(
            v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle),
            v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle)
        );
    }
}
