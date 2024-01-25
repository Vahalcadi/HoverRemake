using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] Player player;

    [Header("Texts References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] TextMeshProUGUI wallText;
    [SerializeField] TextMeshProUGUI invisibilityText;

    private void Update()
    {
        jumpText.text = $"{player.jumpUses}";
        wallText.text = $"{player.wallUses}";
        invisibilityText.text = $"{player.invisibilityUses}";
    }
}
