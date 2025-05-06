using System.IO;
using UnityEditor;
using UnityEngine;

public class ShowPlayerDataWindow : EditorWindow
{
    private PlayerList playerList;
    private Vector2 scrollPos;


    [MenuItem("Tools/Player Tools/Oyuncu Verilerini G�ster-Sil")]
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
            Debug.Log("JSON ��eri�i:\n" + json);
            playerList = JsonUtility.FromJson<PlayerList>(json);
        }
        else
        {
            Debug.LogWarning("PlayerData.json bulunamad�.");
            playerList = null;
        }
    }
    public void DeletePlayerDataFile()
    {
        string filePath = Application.persistentDataPath + "/PlayerData.json";

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Kay�t dosyas� ba�ar�yla silindi: " + filePath);
            EditorUtility.DisplayDialog("Ba�ar�l�", "Kay�t dosyas� silindi.", "Tamam");
        }
        else
        {
            Debug.LogWarning("Silinecek dosya bulunamad�: " + filePath);
            EditorUtility.DisplayDialog("Dosya Bulunamad�", "Silinecek kay�t dosyas� bulunamad�.", "Tamam");
        }
    }

    private void OnGUI()
    {
        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("Bu pencere Play Mode'da �al��maz.", MessageType.Warning);
            return;
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Veriyi Yeniden Y�kle"))
        {
            LoadPlayerData();
        }

        if (GUILayout.Button("Oyuncu Verilerini Sil"))
        {
            DeletePlayerDataFile();
            LoadPlayerData();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        if (playerList == null || playerList.Players == null)
        {
            EditorGUILayout.HelpBox("Oyuncu verisi y�klenemedi!", MessageType.Warning);
            if (GUILayout.Button("Yeniden Dene"))
            {
                LoadPlayerData();
            }
            return;
        }

        EditorGUILayout.LabelField("Oyuncu Listesi", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (PlayerData player in playerList.Players)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Ad:", player.Name);
            EditorGUILayout.LabelField("Alt�n:", player.Gold.ToString());
            EditorGUILayout.LabelField("Skor:", player.Score.ToString());
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();
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