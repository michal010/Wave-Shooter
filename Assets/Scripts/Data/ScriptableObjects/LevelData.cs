using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/new LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public List<WaveData> Waves;
}
