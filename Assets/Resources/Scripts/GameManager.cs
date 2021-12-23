using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    #region VARIABLES
    [HideInInspector] public static int gameMoney;
    [HideInInspector] public static int levelCount;
    [HideInInspector] public static bool isStart;
    #endregion
    private void Awake()
    {
        GetAllData();
    }



    #region GAME MONEY METHOD

    // Increase In-Game Money. And Save.
    public void IncreaseGameMoney(int moneyCount)
    {
        gameMoney += moneyCount;
        PlayerPrefs.SetInt("gamemoney", gameMoney);
    }
    #endregion

    #region DATA CONTROLL METHODS

    // Controls All Data in the Game.
    private void GetAllData()
    {
        // Game Money Data Controll
        if (PlayerPrefs.HasKey("gamemoney"))
        {
            gameMoney = PlayerPrefs.GetInt("gamemoney");
        }
        else
        {
            gameMoney = 0;
        }

        // Level Data Controll
        if (PlayerPrefs.HasKey("leveldata"))
        {
            levelCount = PlayerPrefs.GetInt("leveldata");
        }
        else
        {
            levelCount = 1;
        }
    }
    #endregion

    #region LEVEL METHODS

    // Restart Level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Used to Proceed to the Next Level.
    public void NextLevel()
    {
        levelCount++;
        SceneManager.LoadScene("Level" + levelCount);
    }
    #endregion

}
