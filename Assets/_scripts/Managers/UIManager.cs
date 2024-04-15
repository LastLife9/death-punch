using System.Collections;
using UnityEngine;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private UIWindow[] activeWindows;

    public UIWindow GetActiveWindow(WindowType type) => 
        activeWindows.FirstOrDefault(w => w.WindowType == type);

    public void ShowWindow(WindowType type)
    {
        UIWindow window = GetActiveWindow(type);

        if (window.isActiveAndEnabled) return;

        foreach (var item in activeWindows)
            item.Hide(false);
        window.Show(true);
        window.Init();
    }
}