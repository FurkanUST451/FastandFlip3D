using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("UI VARIABLES")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text flipPushMoneyText;
    [SerializeField] private GameObject flipPushObj;
    [SerializeField] private Slider sliderObj;
    [SerializeField] private Transform playerPosition, finishLinePosition;

    [SerializeField] private GameObject nextLevelPanel;

    private int flipPushMoneyCount = 20;
    private float _maxDistance;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        _maxDistance = GetDistance();
        moneyText.text = GameManager.gameMoney.ToString();
    }

    private void FixedUpdate()
    {
        LevelRoadSlider();
    }

    private void LevelRoadSlider()
    {
        if (-playerPosition.position.x <= _maxDistance && -playerPosition.position.x<=-finishLinePosition.position.x)
        {
            float distance = 1 - (GetDistance() / _maxDistance);
            SetProgress(distance);
        }
    }

    private float GetDistance()
    {
        return Vector3.Distance(-playerPosition.position, -finishLinePosition.position);
    }

    private void SetProgress(float p)
    {
        sliderObj.value = p;

        if (sliderObj.value>=0.95f)
        {
            nextLevelPanel.SetActive(true);
            Debug.Log("LEVEL COMPLETE");
        }
    }
    public void FlipPush()
    {
        StartCoroutine(FlipPushSystem());
        ShowScreenGameMoney(flipPushMoneyCount);
    }

    IEnumerator FlipPushSystem()
    {
        flipPushMoneyText.text = "+" + flipPushMoneyCount;
        flipPushObj.SetActive(true);
        yield return new WaitForSeconds(1);
        flipPushObj.SetActive(false);
    }
    public void ShowScreenGameMoney(int moneyAmount)
    {
        GameManager.Instance.IncreaseGameMoney(moneyAmount); // TEST GIVE MONEY
        moneyText.text = GameManager.gameMoney.ToString();
    }

    public void StartButton()
    {
        GameManager.isStart = true;
    }
    public void NextLevelButton()
    {
        nextLevelPanel.SetActive(false);
        GameManager.isStart = false;
        GameManager.Instance.NextLevel();
    }

    public void RestartButton()
    {
        GameManager.Instance.RestartLevel();
    }
}
