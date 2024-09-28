using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public interface ITargetSpawner
{
    public Target SpawnTarget();
    public Target SpawnTarget(Vector3 pos);
}

public class TargetSpawner : MonoBehaviour, ITargetSpawner
{
    [SerializeField] protected TargetFactory _targetFactory;
    public TargetFactory TargetFactory { get { return _targetFactory; } }

    public virtual Target SpawnTarget()
    {
        return SpawnTarget(Vector3.zero);
    }

    public virtual Target SpawnTarget(Vector3 pos)
    {
        var target = _targetFactory.Create();
        target.transform.position = pos;
        target.transform.SetParent(transform, false);
        return target;
    }
}

public class RandomSlotTargetSpawer : TargetSpawner
{
    [SerializeField]
    UnityEvent m_onTargetSlotsEmpty = new UnityEvent();
    public UnityEvent onTargetSlotsEmpty
    {
        get { return m_onTargetSlotsEmpty; }
        set { m_onTargetSlotsEmpty = value; }
    }

    Dictionary<Transform, Target> targetSlots;
    Dictionary<Transform, Target> TargetSlots
    {
        get { return targetSlots; }
        set { targetSlots = value; }
    }
    

    private void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        TargetSlots = new Dictionary<Transform, Target>();
        
        foreach (Transform t in transform)
        {
            TargetSlots.Add(t, null);
        }
    }

    Transform GetRandomPoint()
    {
        var validSpawnPoints = TargetSlots.Where(kpv => kpv.Value == null).Select(kpv => kpv.Key).ToList();
        if (validSpawnPoints.Count > 0)
            return validSpawnPoints.ElementAt(Random.Range(0, validSpawnPoints.Count));
        return null;
    }

    public void ClearTargets()
    {
        for (int i = 0; i < TargetSlots.Count; i++)
        {
            if (TargetSlots.ElementAt(i).Value != null)
            {
                Destroy(TargetSlots.ElementAt(i).Value.gameObject);
            }
        }
    }

    public override Target SpawnTarget()
    {
        Vector3 spawnPos = Vector3.zero;
        Transform spawnPoint = GetRandomPoint();
        if(spawnPoint != null)
        {
            spawnPos = spawnPoint.position;
        }
        else
        {
            onTargetSlotsEmpty?.Invoke();
            Debug.Log("target slots empty");
            return null;
        }
        Target target = SpawnTarget(spawnPos);
        target.onTargetClicked.AddListener(HandleTargetClicked);
        TargetSlots[spawnPoint] = target;
        return target;
    }

    void HandleTargetClicked(Target target)
    {
        var key = TargetSlots.Where(kpv => kpv.Value == target).FirstOrDefault().Key;
        TargetSlots[key] = null;
        target.onTargetClicked.RemoveListener(HandleTargetClicked);
    }

    public override Target SpawnTarget(Vector3 pos)
    {
        Target target = base.SpawnTarget(pos);
        return target;
    }

}
