using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform pointer;
    public RectTransform gaugeBase;
    public RectTransform point;
    [Range(1, 100)]
    public float pointerSpeed = 100f;

    [Header("Logs")]
    public Image logBase;
    public Image[] logStates;
    public Image[] completedMasksImg;

    [Header("Animation Settings")]
    public Animation sawAnimation;
    public Animation handleAnimation;

    [Header("Game Settings")]
    public float gameTimeLimit = 60f;

    [Header("UI Elements")]
    public TMP_Text remainingTimeText;
    public TextEffect result;
    public TextEffect timeFail;
    public Button startButton;

    private int currentStage = 0;
    private bool isPointerMoving = false;
    private int completedMasks = 0;
    private float remainingTime;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        UpdateUI();
    }

    void Update()
    {
        if (isPointerMoving)
        {
            MovePointer();
            UpdateTimer();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckPointerPosition();
            }
        }
        
    }

    void StartGame()
    {
        startButton.gameObject.SetActive(false);
        remainingTime = gameTimeLimit;
        completedMasks = 0;
        ResetGame();
        MovePointToRandomPosition();
        isPointerMoving = true;
        UpdateUI();
    }

    void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        UpdateUI();

        if (remainingTime <= 0)
        {
            GameOver(false);  // Time ran out, game over
        }
    }

    void ResetLogs()
    {
        foreach (var log in logStates)
        {
            log.gameObject.SetActive(false);
        }
        logBase.gameObject.SetActive(true);
        currentStage = 0;
    }

    public void ProgressLog()
    {
        Debug.Log("current" + currentStage);
        if (currentStage < logStates.Length)
        {
            if (currentStage > 0)
            {
                logStates[currentStage - 1].gameObject.SetActive(false);
            }

            logStates[currentStage].gameObject.SetActive(true);
            currentStage++;
            if(logBase.gameObject.activeSelf)
                logBase.gameObject.SetActive(false);

            if (currentStage == logStates.Length)
            {
                MaskCompleted();
            }
            else
            {
                MovePointToRandomPosition();  // Move the point to a new random position
            }
        }
    }

    void MaskCompleted()
    {
        completedMasks++;
        UpdateUI();

        if (completedMasks >= 6)
        {
            GameOver(true);  // Game win condition met
        }
        else
        {
            ResetLogs();  // Reset for the next mask
            MovePointToRandomPosition();
        }
    }

    void MovePointer()
    {
        float angle = Mathf.PingPong(Time.time * pointerSpeed, 90f) - 45f;
        pointer.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void CheckPointerPosition()
    {
        isPointerMoving = false;
        if(currentStage < 2)
            sawAnimation.Play("SawAni");
        else
        {
            handleAnimation.Play("HandleAni");
        }
        float currentAngle = pointer.localRotation.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;

        float pointAngle = point.localRotation.eulerAngles.z;
        if (pointAngle > 180f) pointAngle -= 360f;

        if (Mathf.Abs(currentAngle - pointAngle) <= 10f)
        {
            result.SetText("성공!");
            ProgressLog();
        }
        else
        {
            result.SetText("실패!");
            remainingTime -= 3f; // Deduct 3 seconds on failure
            timeFail.PlayEffect();
            UpdateUI();
        }

        result.PlayEffect();
        isPointerMoving = true;  // Reset for next round
    }

    void MovePointToRandomPosition()
    {
        float randomAngle = Random.Range(-35f, 35f);
        point.localRotation = Quaternion.Euler(0, 0, randomAngle);
    }

    void GameOver(bool isWin)
    {
        isPointerMoving = false;

        if (isWin)
        {
            Debug.Log("Game Over: 1 Stage Clear");
        }
        else
        {
            Debug.Log("Game Over: 1 Stage Fail");
        }
        
        startButton.gameObject.SetActive(true);
        point.localRotation = Quaternion.Euler(0, 0, 0);
        pointer.localRotation = Quaternion.Euler(0, 0, 0);
        UpdateUI();
        foreach (var masks in completedMasksImg)
        {
            masks.gameObject.SetActive(false);
        }
        //ResetGame();
        

    }

    void ResetGame()
    {
        completedMasks = 0;
        remainingTime = gameTimeLimit;
        ResetLogs();
        MovePointToRandomPosition();
        point.localRotation = Quaternion.Euler(0, 0, 0);
        pointer.localRotation = Quaternion.Euler(0, 0, 0);
        UpdateUI();
        foreach (var masks in completedMasksImg)
        {
            masks.gameObject.SetActive(false);
        }
    }

    void UpdateUI()
    {
        if (completedMasks > 0 && !completedMasksImg[completedMasks - 1].gameObject.activeSelf)
            completedMasksImg[completedMasks - 1].gameObject.SetActive(true);
        remainingTimeText.text = "TIME : " + Mathf.Max(0, remainingTime).ToString("F2") + "s";
    }
}
