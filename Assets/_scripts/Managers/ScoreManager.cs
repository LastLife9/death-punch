using System.Collections;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    private int currentScore = 0;
    private int bestScore = 0;
    private int totalScore = 0;

    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int BestScore { get => bestScore; set => bestScore = value; }
    public int TotalScore { get => totalScore; set => totalScore = value; }

    public void Init()
    {
        BestScore = SaveManager.Instance.GameData.bestScore;
        TotalScore = SaveManager.Instance.GameData.totalScore;
    }

    public void IncreaseCurrentScore()
    {
        CurrentScore++;
        TotalScore++;

        if (BestScore <= CurrentScore)
            BestScore = CurrentScore;

        SaveData data = SaveManager.Instance.GameData;
        data.bestScore = BestScore;
        data.totalScore = TotalScore;
        SaveManager.Instance.UpdateData(data);
    }
}