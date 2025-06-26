using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class PlayerSaveSystem
{
    private static string SaveFolder => Application.persistentDataPath;

    public static void Save(PlayerSaveData data)
    {
        if (string.IsNullOrEmpty(data.saveId))
            data.saveId = Guid.NewGuid().ToString();

        data.timestampLastLoad = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath(data.saveId), json);
    }

    public static PlayerSaveData CreateNewSave()
    {
        PlayerSaveData newSave = PlayerSaveData.CreateDefault();
        Save(newSave);
        return newSave;
    }

    public static IEnumerable<PlayerSaveData> LoadAll()
    {
        if (!Directory.Exists(SaveFolder))
            Directory.CreateDirectory(SaveFolder);

        foreach (var file in Directory.GetFiles(SaveFolder, "*.json"))
        {
            string json = File.ReadAllText(file);
            yield return JsonUtility.FromJson<PlayerSaveData>(json);
        }
    }

    public static IEnumerable<PlayerSaveData> LoadAllOrdered() =>
        LoadAll().OrderByDescending(s => s.timestampLastLoad);

    private static string GetFilePath(string saveId) => Path.Combine(SaveFolder, $"{saveId}.json");
}
