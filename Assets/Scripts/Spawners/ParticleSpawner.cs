using UnityEngine;

public interface IParticleSpawner
{
    public ParticleSystem SpawnParticleSystem();
    public ParticleSystem SpawnParticleSystem(Vector3 pos);
}

public class ParticleSpawner : MonoBehaviour, IParticleSpawner
{
    [SerializeField] protected ParticleFactory _particleFactory;
    public ParticleFactory ParticleFactory { get { return _particleFactory; } }

    public virtual ParticleSystem SpawnParticleSystem()
    {
        return SpawnParticleSystem(Vector3.zero);
    }

    public virtual ParticleSystem SpawnParticleSystem(Vector3 pos)
    {
        var particleSystem = _particleFactory.Create();
        particleSystem.transform.position = pos;
        particleSystem.transform.SetParent(transform, false);
        return particleSystem;
    }
}
