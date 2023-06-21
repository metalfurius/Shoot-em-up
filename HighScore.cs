using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        scoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highValue");
    }
}
