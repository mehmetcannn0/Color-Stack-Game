using TMPro;
using UnityEngine;

public class PlayerDataUI : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private TMP_Text scoreText;

    public void SetData(Player player)
    {
        nameText.text = player.Name;
        coinCountText.text = player.CoinCount.ToString();
        scoreText.text = player.Score.ToString();
    }
}