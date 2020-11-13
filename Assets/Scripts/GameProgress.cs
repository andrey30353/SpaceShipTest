using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameProgressSaveData
{
    public List<LevelData> Levels;
}

public static class GameProgress
{
    public static LevelData SelectedLevel;

    public static List<LevelData> Levels;

    private static bool _inited;          

    public static void Init(IEnumerable<LevelDataSettings> levelSettingsList)
    {
        Debug.Log("Init");

        if (_inited)
            return;

        // load
        var gameProgress = SaveIO.LoadGameProgress();
        if (gameProgress != null)
        {
            // Debug.Log("load gameProgress");
            Levels = gameProgress.Levels;
        }
        else
        {
            Levels = levelSettingsList.Select(t => t.DefineLevelData()).ToList();

            Save();
        }

        _inited = true;
    }

    public static bool IsAvailableLevel(string id)
    {
        var level = Levels.FirstOrDefault(t => t.Id == id);
        if (level == null)
            throw new System.ArgumentException($"Couldn't find level with {nameof(id)} = {id}");

        if (level.Completed)
            return true;

        if (string.IsNullOrEmpty(level.RequiredLevelId))
            return true;

        var requiredLevel = Levels.FirstOrDefault(t => t.Id == level.RequiredLevelId);
        if (requiredLevel != null && requiredLevel.Completed)
            return true;

        return false;
    }

    public static void SelectLevel(string id)
    {
        SelectedLevel = Levels.FirstOrDefault(t => t.Id == id);
    }

    public static void SaveProgress()
    {
        if (SelectedLevel == null)
            throw new System.ArgumentException($"{nameof(SelectedLevel)} can't be null");

        SelectedLevel.Completed = true;
        
        Save();
    }

    private static void Save()
    {
        var gameProgress = new GameProgressSaveData();
        gameProgress.Levels = Levels;
        SaveIO.SaveGameProgress(gameProgress);
    }
}
