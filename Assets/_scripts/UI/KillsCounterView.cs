using System.Collections;
using UnityEngine;
using TMPro;

public class KillsCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCounterTxt;

    private void Update()
    {
        killCounterTxt.text = ScoreManager.Instance.CurrentScore.ToString();
    }
}