using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuWindow : UIWindow
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;
    [SerializeField] private TextMeshProUGUI totalScoreTxt;

    public override void Init()
    {
        exitBtn.onClick.RemoveAllListeners();
        startBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.AddListener(Application.Quit);
        startBtn.onClick.AddListener(GameManager.Instance.StartFight);
        totalScoreTxt.text = $"Total score: {ScoreManager.Instance.TotalScore}";
    }
}
