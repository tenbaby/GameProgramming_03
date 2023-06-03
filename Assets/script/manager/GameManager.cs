using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;

    public static GameManager Instance;

    public bool isStartGame = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        theStage = FindAnyObjectByType<StageManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void GameStart()
    {
        for(int i =0; i< goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        theStage.RemoveStage();
        theStage.SettingStage();
        theCombo.ResetCombo();
        theScore.Initialized();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initailized();

        isStartGame = true;
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        goTitleUI.SetActive(true);
    }
}
