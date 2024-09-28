using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField]
    public Slider ProgressBar;
    [SerializeField]
    public TMP_Text ScoreText;

    public void HandlePointsChanged(int amount)
    {
        ScoreText.text = amount.ToString();
    }

    public void SetWaveProgress(float frac)
    {
        ProgressBar.value = frac;
    }
}
