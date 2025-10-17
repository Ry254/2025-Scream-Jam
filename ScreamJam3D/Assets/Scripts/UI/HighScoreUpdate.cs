using UnityEngine;
using TMPro;

public class HighScoreUpdate : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int highScore = PlayerPrefs.GetInt("Score", 0);
        if (highScore > 0)
            text.text = $"HIGH SCORE: {highScore}M";
        else
            text.enabled = false;
    }
}
