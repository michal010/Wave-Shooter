using UnityEngine.Events;

public interface IWaveProgression
{
    public void ProceedNextWave();
    public UnityEvent<float> onWaveProgressChanged { get; }
    public UnityEvent<WaveData> onNextWave { get; }
}
