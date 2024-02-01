using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Score region")]
    [SerializeField] Player player;
    [SerializeField] NavigationFlagChaser flagChaser;
    [SerializeField] TextMeshProUGUI scoreText;

    [Header("Flags region")]
    [SerializeField] private GameObject playerFlag;
    [SerializeField] private GameObject enemyFlag;
    [NonSerialized] public List<GameObject> playerFlags = new();
    [NonSerialized] public List<GameObject> enemyFlags = new();
    [SerializeField] private int numberOfPlayerFlags;
    private int enemyPickedUpFlags;
    public List<GameObject> flagSpawnPoints;
    [NonSerialized] public List<int> extractedFlagSpawnpoints = new();


    [Header("Pickup region")]
    [SerializeField] private List<GameObject> pickupSpawnpoints;
    [SerializeField] private List<GameObject> pickupPrefabs;
    [SerializeField] private List<GameObject> pickups = new();
    [SerializeField] private int numberOfPickupsPerType;
    [NonSerialized] public List<int> extractedPickupSpawnpoints = new();

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
        InstantiatePickups();
    }

    private void Update()
    {
        player.score = player.pickedUpFlags * 900;

        if (player.pickedUpFlags == 3)
        {
            player.score += flagChaser.flagsLeft * 2250;
            Time.timeScale = 0;
            RestartGame();
            Debug.Log($"You won. Score: {player.score}");
        }
        if (flagChaser.flagsLeft == 0)
        {
            Time.timeScale = 0;
            RestartGame();
            Debug.Log($"You lost. Score: {player.score}");
            //EditorApplication.isPaused = false;           -temporarily disabled for build
        }
    }

    #region Flags region
    private void InstantiatePlayerFlags()
    {
        for (int i = 0; i < numberOfPlayerFlags; i++)
        {
            CheckExtractedNumber(ref extractedFlagSpawnpoints, ref flagSpawnPoints);
            playerFlags.Add(Instantiate(playerFlag, flagSpawnPoints[random].transform));
        }
    }

    private void InstantiateEnemyFlags()
    {
        for (int i = 0; i < flagChaser.flagsLeft; i++)
        {
            CheckExtractedNumber(ref extractedFlagSpawnpoints, ref flagSpawnPoints);
            enemyFlags.Add(Instantiate(enemyFlag, flagSpawnPoints[random].transform));
        }
    }

    public void DestroyLastEnemyPickedUpFlag()
    {
        int enemyFlagIndex = flagSpawnPoints.IndexOf(enemyFlags.Last().transform.parent.gameObject);
        extractedFlagSpawnpoints.Remove(enemyFlagIndex);

        GameObject obj = enemyFlags.Last();
        Destroy(enemyFlags.Last());
        enemyFlags.Remove(obj);

        enemyPickedUpFlags--;

        CheckExtractedNumber(ref extractedFlagSpawnpoints, ref flagSpawnPoints);
        enemyFlags.Add(Instantiate(enemyFlag, flagSpawnPoints[random].transform));
    }

    public void DestroyLastPlayerPickedUpFlag()
    {
        int playerFlagIndex = flagSpawnPoints.IndexOf(playerFlags.Last().transform.parent.gameObject);
        extractedFlagSpawnpoints.Remove(playerFlagIndex);

        GameObject obj = playerFlags.Last();
        Destroy(playerFlags.Last());
        playerFlags.Remove(obj);

        player.pickedUpFlags--;

        CheckExtractedNumber(ref extractedFlagSpawnpoints, ref flagSpawnPoints);
        playerFlags.Add(Instantiate(playerFlag, flagSpawnPoints[random].transform));
    }

    public void EnemyHasPickedUpFlag()
    {
        enemyPickedUpFlags++;
    }

    #endregion

    #region pickups region
    private void InstantiatePickups()
    {
        for (int i = 0; i < pickupPrefabs.Count; i++)
        {
            for (int j = 0; j < numberOfPickupsPerType; j++)
            {
                CheckExtractedNumber(ref extractedPickupSpawnpoints, ref pickupSpawnpoints);
                pickups.Add(Instantiate(pickupPrefabs[i], pickupSpawnpoints[random].transform));
            }
        }
    }

    private void CheckExtractedNumber(ref List<int> extractedSpawnpoints, ref List<GameObject> spawnPoints)
    {
        random = UnityEngine.Random.Range(0, spawnPoints.Count);
        if (extractedSpawnpoints.Contains(random))
        {
            CheckExtractedNumber(ref extractedSpawnpoints, ref spawnPoints);
        }
        extractedSpawnpoints.Add(random);
    }
    #endregion

    public virtual void PauseGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }



    public void RestartGame()
    {
        QuestionDialogUI.Instance.ShowQuestion($"You won. Score: {player.score}\nPlay again?",
            () =>
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(GameMenu.Instance.sceneName);
            },
            () =>
            {
                QuestionDialogUI.Instance.ShowQuestion("This will close the game, are you sure?",
                    () =>
                    {
                        Application.Quit();
                    },
                    () =>
                    {
                        RestartGame();
                    });
            });
    }
}