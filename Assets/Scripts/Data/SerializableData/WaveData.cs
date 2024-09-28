using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveData 
{
    public List<TargetData> PossibleTargets;
    [SerializeField]
    public float TargetSpawnTime;
    [SerializeField]
    public int WaveCompletionScoreRequirement;
}
