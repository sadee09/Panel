
using UnityEngine;
using TMPro;
public class LeaderBoard : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text level;

    public void SetData(string name, string level)
    {
        this.name.text = name;
        this.level.text = level;
    }
}
