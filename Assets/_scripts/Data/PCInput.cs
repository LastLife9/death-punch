using System;
using UnityEngine;

public class PCInput : IInput
{
    public Action LeftAttack { get; set; }
    public Action RightAttack { get; set; }

    public void InputProcces()
    {
        if (Input.GetKeyDown(KeyCode.A)) LeftAttack?.Invoke();
        if (Input.GetKeyDown(KeyCode.D)) RightAttack?.Invoke();
    }
}
