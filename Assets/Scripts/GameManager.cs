using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] NavigationFlagChaser flagChaser;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] private GameObject playerFlag;
    [SerializeField] private GameObject enemyFlag;

    [NonSerialized] public List<GameObject> playerFlags = new();
    [NonSerialized] public List<GameObject> enemyFlags = new();

    [SerializeField] private int numberOfPlayerFlags;
    private int enemyPickedUpFlags;

    public List<GameObject> flagSpawnpoints;
    [NonSerialized] public List<int> extractedSpawnpoints = new();
    private int random;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;

    }

    private void Start()
    {
        InstantiatePlayerFlags();
        InstantiateEnemyFlags();
    }

    private void Update()
    {
        player.score = player.pickedUpFlags * 900;

        if (player.pickedUpFlags == 3)
        {
            player.score += flagChaser.flagsLeft * 2250;
            Debug.Log($"You won. Score: {player.score}");
            //EditorApplication.isPaused = true;            -temporarily disabled for build
        }
        if (flagChaser.flagsLeft == 0)
        {
            Debug.Log($"You lost. Score: {player.score}");
            //EditorApplication.isPaused = false;           -temporarily disabled for build
        }
    }

    private void InstantiatePlayerFlags()
    {
        for(int i = 0; i < numberOfPlayerFlags; i++)
        {
            CheckExtractedNumber();
            playerFlags.Add(Instantiate(playerFlag, flagSpawnpoints[random].transform));
        }
    }

    private void InstantiateEnemyFlags()
    {
        for (int i = 0; i < flagChaser.flagsLeft; i++)
        {
            CheckExtractedNumber();
            enemyFlags.Add(Instantiate(enemyFlag, flagSpawnpoints[random].transform));
        }
    }

    public void DestroyLastEnemyPickedUpFlag()
    {
        int enemyFlagIndex = flagSpawnpoints.IndexOf(enemyFlags.Last().transform.parent.gameObject);
        extractedSpawnpoints.Remove(enemyFlagIndex);

        GameObject obj = enemyFlags.Last();
        Destroy(enemyFlags.Last());
        enemyFlags.Remove(obj);

        enemyPickedUpFlags--;

        CheckExtractedNumber();
        enemyFlags.Add(Instantiate(enemyFlag, flagSpawnpoints[random].transform));
    }

    public void DestroyLastPlayerPickedUpFlag()
    {
        int playerFlagIndex = flagSpawnpoints.IndexOf(playerFlags.Last().transform.parent.gameObject);
        extractedSpawnpoints.Remove(playerFlagIndex);

        GameObject obj = playerFlags.Last();
        Destroy(playerFlags.Last());
        playerFlags.Remove(obj);

        player.pickedUpFlags--;

        CheckExtractedNumber();
        playerFlags.Add(Instantiate(playerFlag, flagSpawnpoints[random].transform));
    }

    public void EnemyHasPickedUpFlag()
    {
        enemyPickedUpFlags++;
    }

    private void CheckExtractedNumber()
    {
        random = UnityEngine.Random.Range(0, flagSpawnpoints.Count);
        if (extractedSpawnpoints.Contains(random))
        {
            CheckExtractedNumber();
        }
        extractedSpawnpoints.Add(random);
    }
}
