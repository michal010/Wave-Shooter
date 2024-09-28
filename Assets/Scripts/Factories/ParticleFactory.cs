using UnityEngine;
using UnityEngine.Pool;

public interface IParticleFactory
{
    public ParticleSystem Create();
}

public class ParticleFactory : MonoBehaviour, IParticleFactory
{
    protected ObjectPool<ParticleSystem> pool;
    [SerializeField] ParticleSystem _prefab;

    protected void Awake()
    {
        pool = new ObjectPool<ParticleSystem>(OnCreate, OnTargetPooled, OnTargetReleased, OnDestroyTarget, true, 16);
    }

    public ParticleSystem Create()
    {
        return pool.Get();
    }

    protected virtual ParticleSystem OnCreate()
    {
        var particleSystem = GameObject.Instantiate(_prefab, transform);
        particleSystem.GetComponent<ParticlePool>().SetPool(pool);
        return particleSystem;
    }

    protected virtual void OnTargetPooled(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
    }

    protected virtual void OnTargetReleased(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(false);
    }

    protected virtual void OnDestroyTarget(ParticleSystem particleSystem)
    {
        Destroy(particleSystem.gameObject);
    }
}
