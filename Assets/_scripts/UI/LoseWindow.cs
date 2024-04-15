using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoseWindow : UIWindow
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI bestScoreLabel;
    [SerializeField] private Button restartBtn;

    public override void Init()
    {
        restartBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.AddListener(GameManager.Instance.Reload);
        scoreLabel.text = $"Current: {ScoreManager.Instance.CurrentScore}";
        bestScoreLabel.text = $"Best: {ScoreManager.Instance.BestScore}";
    }
}