using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] LevelData _levelData;
    [SerializeField] WaveProgressionManager _progressionManager;

    private void Start()
    {
        _progressionManager.Waves = _levelData.Waves;
        _progressionManager.ProceedNextWave();
    }

}
