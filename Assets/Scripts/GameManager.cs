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
        player.score = player.pickedUpFlags * 900;

        if (player.pickedUpFlags == 3)
        {
            player.score += flagChaser.flagsLeft * 2250;
            Debug.Log($"You won. Score: {player.score}");
            EditorApplication.isPaused = true;
        }
        if (flagChaser.flagsLeft == 0)
        {
            Debug.Log($"You lost. Score: {player.score}");
            EditorApplication.isPaused = false;
        }

    }
}
