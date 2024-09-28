using UnityEngine;
using UnityEngine.Pool;

public interface ITargetFactory
{
    public Target Create();
}

public class TargetFactory : MonoBehaviour, ITargetFactory
{
    protected ObjectPool<Target> pool;
    [SerializeField] Target _prefab;

    protected void Awake()
    {
        pool = new ObjectPool<Target>(OnCreate, OnTargetPooled, OnTargetReleased, OnDestroyTarget, true, 16);
    }

    public Target Create()
    {
        return pool.Get();
    }

    protected virtual Target OnCreate()
    {
        var target = GameObject.Instantiate(_prefab, transform);
        target.SetPool(pool);
        return target;
    }

    protected virtual void OnTargetPooled(Target target)
    {
        target.gameObject.SetActive(true);
    }

    protected virtual void OnTargetReleased(Target target)
    {
        target.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyTarget(Target target)
    {
        Destroy(target.gameObject);
    }
}