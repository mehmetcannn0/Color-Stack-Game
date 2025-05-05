using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    GameManager gameManager;
    public Leaderboard Leaderboard = new Leaderboard();
    public static SaveData Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
        //ActionController.FinishLevel += LoadFromJson;
    }

    private void OnDestroy()
    {
        //ActionController.FinishLevel -= LoadFromJson;

    }
    public void SaveToJson()
    {
        string leaderboardData = JsonUtility.ToJson(Leaderboard, true); 
        string filePath = Application.persistentDataPath + Utils.PLAYER_DATA_FILE_NAME;
        System.IO.File.WriteAllText(filePath, leaderboardData);
        Debug.Log("data kaydedýldý"); 
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + Utils.PLAYER_DATA_FILE_NAME;
        if (System.IO.File.Exists(filePath))
        {
            string jsonData = System.IO.File.ReadAllText(filePath);
            Leaderboard = JsonUtility.FromJson<Leaderboard>(jsonData);
            Debug.Log("data çekildi");
        }
        else
        {
            Debug.Log("Kayýt dosyasý bulunamadý.");
        }
    }
    public void SavePlayerData()
    {
        Player newPlayer = new Player();
        newPlayer.Name = gameManager.PlayerName;
        newPlayer.Score = gameManager.Score;
        newPlayer.CoinCount = gameManager.CoinCount;
        Leaderboard.Players.Add(newPlayer);
        SaveToJson();
        LoadFromJson();
    }

}

[System.Serializable]
public class Leaderboard
{
    public List<Player> Players = new List<Player>();
}

[System.Serializable]
public class Player
{
    public string Name;
    public float CoinCount;
    public float Score;
}

