using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] NavigationFlagChaser flagChaser;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] private GameObject playerFlag;
    [SerializeField] private GameObject enemyFlag;

    [SerializeField] private int numberOfPlayerFlags;
    [SerializeField] private int numberOfEnemyFlags;

    [SerializeField] private List<GameObject> flagSpawnpoints;
    private List<int> extractedSpawnpoints;
    private int random;

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

    private void InstantiatePlayerFlags()
    {
        for(int i = 0; i < numberOfPlayerFlags; i++)
        {
            CheckExtractedNumber();
            Instantiate(playerFlag, flagSpawnpoints[random].transform);
        }
    }

    private void CheckExtractedNumber()
    {
        random = Random.Range(0, flagSpawnpoints.Count);
        if (extractedSpawnpoints.Contains(random))
        {
            CheckExtractedNumber();
        }
        extractedSpawnpoints.Add(random);
    }
}
