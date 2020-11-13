using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class LevelData
{
    public string Id;

    public float AsteroidTimeout;

    public int AsteroidCountOnSpawn;

    public bool Completed;

    public string RequiredLevelId;
}

[CreateAssetMenu(fileName = "LevelDataSettings", menuName = "Level Data settings")]
public class LevelDataSettings : ScriptableObject
{
    public string Id;

    public float AsteroidTimeoutMin = 0.1f;

    public float AsteroidTimeoutMax = 0.1f;

    public int AsteroidCountOnSpawnMin = 1;

    public int AsteroidCountOnSpawnMax = 1;

    public LevelDataSettings RequiredLevel;

#if UNITY_EDITOR

    private void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        Id = AssetDatabase.AssetPathToGUID(path);

        Assert.IsTrue(AsteroidTimeoutMin != 0, $"{nameof(AsteroidTimeoutMin)} can't be zero");
        Assert.IsTrue(AsteroidTimeoutMax != 0, $"{nameof(AsteroidTimeoutMax)} can't be zero");
        Assert.IsTrue(AsteroidCountOnSpawnMin != 0, $"{nameof(AsteroidCountOnSpawnMin)} can't be zero");
        Assert.IsTrue(AsteroidCountOnSpawnMax != 0, $"{nameof(AsteroidCountOnSpawnMax)} can't be zero");

        Assert.IsTrue(AsteroidTimeoutMin <= AsteroidTimeoutMax, $"{nameof(AsteroidTimeoutMin)} can't be less then {nameof(AsteroidTimeoutMax)}");
        Assert.IsTrue(AsteroidCountOnSpawnMin <= AsteroidCountOnSpawnMax, $"{nameof(AsteroidCountOnSpawnMin)} can't be less then {nameof(AsteroidCountOnSpawnMax)}");        
    }
   
#endif

    public LevelData DefineLevelData()
    {
        var result = new LevelData
        {
            Id = Id,
            AsteroidTimeout = Random.Range(AsteroidTimeoutMin, AsteroidTimeoutMax),
            AsteroidCountOnSpawn = Random.Range(AsteroidCountOnSpawnMin, AsteroidCountOnSpawnMax + 1),            
        };

        if (RequiredLevel != null)
            result.RequiredLevelId = RequiredLevel.Id;

        return result;
    }
}
