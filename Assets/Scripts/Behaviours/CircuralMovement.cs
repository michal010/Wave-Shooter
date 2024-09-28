using UnityEngine;

public abstract class MovementModifier : MonoBehaviour
{
    public abstract void Setup(MovingTargetData data);
}

public class CircuralMovement : MovementModifier
{
    float _radius;
    float _speed;

    Vector3 _center;

    private void Start()
    {
        _center = transform.position;
    }

    public override void Setup(MovingTargetData data)
    {
        _radius = data.MovementRadius;
        _speed = data.MovementSpeed;
        angle = 0;
    }

    float angle = 0;
    private void FixedUpdate()
    {
        angle += _speed * Time.deltaTime;
        Vector3 circlePoint = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0)*_radius;
        transform.position = _center + circlePoint;
    }
}
