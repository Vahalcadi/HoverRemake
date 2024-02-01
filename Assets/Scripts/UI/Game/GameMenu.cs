using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject restartGameUI;
    [SerializeField] private GameObject pauseGameUI;
    [SerializeField] private GameObject playerControlsUI;
    [SerializeField] private GameObject customiseGameSettingsUI;
    public string sceneName = "TestLevel";
    public static GameMenu Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SwitchWithKeyTo(restartGameUI);
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.F3))
            SwitchWithKeyTo(pauseGameUI);

        if (Input.GetKeyDown(KeyCode.F8))
            SwitchWithKeyTo(playerControlsUI);

        if (Input.GetKeyDown(KeyCode.F9))
            SwitchWithKeyTo(customiseGameSettingsUI);
    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);

            CheckForInGameUI();

            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        SwitchTo(inGameUI);
    }

    public void SwitchTo(GameObject _menu)
    {


        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (inGameUI != null)
        {
            inGameUI.SetActive(true);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }

        if (GameManager.Instance != null)
        {
            if (_menu == inGameUI)
                GameManager.Instance.PauseGame(false);
            else
                GameManager.Instance.PauseGame(true);

        }
    }

    public void RestartGame()
    {
        QuestionDialogUI.Instance.ShowQuestion("This action will restart the game, are you sure?",
            () =>
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(sceneName);
            },
            () =>
            {
                SwitchWithKeyTo(inGameUI);
            });
    }
}
