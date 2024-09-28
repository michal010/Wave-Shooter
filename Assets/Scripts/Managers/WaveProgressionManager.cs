using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class WaveProgressionManager : MonoBehaviour, IWaveProgression
{
    [FormerlySerializedAs("onWaveProgressChagned")]
    [SerializeField]
    UnityEvent<float> m_onWaveProgressChanged = new UnityEvent<float>();
    public UnityEvent<float> onWaveProgressChanged
    {
        get { return m_onWaveProgressChanged; }
        set { m_onWaveProgressChanged = value;}
    }

    [FormerlySerializedAs("onNextWave")]
    [SerializeField]
    UnityEvent<WaveData> m_onNextWave = new UnityEvent<WaveData>();
    public UnityEvent<WaveData> onNextWave
    {
        get { return m_onNextWave; }
        set { m_onNextWave = value;}
    }

    UnityEvent m_onWavesFinished = new UnityEvent();
    public UnityEvent onWavesFinished
    {
        get { return m_onWavesFinished; }
        set { m_onWavesFinished = value; }
    }

    [SerializeField] List<WaveData> waves = new List<WaveData>();
    public List<WaveData> Waves { get { return waves; } set { waves = value; } }
    WaveData currentWave;
    int currentWaveIndex = -1;

    public void SetWaveLevel(int level)
    {
        currentWaveIndex = level;
        currentWave = waves[currentWaveIndex];
        currentWavePoints = 0;
        onNextWave?.Invoke(currentWave);
    }

    public void ProceedNextWave()
    {
        if (currentWaveIndex + 1 < waves.Count())
        {
            currentWaveIndex++;
            currentWave = waves[currentWaveIndex];
            onNextWave?.Invoke(currentWave);
            currentWavePoints = 0;
        }
    }

    int currentWavePoints = 0;
    public void HandlePointsAdded(int points)
    {
        currentWavePoints += points;
        float waveCompletionFrac = ((float)currentWavePoints / (float)currentWave.WaveCompletionScoreRequirement);
        onWaveProgressChanged?.Invoke(waveCompletionFrac);
        if (currentWavePoints >= currentWave.WaveCompletionScoreRequirement)
        {
            ProceedNextWave();
        }
    }
}
