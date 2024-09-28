using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlePool : MonoBehaviour
{
    ObjectPool<ParticleSystem> _pool;
    private ParticleSystem _ps;

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();

        ParticleSystem.MainModule main = _ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void SetPool(ObjectPool<ParticleSystem> pool)
    {
        _pool = pool;
    }

    private void OnParticleSystemStopped()
    {
        _pool.Release(_ps);
    }
}
