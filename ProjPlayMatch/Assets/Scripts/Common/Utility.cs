using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace PlayMatch
{
    public class Utility
    {
        public static void SaveGame(GameData gameData)
        {
            string json = JsonUtility.ToJson(gameData);
            string filePath = $"{Application.persistentDataPath}{Constants.gameSaveLoadPath}";

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to save game data: " + e.Message);
            }
        }

        public static GameData LoadGame()
        {
            string filePath = $"{Application.persistentDataPath}{Constants.gameSaveLoadPath}";

            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);
                    return JsonUtility.FromJson<GameData>(json);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Failed to load game data: " + e.Message);
                }
            }

            return null;
        }
    }
}

