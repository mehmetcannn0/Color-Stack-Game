using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private List<PlayerDataUI> playerUIList = new List<PlayerDataUI>();

    SaveData saveData;

    void Start()
    {
        saveData = SaveData.Instance;
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

    public void UpdateLeaderboardUI()
    {
        List<Player> players = saveData.Leaderboard.Players;
        players.Sort((a, b) => b.CoinCount.CompareTo(a.CoinCount));

        for (int i = 0; i < playerUIList.Count; i++)
        {
            if (i < players.Count)
            {
                playerUIList[i].SetData(players[i]);
            }
        }
    }
}

public static partial class ActionController
{
    public static Action UpdateLeaderboard;
}