using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] Player player;

    [Header("Enemy References")]
    [SerializeField] NavigationFlagChaser flagChaser;

    [Header("Sprites References")]
    [SerializeField] SpriteRenderer redFlag1;
    [SerializeField] SpriteRenderer redFlag2;
    [SerializeField] SpriteRenderer redFlag3;
    [SerializeField] SpriteRenderer blueFlag1;
    [SerializeField] SpriteRenderer blueFlag2;
    [SerializeField] SpriteRenderer blueFlag3;

    [Header("Texts References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] TextMeshProUGUI wallText;
    [SerializeField] TextMeshProUGUI invisibilityText;

    private void Start()
    {
        redFlag1.enabled = false;
        redFlag2.enabled = false;
        redFlag3.enabled = false;
        blueFlag1.enabled = false;
        blueFlag2.enabled = false;
        blueFlag3.enabled = false;
    }

    private void Update()
    {
        jumpText.text = $"{player.jumpUses}";
        wallText.text = $"{player.wallUses}";
        invisibilityText.text = $"{player.invisibilityUses}";
        scoreText.text = player.score.ToString();

        switch (player.pickedUpFlags)
        {
            case 0:
                break;
            case 1:
                blueFlag1.enabled = true;
                break;
            case 2:
                blueFlag2.enabled = true;
                break;
            case 3:
                blueFlag3.enabled = true;
                break;
        }

        switch (flagChaser.flagsLeft)
        {
            case 3:
                break;
            case 2:
                blueFlag1.enabled = true;
                break;
            case 1:
                blueFlag2.enabled = true;
                break;
            case 0:
                blueFlag3.enabled = true;
                break;
        }
    }
}
