using System.IO;
using UnityEditor;
using UnityEngine;

public class ShowPlayerDataWindow : EditorWindow
{
    private PlayerList playerList;

    [MenuItem("Tools/Player Tools/Oyuncu Verilerini Göster-Sil")]
    public static void ShowWindow()
    {
        GetWindow<ShowPlayerDataWindow>("Oyuncu Verileri");
    }

    private void OnEnable()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log("Dosya yolu: " + filePath);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.Log("JSON Ýçeriði:\n" + json);
            playerList = JsonUtility.FromJson<PlayerList>(json);
        }
        else
        {
            Debug.LogWarning("PlayerData.json bulunamadý.");
            playerList = null;
        }
    }
    public  void DeletePlayerDataFile()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Kayýt dosyasý baþarýyla silindi: " + filePath);
            EditorUtility.DisplayDialog("Baþarýlý", "Kayýt dosyasý silindi.", "Tamam");
        }
        else
        {
            Debug.LogWarning("Silinecek dosya bulunamadý: " + filePath);
            EditorUtility.DisplayDialog("Dosya Bulunamadý", "Silinecek kayýt dosyasý bulunamadý.", "Tamam");
        }
    }


    private void OnGUI()
    {
        if (playerList == null || playerList.Players == null)
        {
            EditorGUILayout.HelpBox("Oyuncu verisi yüklenemedi!", MessageType.Warning);
            if (GUILayout.Button("Yeniden Dene"))
            {
                LoadPlayerData();
            }
            return;
        }

        EditorGUILayout.LabelField("Oyuncu Listesi", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        foreach (PlayerData player in playerList.Players)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Ad:", player.Name);
            EditorGUILayout.LabelField("Altýn:", player.Gold.ToString());
            EditorGUILayout.LabelField("Skor:", player.Score.ToString());
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("Veriyi Yeniden Yükle"))
        {
            LoadPlayerData();
        }

        if (GUILayout.Button("Oyuncu Verilerini Sil"))
        {
            DeletePlayerDataFile();
            LoadPlayerData();   
        }
    }
}

[System.Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
    public int Gold;
}
[System.Serializable]
public class PlayerList
{
    public PlayerData[] Players;
}