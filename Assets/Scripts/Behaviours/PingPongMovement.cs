using UnityEngine;

public class PingPongMovement : MovementModifier
{
    float _radius;
    float _speed;

    Vector3 _center;
    Vector3 posA;
    Vector3 posB;

    MovementTypes _type;
    private void Start()
    {
        _center = transform.position;
        if(_type == MovementTypes.PingPongLeftAndRight)
        {
            posA = new Vector3(_center.x - _radius, _center.y,_center.z);
            posB = new Vector3(_center.x + _radius, _center.y, _center.z);
        }
        else if(_type == MovementTypes.PingPongUpAndDown)
        {
            posA = new Vector3(_center.x, _center.y + _radius, _center.z);
            posB = new Vector3(_center.x, _center.y - _radius, _center.z);

        }
    }

    public override void Setup(MovingTargetData data)
    {
        _radius = data.MovementRadius;
        _speed = data.MovementSpeed;
        _type = data.MovementType;
    }

    private float step = 0f;
    private void FixedUpdate()
    {
        step += _speed * Time.deltaTime;

        float lerpFactor = Mathf.PingPong(step, 1f);

        transform.position = Vector3.Lerp(posA, posB, lerpFactor);
    }
}
