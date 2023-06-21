using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        int highValue=PlayerPrefs.GetInt("highValue");
        int currValue=PlayerPrefs.GetInt("currValue");
        scoreText.text = "" + currValue;
        if(currValue>highValue){
            PlayerPrefs.SetInt("highValue",currValue);
        }
    }
}
