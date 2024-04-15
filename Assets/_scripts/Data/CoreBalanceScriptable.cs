using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "CoreBalance", menuName = "General/CoreBalance", order = 51)]
public class CoreBalanceScriptable : ScriptableObject
{
    [field: SerializeField] public EnemyData[] EnemyDatas { get; private set; }
}