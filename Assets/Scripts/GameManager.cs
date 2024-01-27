using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] NavigationFlagChaser flagChaser;
    [SerializeField] TextMeshProUGUI scoreText;

    private void Update()
    {
        if (player.pickedUpFlags == 3)
        {
            Debug.Log($"You won. Score: {player.score}");
            EditorApplication.isPlaying = false;
        }
        if (flagChaser.flagsLeft == 0)
        {
            Debug.Log($"You lost. Score: {player.score}");
            EditorApplication.isPlaying = false;
        }
        player.score = player.pickedUpFlags * 900;
        scoreText.text = player.score.ToString();
    }
}
