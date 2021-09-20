using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordSetter : MonoBehaviour
{
    public Toggle a, b, c, x, y;
    public Text recordText, victoriesText;

    private System.TimeSpan ts;
    private int time, victories;

    // Start is called before the first frame update
    void Start()
    {
        // Old records (no 3x3 in the name) must preserve that name to save te old players ciphers

        if (PlayerPrefs.GetInt("easyRecord") < 1) PlayerPrefs.SetInt("easyRecord", 215999);
        if (PlayerPrefs.GetInt("mediumRecord") < 1) PlayerPrefs.SetInt("mediumRecord", 215999);
        if (PlayerPrefs.GetInt("hardRecord") < 1) PlayerPrefs.SetInt("hardRecord", 215999);
        if (PlayerPrefs.GetInt("easy4x4Record") < 1) PlayerPrefs.SetInt("easy4x4Record", 215999);
        if (PlayerPrefs.GetInt("medium4x4Record") < 1) PlayerPrefs.SetInt("medium4x4Record", 215999);
        if (PlayerPrefs.GetInt("hard4x4Record") < 1) PlayerPrefs.SetInt("hard4x4Record", 215999);
    }

    private void Update()
    {
        if (x.isOn)
        {
            if (a.isOn)
            {
                time = PlayerPrefs.GetInt("easyRecord");
                victories = PlayerPrefs.GetInt("easy");
            }
            else if (b.isOn)
            {
                time = PlayerPrefs.GetInt("mediumRecord");
                victories = PlayerPrefs.GetInt("medium");
            }
            else
            {
                time = PlayerPrefs.GetInt("hardRecord");
                victories = PlayerPrefs.GetInt("hard");
            }
        }
        else
        {
            if (a.isOn)
            {
                time = PlayerPrefs.GetInt("easy4x4Record");
                victories = PlayerPrefs.GetInt("easy4x4");
            }
            else if (b.isOn)
            {
                time = PlayerPrefs.GetInt("medium4x4Record");
                victories = PlayerPrefs.GetInt("medium4x4");
            }
            else
            {
                time = PlayerPrefs.GetInt("hard4x4Record");
                victories = PlayerPrefs.GetInt("hard4x4");
            }
        }

        ts = System.TimeSpan.FromSeconds(time);
        recordText.text = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");

        victoriesText.text = victories.ToString();
    }
}
