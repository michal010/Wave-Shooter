using UnityEngine;
using UnityEngine.Events;

public interface ITargetController
{
    public UnityEvent<Target> onTargetClicked { get; set; }
    public UnityEvent<Target> onTargetSpawned { get; set; }
}

public class TargetController : MonoBehaviour
{
    [SerializeField]
    UnityEvent<Target> m_onTargetClicked = new UnityEvent<Target>();
    public UnityEvent<Target> onTargetClicked { get { return m_onTargetClicked; } set { m_onTargetClicked = value; } }
    [SerializeField]
    UnityEvent<Target> m_onTargetSpawned = new UnityEvent<Target>();
    public UnityEvent<Target> onTargetSpawned { get { return m_onTargetSpawned; } set { m_onTargetSpawned = value; } }

    [SerializeField] protected TargetSpawner _targetSpawner;

    public float TargetSpawnTime { get; set; }

    public float ElapsedTime { get; set; } = 0;

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
        if(ElapsedTime >= TargetSpawnTime)
        {
            Target t = _targetSpawner.SpawnTarget();
            if (t == null)
                return;
            t.onTargetClicked.AddListener(HandleTargetClicked);
            ElapsedTime = 0;
        }
    }

    void HandleTargetClicked(Target target)
    {
        this.onTargetClicked?.Invoke(target);
        target.onTargetClicked.RemoveListener(HandleTargetClicked);
    }

    public void HandleNextWave(WaveData data)
    {
        TargetSpawnTime = data.TargetSpawnTime;
        if(_targetSpawner.TargetFactory is RandomTargetFactory randomTargetFacotry)
            randomTargetFacotry.SpawnableTargets = data.PossibleTargets;
    }
}
