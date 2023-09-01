using System;
using UnityEngine;

public class LeaderboardUIController : MonoBehaviour
{
    private string json = "{\"Players\":[{\"Username\":\"Player1\",\"level\":5,\"type\":1},{\"Username\":\"Player2\",\"level\":3,\"type\":2},{\"Username\":\"Player3\",\"level\":8,\"type\":1}]}";
    [SerializeField] private LeaderBoardUIVIew uiViewPrefab;
    [SerializeField] private Transform spawnLocation;
    
    private void Start()
    {
        PlayerData playerDatas = JsonUtility.FromJson<PlayerData>(json);
        foreach (var data in playerDatas.Players)
        {
            LeaderBoardUIVIew view = Instantiate(uiViewPrefab, spawnLocation);
            view.SetData(data.Username,data.level);
        }
        
    }
}

[Serializable]
public class PlayerData
{
    public Player[] Players;
}

[Serializable]
public class Player
{
    public string Username;
    public int level;
    public int type;
}