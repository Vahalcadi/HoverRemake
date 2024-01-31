using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject restartGameUI;
    [SerializeField] private GameObject pauseGameUI;
    [SerializeField] private GameObject playerControlsUI;
    [SerializeField] private GameObject customiseGameSettingsUI;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
            SwitchWithKeyTo(restartGameUI);

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

    public void ExitGame()
    {
        Application.Quit();
    }
}
