using UnityEngine;
using TMPro;

public class HighScoreUpdate : MonoBehaviour
{
    public TextMeshPro text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int highScore = PlayerPrefs.GetInt("Score", 0);
        if (highScore > 0)
            text.text = $"High Score: {highScore}m";
        else
            text.enabled = false;
    }
}
