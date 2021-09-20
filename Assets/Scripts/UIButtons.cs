using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public GameObject panel;

    public Generator generator;

    public Tuto Tuto;

    public void openMenu()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void closeMenu()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public Toggle a, b, c, x, y;

    public void play()
    {
        if (a.isOn)
        {
            PlayerPrefs.SetInt("dificulty", 1);
        }
        else if (b.isOn)
        {
            PlayerPrefs.SetInt("dificulty", 2);
        }
        else
        {
            PlayerPrefs.SetInt("dificulty", 3);
        }

        if (x.isOn)
        {
            PlayerPrefs.SetInt("dimensionX", 3);
            PlayerPrefs.SetInt("dimensionY", 3);
            PlayerPrefs.SetInt("dimensionZ", 3);
        }
        else
        {
            PlayerPrefs.SetInt("dimensionX", 4);
            PlayerPrefs.SetInt("dimensionY", 4);
            PlayerPrefs.SetInt("dimensionZ", 4);
        }

        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    public void playEasy()
    {
        PlayerPrefs.SetInt("dificulty", 1);
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    public void playMedium()
    {
        PlayerPrefs.SetInt("dificulty", 2);
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    public void playHard()
    {
        PlayerPrefs.SetInt("dificulty", 3);
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    public void playTutorial()
    {
        PlayerPrefs.SetInt("dimensionX", 3);
        PlayerPrefs.SetInt("dimensionY", 3);
        PlayerPrefs.SetInt("dimensionZ", 3);

        PlayerPrefs.SetInt("dificulty", 4);
        SceneManager.LoadScene("Tuto", LoadSceneMode.Single);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
    }

    private void playRecordTryOut()
    {

    }
    private void playRecordTryOut4x4()
    {

    }

    public void changeLights()
    {
        generator.changeLights();
    }

    public void clear()
    {
        generator.deleteAllBridges();
        closeMenu();
    }

    public void toStart()
    {
        SceneManager.LoadScene("NewStart", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void fontFabricHyperlink()
    {
        Application.OpenURL("https://www.fontfabric.com/");
    }

    public void cakeDevHyperlink()
    {
        Application.OpenURL("https://twitter.com/cakeslice_dev");
    }

    public void nikoliHyperLink()
    {
        Application.OpenURL("https://www.nikoli.co.jp/en/puzzles/");
    }

    public void nuriaHyperLink()
    {
        Application.OpenURL("https://mauratosh.wixsite.com/mauratosh");
    }

    public void moveTutoRight()
    {
        Tuto.moveTutoRight();
    }

    public void moveTutoLeft()
    {
        Tuto.moveTutoLeft();
    }
}
