using TMPro;
using UnityEngine;

public class LeaderBoardUIVIew : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text level;

    public void SetData(string name , int level)
    {
        this.name.text = name;
        this.level.text = level.ToString();
    }
}
