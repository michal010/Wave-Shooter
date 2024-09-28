using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public interface ITarget
{
    public UnityEvent<Target> onTargetClicked { get; set; }
}

public class Target : MonoBehaviour, ITarget
{
    protected TargetData _data;
    public TargetData Data => _data;
    Material mat;
    Color assignedColor;
    public Color AssignedColor => assignedColor;

    MovementModifier movementModifier;
    ObjectPool<Target> _pool;
    
    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        assignedColor = new Color(Random.value, Random.value, Random.value);
        mat.color = assignedColor;
    }

    UnityEvent<Target> m_onTargetClicked = new UnityEvent<Target>();
    public UnityEvent<Target> onTargetClicked
    {
        get { return m_onTargetClicked; }
        set { m_onTargetClicked = value;}
    }

    public void SetPool(ObjectPool<Target> pool)
    {
        _pool = pool;
    }

    public virtual void Setup(TargetData data)
    {
        _data = data;
        transform.localScale = data.Scale;
        assignedColor = new Color(Random.value, Random.value, Random.value);
        mat.color = assignedColor;
        
        if (movementModifier != null)
            Destroy(movementModifier);
        
        if(data is MovingTargetData movingTargetData)
        {
            switch (movingTargetData.MovementType)
            {
                case MovementTypes.PingPongUpAndDown:
                case MovementTypes.PingPongLeftAndRight:
                    movementModifier = gameObject.AddComponent<PingPongMovement>();
                    movementModifier.Setup(movingTargetData);
                    break;
                case MovementTypes.Circural:
                    movementModifier = gameObject.AddComponent<CircuralMovement>();
                    movementModifier.Setup(movingTargetData);
                    break;
            }
        }

    }

    private void OnMouseDown()
    {
        onTargetClicked?.Invoke(this);
        _pool.Release(this);
    }
}
