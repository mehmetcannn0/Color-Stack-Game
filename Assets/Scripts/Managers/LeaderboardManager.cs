using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public class PlayerUI
    {
        public TMP_Text NameText;
        public TMP_Text CoinCountText;
        public TMP_Text ScoreText;
    }

    private List<PlayerUI> playerUIList = new List<PlayerUI>();
    [SerializeField] Transform content;

    SaveData saveData;

    void Start()
    {
        saveData = SaveData.Instance;
        InitializePlayerUIs();
        saveData.LoadFromJson();
        UpdateLeaderboardUI();
    }

    private void OnEnable()
    {
        ActionController.UpdateLeaderboard += UpdateLeaderboardUI;
    }

    private void OnDisable()
    {
        ActionController.UpdateLeaderboard -= UpdateLeaderboardUI;
    }

    void InitializePlayerUIs()
    {
        playerUIList.Clear();

        foreach (Transform child in content)
        {
            PlayerUI ui = new PlayerUI();
            ui.NameText = child.Find("Player Name").GetComponent<TextMeshProUGUI>();
            ui.ScoreText = child.Find("Player Score").GetComponent<TextMeshProUGUI>();
            ui.CoinCountText = child.Find("Player Coin").GetComponent<TextMeshProUGUI>();

            playerUIList.Add(ui);
        }
    }

    public void UpdateLeaderboardUI()
    {
        var players = saveData.Leaderboard.Players;
        players.Sort((a, b) => b.Score.CompareTo(a.Score));

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i < players.Count)
            {
                playerUIList[i].NameText.text = players[i].Name;
                playerUIList[i].CoinCountText.text = players[i].CoinCount.ToString();
                playerUIList[i].ScoreText.text = players[i].Score.ToString();
            }
            else
            {
                playerUIList[i].NameText.text = "-";
                playerUIList[i].CoinCountText.text = "";
                playerUIList[i].ScoreText.text = "";
            }
        }
    }
}

public static partial class ActionController
{

    public static Action UpdateLeaderboard;
    
}