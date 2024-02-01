using System;
using System.Collections;
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
    [NonSerialized] int playerScore;

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
    [SerializeField] public List<GameObject> pickups = new();
    [NonSerialized] public List<GameObject> pickupsToRemove = new();
    [NonSerialized] public int numberOfPickupsToRemove;
    [SerializeField] private int numberOfPickupsPerType;
    [NonSerialized] public List<int> extractedPickupSpawnpoints = new();
    [SerializeField] private float respawnTime;

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

            playerScore = player.score;

            player.pickedUpFlags = 0;

            RestartGame();
            Debug.Log($"You won. Score: {player.score}");
        }
        if (flagChaser.flagsLeft == 0)
        {
            Time.timeScale = 0;

            playerScore = player.score;

            flagChaser.flagsLeft = 3;

            RestartGame();
            Debug.Log($"You lost. Score: {player.score}");
            //EditorApplication.isPaused = false;           -temporarily disabled for build
        }

        if (numberOfPickupsToRemove == 4)
        {
            numberOfPickupsToRemove = 0;
            StartCoroutine(ReplacePickups());
        }
    }

    #region Flags region
    private void InstantiatePlayerFlags()
    {
        for (int i = 0; i < numberOfPlayerFlags; i++)
        {
            CheckExtractedFlagSpawnpoints();
            playerFlags.Add(Instantiate(playerFlag, flagSpawnPoints[random].transform));
        }
    }

    private void InstantiateEnemyFlags()
    {
        for (int i = 0; i < flagChaser.flagsLeft; i++)
        {
            CheckExtractedFlagSpawnpoints();
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

        CheckExtractedFlagSpawnpoints();
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

        CheckExtractedFlagSpawnpoints();
        playerFlags.Add(Instantiate(playerFlag, flagSpawnPoints[random].transform));
    }

    public void EnemyHasPickedUpFlag()
    {
        enemyPickedUpFlags++;
    }

    private void CheckExtractedFlagSpawnpoints()
    {
        random = UnityEngine.Random.Range(0, flagSpawnPoints.Count);
        if (extractedFlagSpawnpoints.Contains(random))
        {
            CheckExtractedFlagSpawnpoints();
        }
        else
            extractedFlagSpawnpoints.Add(random);
    }

    #endregion

    #region pickups region
    private void InstantiatePickups()
    {
        for (int i = 0; i < pickupPrefabs.Count; i++)
        {
            for (int j = 0; j < numberOfPickupsPerType; j++)
            {
                CheckExtractedPickupSpawnpoints();
                pickups.Add(Instantiate(pickupPrefabs[i], pickupSpawnpoints[random].transform));
            }
        }
    }

    private IEnumerator ReplacePickups()
    {
        yield return new WaitForSeconds(respawnTime);
        for(int i = 0; i < pickupsToRemove.Count; i++)
        {
            pickupsToRemove[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            pickupsToRemove[i].gameObject.GetComponent<SphereCollider>().enabled = true;
        }

        pickupsToRemove.Clear();
    }


    private void CheckExtractedPickupSpawnpoints()
    {
        random = UnityEngine.Random.Range(0, pickupSpawnpoints.Count);
        if (extractedPickupSpawnpoints.Contains(random))
        {
            CheckExtractedPickupSpawnpoints();
        }
        else
            extractedPickupSpawnpoints.Add(random);
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

        QuestionDialogUI.Instance.ShowQuestion($"You won. Score: {playerScore}\nPlay again?",
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