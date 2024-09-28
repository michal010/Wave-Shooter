using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public interface IPointsHandler
{
    void AddPoints(int amount);
    public int Points { get; }
    public UnityEvent<int> onPointsChanged { get; set; }
}

public class PointsController : MonoBehaviour, IPointsHandler
{
    [FormerlySerializedAs("onPointsChanged")]
    [SerializeField]
    UnityEvent<int> m_onPointsChanged = new UnityEvent<int>();
    public UnityEvent<int> onPointsChanged
    {
        get { return m_onPointsChanged; }
        set { m_onPointsChanged = value; }
    }
    [FormerlySerializedAs("onPointsAdded")]
    [SerializeField]
    UnityEvent<int> m_onPointsAdded = new UnityEvent<int>();
    public UnityEvent<int> onPointsAdded
    {
        get { return m_onPointsAdded; }
        set { m_onPointsAdded = value; }
    }

    public int Points { get; set; } = 0;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        Points = 0;
    }

    public void HandleTargetClicked(Target t)
    {
        AddPoints(t.Data.ScoreValue);
    }

    public void AddPoints(int amount)
    {
        Points += amount;
        m_onPointsAdded?.Invoke(amount);
        onPointsChanged?.Invoke(Points);
    }
}
