using System.Collections;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int bestScore = 0;
    public int totalScore = 0;
}

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveData saveData;
    public SaveData GameData { get => saveData; }

    private string _path;
    private string _generalFileName = "general.json";

    protected override void Awake()
    {
        base.Awake();

        _path = Application.persistentDataPath;

        PrepareData();
    }

    public void PrepareData()
    {
        if (!PlayerPrefs.HasKey("firstRun"))
        {
            PlayerPrefs.SetInt("firstRun", 1);

            DeleteFile(Path.Combine(_path, _generalFileName));
        }

        if (!File.Exists(Path.Combine(_path, _generalFileName)))
            CreateGeneralData();
        else
            ReadGeneralData();
    }

    private void CreateGeneralData()
    {
        saveData = new SaveData();
        Debug.Log("Data created!");
        SaveData();
    }

    private void ReadGeneralData()
    {
        var curData = File.ReadAllText(Path.Combine(_path, _generalFileName));
        saveData = JsonUtility.FromJson<SaveData>(curData);

        Debug.Log("Data readed!");
    }

    public void UpdateData(SaveData newData)
    {
        saveData = newData;
    }

    public void SaveData()
    {
        var json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Path.Combine(_path, _generalFileName), json);

        Debug.Log("Data saved!");
    }

    private void DeleteFile(string filePath)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);

        Debug.Log("Data deleted!");
    }

    [ContextMenu("Delete data")]
    public void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
        SaveData();
    }
}