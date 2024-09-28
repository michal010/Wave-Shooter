using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField]
    TMP_Text ScoreText;

    public void SetScoreText(int score)
    {
        ScoreText.text = $"score: {score}"; 
    }
}
