using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGrid : MonoBehaviour
{
    private GameObject[] positionGrid;

    public GameObject IslandBlock;
    public GameObject BridgeBlock;



    // Start is called before the first frame update
    void Awake()
    {
        positionGrid = new GameObject[this.gameObject.transform.childCount];
        int i = 0;
        foreach(Transform child in this.gameObject.transform)
        {
            positionGrid[i] = child.gameObject;
            i++;
        }
    }

    // Only for dimension == 3
    private static int translate(int x, int y, int z)
    {
        return translate("" + x + y + z);
    }

    private static int translate(string s)
    {
        if (s.Equals("000")) return 0;
        if (s.Equals("001")) return 1;
        if (s.Equals("002")) return 2;
        if (s.Equals("010")) return 3;
        if (s.Equals("011")) return 4;
        if (s.Equals("012")) return 5;
        if (s.Equals("020")) return 6;
        if (s.Equals("021")) return 7;
        if (s.Equals("022")) return 8;
        if (s.Equals("100")) return 9;
        if (s.Equals("101")) return 10;
        if (s.Equals("102")) return 11;
        if (s.Equals("110")) return 12;
        if (s.Equals("111")) return 13;
        if (s.Equals("112")) return 14;
        if (s.Equals("120")) return 15;
        if (s.Equals("121")) return 16;
        if (s.Equals("122")) return 17;
        if (s.Equals("200")) return 18;
        if (s.Equals("201")) return 19;
        if (s.Equals("202")) return 20;
        if (s.Equals("210")) return 21;
        if (s.Equals("211")) return 22;
        if (s.Equals("212")) return 23;
        if (s.Equals("220")) return 24;
        if (s.Equals("221")) return 25;
        if (s.Equals("222")) return 26;
        else
        {
            Debug.LogError("Cagaste en PositionGrid.translate()");
            return 0;
        }
    }

    private static int translate2(int x, int y, int z)
    {
        return translate2("" + x + y + z);
    }

    // For 4x4 dimension
    private static int translate2(string s)
    {
        if (s.Equals("000")) return 0;
        if (s.Equals("001")) return 1;
        if (s.Equals("002")) return 2;
        if (s.Equals("003")) return 3;

        if (s.Equals("010")) return 4;
        if (s.Equals("011")) return 5;
        if (s.Equals("012")) return 6;
        if (s.Equals("013")) return 7;

        if (s.Equals("020")) return 8;
        if (s.Equals("021")) return 9;
        if (s.Equals("022")) return 10;
        if (s.Equals("023")) return 11;

        if (s.Equals("030")) return 12;
        if (s.Equals("031")) return 13;
        if (s.Equals("032")) return 14;
        if (s.Equals("033")) return 15;

        if (s.Equals("100")) return 16;
        if (s.Equals("101")) return 17;
        if (s.Equals("102")) return 18;
        if (s.Equals("103")) return 19;

        if (s.Equals("110")) return 20;
        if (s.Equals("111")) return 21;
        if (s.Equals("112")) return 22;
        if (s.Equals("113")) return 23;

        if (s.Equals("120")) return 24;
        if (s.Equals("121")) return 25;
        if (s.Equals("122")) return 26;
        if (s.Equals("123")) return 27;

        if (s.Equals("130")) return 28;
        if (s.Equals("131")) return 29;
        if (s.Equals("132")) return 30;
        if (s.Equals("133")) return 31;

        if (s.Equals("200")) return 32;
        if (s.Equals("201")) return 33;
        if (s.Equals("202")) return 34;
        if (s.Equals("203")) return 35;

        if (s.Equals("210")) return 36;
        if (s.Equals("211")) return 37;
        if (s.Equals("212")) return 38;
        if (s.Equals("213")) return 39;

        if (s.Equals("220")) return 40;
        if (s.Equals("221")) return 41;
        if (s.Equals("222")) return 42;
        if (s.Equals("223")) return 43;

        if (s.Equals("230")) return 44;
        if (s.Equals("231")) return 45;
        if (s.Equals("232")) return 46;
        if (s.Equals("233")) return 47;

        if (s.Equals("300")) return 48;
        if (s.Equals("301")) return 49;
        if (s.Equals("302")) return 50;
        if (s.Equals("303")) return 51;

        if (s.Equals("310")) return 52;
        if (s.Equals("311")) return 53;
        if (s.Equals("312")) return 54;
        if (s.Equals("313")) return 55;

        if (s.Equals("320")) return 56;
        if (s.Equals("321")) return 57;
        if (s.Equals("322")) return 58;
        if (s.Equals("323")) return 59;

        if (s.Equals("330")) return 60;
        if (s.Equals("331")) return 61;
        if (s.Equals("332")) return 62;
        if (s.Equals("333")) return 63;
        else
        {
            Debug.LogError("Cagaste en PositionGrid.translate() con " + s);
            return 0;
        }
    }

    private static int fullTranslate(int x, int y, int z)
    {
        int num;
        if (PlayerPrefs.GetInt("dimensionX") == 3)
        {
            num = translate(x, y, z);
        }
        else if (PlayerPrefs.GetInt("dimensionX") == 4)
        {
            num = translate2(x, y, z);
        }
        else
        {
            Debug.Log("Failure on dimension");
            num = 0;
        }
        return num;
    }

    private static int fullTranslate(string s)
    {
        int num;
        if (PlayerPrefs.GetInt("dimensionX") == 3)
        {
            num = translate(s);
        }
        else if (PlayerPrefs.GetInt("dimensionX") == 4)
        {
            num = translate2(s);
        }
        else
        {
            Debug.Log("Failure on dimension");
            num = 0;
        }
        return num;
    }



    public GameObject getPosition(int x, int y, int z)
    {
        int num = fullTranslate(x, y, z);
        return positionGrid[num];
    }

    public GameObject getPosition(string s)
    {
        int num = fullTranslate(s);
        return positionGrid[num];
    }

    public GameObject getPosition(int num)
    {
        return positionGrid[num];
    }
    
    public GameObject getPosition(Vector3 v)
    {
        int num = fullTranslate((int)v.x, (int)v.y, (int)v.z);
        return positionGrid[num];
    }

    public void clearPosition(int x, int y, int z)
    {
        int num = fullTranslate(x, y, z);
        var gameObject = positionGrid[num];
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setIsland(int x, int y, int z)
    {
        int num = fullTranslate(x, y, z);
        var gameObject = positionGrid[num];
        Instantiate(IslandBlock, gameObject.transform);
    }

    public void clearPosition(int num)
    {
        var gameObject = positionGrid[num];
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void setIsland(int num)
    {
        var gameObject = positionGrid[num];
        Instantiate(IslandBlock, gameObject.transform);
    }

    public void setBridgeBlock(int num)
    {
        var gameObject = positionGrid[num];
        Instantiate(BridgeBlock, gameObject.transform);
    }
}
