using System;
using UnityEngine;

public class LeaderBoardController : MonoBehaviour
{
    private string json = "{\"Players\":[{\"Username\":\"Player1\",\"level\":5,\"type\":1},{\"Username\":\"Player2\",\"level\":3,\"type\":2},{\"Username\":\"Player3\",\"level\":8,\"type\":1}]}";
    [SerializeField] private LeaderBoard boardprefab;
    [SerializeField] private Transform spwanLocation;

    private void Start()
    {
        PlayerDatas playerData = JsonUtility.FromJson<PlayerDatas>(json);
        foreach (var data in playerData.Players)
        {
            LeaderBoard view =Instantiate(boardprefab, spwanLocation);
            view.SetData(data.Username, data.level.ToString());
        }
    }
}

[Serializable]
public class PlayerDatas
{
    public Players[] Players;
}

[Serializable]
public class Players
{
    public string Username;
    public int level;
}
