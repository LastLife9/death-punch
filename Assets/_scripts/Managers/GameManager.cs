using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public CoreBalanceScriptable CoreBalance;

    private void Start()
    {
        Application.targetFrameRate = 60;

        Init();
    }

    public void Init()
    {
        ScoreManager.Instance.Init();

        UIManager.Instance.ShowWindow(WindowType.Menu);

        Player player = FindObjectOfType<Player>();
        player.OnDie += () => UIManager.Instance.ShowWindow(WindowType.Lose);
        player.OnDie += EnemySpawner.Instance.StopSpawning;
    }

    public void StartFight()
    {
        Debug.Log("Start ");
        ScoreManager.Instance.CurrentScore = 0;
        UIManager.Instance.ShowWindow(WindowType.Game);
        EnemySpawner.Instance.Init();
    }

    public void Reload()
    {
        SaveManager.Instance.SaveData();
        DOTween.CompleteAll();
        DOTween.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}