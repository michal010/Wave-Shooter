using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomTargetFactory : TargetFactory
{
    [SerializeField]
    List<TargetData> spawnableTargets = new List<TargetData>();
    public List<TargetData> SpawnableTargets { get { return spawnableTargets; } set { spawnableTargets = value; } }

    private TargetData GetRandomTargetData()
    {
        if(SpawnableTargets.Count == 0)
        {
            Debug.LogError("Spawnable targets list is empty!");
            return null;
        }
        TargetData data = SpawnableTargets.ElementAt(Random.Range(0, SpawnableTargets.Count));
        return data;
    }

    private void Awake()
    {
        base.Awake();
    }

    override protected void OnTargetPooled(Target target)
    {
        target.Setup(GetRandomTargetData());
        base.OnTargetPooled(target);
    }
}
