using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] Player player;
    //[SerializeField] Rigidbody rb;

    [Header("Enemy References")]
    [SerializeField] NavigationFlagChaser flagChaser;

    [Header("Flag SpriteRenderers References")]
    [SerializeField] SpriteRenderer redFlag1;
    [SerializeField] SpriteRenderer redFlag2;
    [SerializeField] SpriteRenderer redFlag3;
    [SerializeField] SpriteRenderer blueFlag1;
    [SerializeField] SpriteRenderer blueFlag2;
    [SerializeField] SpriteRenderer blueFlag3;

    [Header("Left Frame Images References")]
    [SerializeField] Image jumpBar;
    [SerializeField] Image wallBar;
    [SerializeField] Image invisibilityBar;

    [Header("Right Frame Images References")]
    [SerializeField] Image accelerationBar;
    [SerializeField] Image shieldBar;
    [SerializeField] Image redLightBar;
    [SerializeField] Image greenLightBar;
    /*[SerializeField] Image directionLine;
    float maxLength = 10f;*/

    [Header("Texts References")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI jumpText;
    [SerializeField] TextMeshProUGUI wallText;
    [SerializeField] TextMeshProUGUI invisibilityText;

    Coroutine shieldBarCoroutine;
    Coroutine invisibleBarCoroutine;
    Coroutine wallBarCoroutine;
    Coroutine greenLightBarCoroutine;
    Coroutine redLightBarCoroutine;

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
        scoreText.text = $"{player.score:D7}";

        //ability bars
        jumpBar.fillAmount = (player.transform.position.y - 0.6f) / 5;

        accelerationBar.fillAmount = player.GetComponent<Rigidbody>().velocity.magnitude / 17;

        if (player.isShielded && shieldBarCoroutine == null)
        {
            shieldBarCoroutine = StartCoroutine(DecreaseFillAmount(shieldBar, player.isShielded, 0));

        }

        if (player.isInvisible && invisibleBarCoroutine == null)
        {
            invisibleBarCoroutine = StartCoroutine(DecreaseFillAmount(invisibilityBar, player.isInvisible, 1));
        }

        if (player.wallPlaced && wallBarCoroutine == null)
        {
            wallBarCoroutine = StartCoroutine(DecreaseFillAmount(wallBar, player.wallPlaced, 2));
        }

        if (player.isSpedUp && greenLightBarCoroutine == null)
        {
            if (redLightBarCoroutine != null)
            {
                StopCoroutine(redLightBarCoroutine);
                redLightBarCoroutine = null;
                redLightBar.fillAmount = 0;
            }

            greenLightBarCoroutine = StartCoroutine(DecreaseFillAmount(greenLightBar, player.isSpedUp, 3));
        }

        if (player.isSlowedDown && redLightBarCoroutine == null)
        {
            if (greenLightBarCoroutine != null)
            {
                StopCoroutine(greenLightBarCoroutine);
                greenLightBarCoroutine = null;
                greenLightBar.fillAmount = 0;
            }
            redLightBarCoroutine = StartCoroutine(DecreaseFillAmount(redLightBar, player.isSlowedDown, 4));
        }

        //activates/deactivates SpriteRenderer based on flags currently owned by player/enemy
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

        //draw line based on player direction and speed (not working lol)
        /*float length = Mathf.Min(rb.velocity.magnitude * rb.velocity.magnitude / 17, maxLength);
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        RectTransform rectTransform = directionLine.rectTransform;
        rectTransform.sizeDelta = new Vector2(length, rectTransform.sizeDelta.y);
        rectTransform.rotation = Quaternion.Euler(0f, 0f, rb.rotation.x);*/
    }

    IEnumerator DecreaseFillAmount(Image image, bool isActive, int coroutineIndex)
    {
        float decreaseSpeed = 0.11f;
        image.fillAmount = 1f;
        while (image.fillAmount > 0f && isActive)
        {
            image.fillAmount -= decreaseSpeed * Time.deltaTime;
            yield return null;
        }

        switch (coroutineIndex)
        {
            case 0:
                shieldBarCoroutine = null;
                break;
            case 1:
                invisibleBarCoroutine = null;
                break;
            case 2:
                wallBarCoroutine = null;
                break;
            case 3:
                greenLightBarCoroutine = null;
                break;
            case 4:
                redLightBarCoroutine = null;
                break;
        }
    }
}