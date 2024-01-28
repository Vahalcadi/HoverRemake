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
        //sets all the flags SpriteRenderer to false
        redFlag1.enabled = false;
        redFlag2.enabled = false;
        redFlag3.enabled = false;
        blueFlag1.enabled = false;
        blueFlag2.enabled = false;
        blueFlag3.enabled = false;
    }

    private void Update()
    {
        //texts are updated based on player ref
        jumpText.text = $"{player.jumpUses}";
        wallText.text = $"{player.wallUses}";
        invisibilityText.text = $"{player.invisibilityUses}";
        scoreText.text = player.score.ToString();

        //activates/deactivates SpriteRenderer based on flags currently owned
        switch (player.pickedUpFlags)
        {
            case 0:
                blueFlag1.enabled = false;
                blueFlag2.enabled = false;
                blueFlag3.enabled = false;
                break;
            case 1:
                blueFlag1.enabled = true;
                blueFlag2.enabled = false;
                blueFlag3.enabled = false;
                break;
            case 2:
                blueFlag1.enabled = true;
                blueFlag2.enabled = true;
                blueFlag3.enabled = false;
                break;
            case 3:
                blueFlag1.enabled = true;
                blueFlag2.enabled = true;
                blueFlag3.enabled = true;
                break;
        }

        switch (flagChaser.flagsLeft)
        {
            case 3:
                redFlag1.enabled = false;
                redFlag2.enabled = false;
                redFlag3.enabled = false;
                break;
            case 2:
                redFlag1.enabled = true;
                redFlag2.enabled = false;
                redFlag3.enabled = false;
                break;
            case 1:
                redFlag1.enabled = true;
                redFlag2.enabled = true;
                redFlag3.enabled = false;
                break;
            case 0:
                redFlag1.enabled = true;
                redFlag2.enabled = true;
                redFlag3.enabled = true;
                break;
        }
    }
}
