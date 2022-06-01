using System.IO;
using UnityEngine;

/// <summary>
/// The store manager.
/// </summary>
public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    public string Username { get; set; }
    public int Score { get; set; }
    public string BestUsername { get; set; }
    public int BestScore { get; set; }

    /// <summary>
    /// Awakes this instance.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Instance.Load();
    }

    /// <summary>
    /// Resets score.
    /// </summary>
    public void Reset()
    {
        File.Delete(Application.persistentDataPath + "/savefile.json");
        Username = string.Empty;
        BestUsername = string.Empty;
        BestScore = 0;
    }

    /// <summary>
    /// Loads game data.
    /// </summary>
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Username = data.Username;
            Score = data.Score;

            BestUsername = data.BestUsername;
            BestScore = data.BestScore;
        }
    }

    /// <summary>
    /// Saves game data.
    /// </summary>
    public void Save()
    {
        SaveData data = new SaveData();
        data.Username = Username;
        data.Score = Score;
        data.BestUsername = BestScore < Score ? Username : BestUsername;
        data.BestScore = BestScore < Score ? Score : BestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    /// <summary>
    /// The SaveData model.
    /// </summary>
    [System.Serializable]
    class SaveData
    {
        public string Username;
        public int Score;

        public string BestUsername;
        public int BestScore;
    }
}
