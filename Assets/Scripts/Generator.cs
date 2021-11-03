using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    public GameObject loadingPanel;

    private int dimensionX, dimensionY, dimensionZ;

    private Grid3D grid;

    private List<Bridge> bridgeList;

    public GameObject PositionGrid;

    private List<GameObject> bridgeLines;

    public GameObject line, BridgeLines;

    public GameObject VictoryPanel, NewRecord;

    public Text notInterconnectedWarning;

    private int islandNum;

    public GameObject RegularLights;

    public GameObject ColoredLights;

    private bool lights;

    public void Awake()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("lights") == 0) lights = false;
        else lights = false;
        setLights();
        init();
        StartCoroutine(closeLoadingPanel());

    }

    public void setLights()
    {
        ColoredLights.SetActive(lights);
        RegularLights.SetActive(!lights);

    }

    public void changeLights()
    {
        if (PlayerPrefs.GetInt("lights") == 0) PlayerPrefs.SetInt("lights", 1);
        else PlayerPrefs.SetInt("lights", 0);
        lights = !lights;
        setLights();

    }

    IEnumerator closeLoadingPanel()
    {
        yield return new WaitForSeconds(0.5f);
        loadingPanel.SetActive(false);
    }

    private void init()
    {
        bridgeLines = new List<GameObject>();
        dimensionX = PlayerPrefs.GetInt("dimensionX");
        dimensionY = PlayerPrefs.GetInt("dimensionY");
        dimensionZ = PlayerPrefs.GetInt("dimensionZ");

        // Creamos el grid de la dimension dada (es siempre un cubo), esto tiene que ir en awake para que el touch manager lo encuentre antes??
        grid = new Grid3D(dimensionX, dimensionY, dimensionZ);
    }

    public void Start()
    {
        
        switch (PlayerPrefs.GetInt("dificulty"))
        {
            case 1:
                generateEasy();
                break;
            case 2:
                generateMedium();
                break;
            case 3:
                generateHard();
                break;
            case 4:
                generateTutorial();
                break;
            default:
                Debug.LogError("Wrong difficulty sent (PlayerPrefs)");
                generate();
                break;
        }
    }

    public void addBridge(Bridge b)
    {
        bridgeList.Add(b);
    }

    public void addBridge(Island a, Island b, int axis)
    {
        Bridge bridge = new Bridge(axis, a, b, false);
        a.addBridge(bridge);
        b.addBridge(bridge);
        bridgeList.Add(bridge);
    }

    public List<Bridge> getBridgeList()
    {
        return bridgeList;
    }

    public Grid3D getGrid()
    {
        return grid;
    }

    public Island getIsland(int x, int y, int z)
    {
        return grid.getIntersection(x, y, z).getIsland();
    }

    private void generateWinTest()
    {
        bridgeList = new List<Bridge>();
        Bridge auxBridge;
        Island auxIslandA, auxIslandB;

        grid.getIntersection(0, 0, 0).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 0, 0);

        grid.getIntersection(0, 0, 1).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 0, 1);

        auxIslandA = grid.getIntersection(0, 0, 0).getIsland();
        auxIslandB = grid.getIntersection(0, 0, 1).getIsland();
        auxBridge = new Bridge(2, auxIslandA, auxIslandB, false);

        bridgeList.Add(auxBridge);
        auxIslandA.addBridge(auxBridge);
        auxIslandB.addBridge(auxBridge);

        printAllBridges();
        calculateNeededBridges();
        unprintAllBridges();
        deleteAllBridges();
    }

    private void generateEasy()
    {
        init();
        generate();

        if (PlayerPrefs.GetInt("dimensionX") == 3)
        {
            if (islandNum < 6 || islandNum > 9) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else if (PlayerPrefs.GetInt("dimensionX") == 4)
        {
            if (islandNum < 15 || islandNum > 21) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Cagaste");
        }
    }

    private void generateMedium()
    {
        init();
        generate();

        if (PlayerPrefs.GetInt("dimensionX") == 3)
        {
            if (islandNum < 12 || islandNum > 18) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else if (PlayerPrefs.GetInt("dimensionX") == 4)
        {
            if (islandNum < 28 || islandNum > 38) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Cagaste");
        }
    }

    private void generateHard()
    {
        init();
        generate();

        if (PlayerPrefs.GetInt("dimensionX") == 3)
        {
            if (islandNum < 23) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else if (PlayerPrefs.GetInt("dimensionX") == 4)
        {
            if (islandNum < 48) SceneManager.LoadScene("NewGame", LoadSceneMode.Single);
        }
        else
        {
            Debug.LogError("Cagaste");
        }
    }

    public void generateTutorial()
    {
        bridgeList = new List<Bridge>();
        Bridge auxBridge;
        Island auxIslandA, auxIslandB;

        grid.getIntersection(0, 0, 0).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 0, 0);

        grid.getIntersection(0, 0, 1).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 0, 1);

        grid.getIntersection(0, 0, 2).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 0, 2);

        grid.getIntersection(0, 2, 0).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(0, 2, 0);

        grid.getIntersection(1, 0, 1).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(1, 0, 1);

        auxIslandA = grid.getIntersection(0, 0, 0).getIsland();
        auxIslandB = grid.getIntersection(0, 0, 1).getIsland();
        auxBridge = new Bridge(2, auxIslandA, auxIslandB, false);

        bridgeList.Add(auxBridge);
        auxIslandA.addBridge(auxBridge);
        auxIslandB.addBridge(auxBridge);

        auxIslandA = grid.getIntersection(0, 0, 1).getIsland();
        auxIslandB = grid.getIntersection(0, 0, 2).getIsland();
        auxBridge = new Bridge(2, auxIslandA, auxIslandB, false);

        bridgeList.Add(auxBridge);
        auxIslandA.addBridge(auxBridge);
        auxIslandB.addBridge(auxBridge);

        auxIslandA = grid.getIntersection(0, 0, 1).getIsland();
        auxIslandB = grid.getIntersection(1, 0, 1).getIsland();
        auxBridge = new Bridge(1, auxIslandA, auxIslandB, true);

        bridgeList.Add(auxBridge);
        auxIslandA.addBridge(auxBridge);
        auxIslandB.addBridge(auxBridge);

        auxIslandA = grid.getIntersection(0, 2, 0).getIsland();
        auxIslandB = grid.getIntersection(0, 0, 0).getIsland();
        auxBridge = new Bridge(2, auxIslandA, auxIslandB, false);

        bridgeList.Add(auxBridge);
        auxIslandA.addBridge(auxBridge);
        auxIslandB.addBridge(auxBridge);

        printAllBridges();
        calculateNeededBridges();
        unprintAllBridges();
        deleteAllBridges();
    }

    public void generate()
    {
        // Island num init
        islandNum = 1;

        // Inicializamos la lista de bridges
        bridgeList = new List<Bridge>();

        // Elegimos una interseccion al azar y creamos a partir de ahi un camino conexo aleatorio
        int rX = Random.Range(0, dimensionX), rY = Random.Range(0, dimensionY), rZ = Random.Range(0, dimensionZ);


        grid.getIntersection(rX, rY, rZ).placeIsland();
        PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ);

        generateNewFromIntersection(rX, rY, rZ);


        // Elegimos una interseccion que contenga una isla creada en el camino anterior para generar un nuevo camino a partir de esa isla (deberiamos repetir este paso? cuantas veces? De momento se ejecuta una sola vez)
        while (true)
        {
            rX = Random.Range(0, dimensionX); rY = Random.Range(0, dimensionY); rZ = Random.Range(0, dimensionZ);
            if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
        }


        generateNewFromIntersection(rX, rY, rZ);


        if (PlayerPrefs.GetInt("dificulty") == 3)
        {
            while (true)
            {
                rX = Random.Range(0, dimensionX); rY = Random.Range(0, dimensionY); rZ = Random.Range(0, dimensionZ);
                if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
            }


            generateNewFromIntersection(rX, rY, rZ);


            if (PlayerPrefs.GetInt("dimensionX") == 4)
            {
                while (true)
                {
                    rX = Random.Range(0, dimensionX); rY = Random.Range(0, dimensionY); rZ = Random.Range(0, dimensionZ);
                    if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
                }


                generateNewFromIntersection(rX, rY, rZ);

            }
        }

        // Generar puentes dobles
        // Este paso lo he introducido en la propia generacion, hay un 50% de que los puentes creados sean dobles

        // Print bridges

        printAllBridges();


        // Echar cuentas

        calculateNeededBridges();


        // Borar todos los puentes

        deleteAllBridges();

    }


    private void generateNewFromIntersection(int rX, int rY, int rZ)
    {
        grid.getIntersection(rX, rY, rZ);

        int direction; // shufflear el array e ir sacando de ahi los numeros? Creo que no

        int aux, length = 0, maxPossibleLength = 0, bridges, failed = 0, maxFailed;

        switch (PlayerPrefs.GetInt("dificulty"))
        {
            case 1:
                maxFailed = 2;
                break;
            case 2:
                maxFailed = 7;
                break;
            case 3:
                maxFailed = 15;
                break;
            default:
                Debug.LogError("Wrong difficulty sent (PlayerPrefs)");
                maxFailed = 10;
                break;
        }

        bool doble;

        Island a, b;
        Bridge auxBridge;

        while (failed < maxFailed)
        {


            bridges = Random.Range(1, 3);
            doble = bridges > 1 ? true : false;
            direction = Random.Range(0, 6);

            // +x
            if (direction == 0)
            {
                aux = rX;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimensionX)
                {
                    if (grid.getIntersection(aux + 1, rY, rZ).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX + length, rY, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX + length, rY, rZ);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX + length, rY, rZ).getIsland();

                    auxBridge = new Bridge(1, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX + aux, rY, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rX = rX + length;
                }
                else
                {
                    failed++;
                }
            }

            // -x
            if (direction == 1)
            {
                aux = rX;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 >= 0)
                {
                    if (grid.getIntersection(aux - 1, rY, rZ).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX - length, rY, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX - length, rY, rZ);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX - length, rY, rZ).getIsland();

                    auxBridge = new Bridge(1, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX - aux, rY, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rX = rX - length;
                }
                else
                {
                    failed++;
                }
            }

            // +y
            if (direction == 2)
            {
                aux = rY;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimensionY)
                {
                    if (grid.getIntersection(rX, aux + 1, rZ).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY + length, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY + length, rZ);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY + length, rZ).getIsland();

                    auxBridge = new Bridge(2, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY + aux, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rY = rY + length;
                }
                else
                {
                    failed++;
                }
            }

            // -y
            if (direction == 3)
            {
                aux = rY;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 >= 0)
                {
                    if (grid.getIntersection(rX, aux - 1, rZ).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY - length, rZ).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY - length, rZ);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY - length, rZ).getIsland();

                    auxBridge = new Bridge(2, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY - aux, rZ).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rY = rY - length;
                }
                else
                {
                    failed++;
                }
            }

            // +z
            if (direction == 4)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux + 1 < dimensionZ)
                {
                    if (grid.getIntersection(rX, rY, aux + 1).isEmptY())
                    {
                        aux++;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY, rZ + length).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ + length);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY, rZ + length).getIsland();

                    auxBridge = new Bridge(3, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY, rZ + aux).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rZ = rZ + length;
                }
                else
                {
                    failed++;
                }
            }

            // -z
            if (direction == 5)
            {
                aux = rZ;
                maxPossibleLength = 0;
                //calculate max possible distance
                while (aux - 1 >= 0)
                {
                    if (grid.getIntersection(rX, rY, aux - 1).isEmptY())
                    {
                        aux--;
                        maxPossibleLength++;
                    }
                    else break;
                }

                //random distance
                if (maxPossibleLength >= 1)
                {
                    aux = 1;
                    length = Random.Range(1, maxPossibleLength + 1);

                    grid.getIntersection(rX, rY, rZ - length).placeIsland();
                    PositionGrid.GetComponent<PositionGrid>().setIsland(rX, rY, rZ - length);
                    islandNum++;

                    a = grid.getIntersection(rX, rY, rZ).getIsland();
                    b = grid.getIntersection(rX, rY, rZ - length).getIsland();

                    auxBridge = new Bridge(3, a, b, doble);

                    bridgeList.Add(auxBridge);
                    a.addBridge(auxBridge);
                    b.addBridge(auxBridge);

                    do
                    {
                        grid.getIntersection(rX, rY, rZ - aux).setBridged(true);
                        aux++;
                    }
                    while (aux < length);
                    failed = 0;
                    rZ = rZ - length;
                }
                else
                {
                    failed++;
                }
            }
        }
    }


    public void calculateNeededBridges()
    {
        int count;
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                for (int k = 0; k < dimensionZ; k++)
                {

                    if (grid.getIntersection(i, j, k).hasIsland())
                    {
                        Island island = grid.getIntersection(i, j, k).getIsland();
                        count = island.getCount();
                        island.setNeededBridges(count);
                        foreach (Transform child in GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(i, j, k).transform)
                        {
                            foreach (Transform child2 in child)
                            {
                                child2.gameObject.GetComponent<TextMeshPro>().text = "" + count;
                            }
                        }
                    }
                }
            }
        }
    }

    public void printAllBridges()
    {
        bridgeLines = new List<GameObject>();
        Vector3 A, B;
        int aux = 0;
        foreach (Bridge b in bridgeList)
        {
            bridgeLines.Add(Instantiate(line, BridgeLines.transform));
            A = GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(b.getA().getIntersection().getCoordinates()).transform.position;
            B = GameObject.Find("PositionGrid").GetComponent<PositionGrid>().getPosition(b.getB().getIntersection().getCoordinates()).transform.position;
            if (b.isDoble())
            {
                if (b.getAxis() == 1) //x
                {
                    A.z += 0.2f;
                    B.z += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.z -= 0.4f;
                    B.z -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else if (b.getAxis() == 2) //y
                {
                    A.x += 0.2f;
                    B.x += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.x -= 0.4f;
                    B.x -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else if (b.getAxis() == 3) //z
                {
                    A.x += 0.2f;
                    B.x += 0.2f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                    aux++;
                    bridgeLines.Add(Instantiate(line, BridgeLines.transform));
                    A.x -= 0.4f;
                    B.x -= 0.4f;
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                    bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
                }
                else
                {
                    Debug.LogError("No axis bridge");
                }
            }
            else
            {
                bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(0, A);
                bridgeLines[aux].GetComponent<LineRenderer>().SetPosition(1, B);
            }
            aux++;
        }
    }

    public void unprintAllBridges()
    {
        foreach (GameObject g in bridgeLines)
        {
            Destroy(g);
        }
    }

    public void deleteAllBridges()
    {
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                for (int k = 0; k < dimensionZ; k++)
                {
                    Intersection intersection = grid.getIntersection(i, j, k);
                    intersection.setBridged(false);
                    if (intersection.hasIsland()) intersection.getIsland().clearBridges();
                }
            }
        }
        bridgeList = new List<Bridge>();
        foreach (GameObject g in bridgeLines)
        {
            Destroy(g);
        }
        check();
    }

    public void setDobleBridge(int index)
    {
        bridgeList[index].setDoble(true);
    }

    public void deleteBridge(int index)
    {
        bridgeList.RemoveAt(index);
    }

    public void updateBridges()
    {
        unprintAllBridges();
        printAllBridges();
    }

    public Material Incomplete, Complete, TooMuch;

    public void check()
    {
        Intersection intersection;
        Island island;
        int neededBridges, actualBridges;
        bool victory = true;
        GameObject IslandClone;
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                for (int k = 0; k < dimensionZ; k++)
                {
                    intersection = grid.getIntersection(i, j, k);
                    if (intersection.hasIsland())
                    {
                        island = intersection.getIsland();
                        actualBridges = island.getCount();
                        neededBridges = island.getNeededBridges();
                        if (actualBridges > neededBridges)
                        {
                            // Too much
                            victory = false;
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = TooMuch;
                        }
                        else if (actualBridges == neededBridges)
                        {
                            // Complete
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Complete;
                        }
                        else
                        {
                            // Uncomplete
                            victory = false;
                            IslandClone = GameObject.Find("PositionGrid/" + intersection.getCoordinates() + "/Island(Clone)");
                            IslandClone.GetComponent<MeshRenderer>().material = Incomplete;
                        }
                    }
                }
            }
        }
        if (victory)
        {
            // Hay que checkear si estan interconectados todos los puentes
            if (interconnectedBridges())
            {
                VictoryPanel.SetActive(true);
                Time.timeScale = 0;
                int time = GameObject.Find("Canvas/TimeCounter").GetComponent<TimeCount>().getTimeInSeconds();
                bool record = false;

                // For the record, I need to set it first at 50 hours (for example)

                if (PlayerPrefs.GetInt("dimensionX") == 3)
                {
                    switch (PlayerPrefs.GetInt("dificulty"))
                    {
                        case 1:
                            if (PlayerPrefs.GetInt("easyRecord") > time)
                            {
                                PlayerPrefs.SetInt("easyRecord", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("easy", PlayerPrefs.GetInt("easy") + 1);
                            break;
                        case 2:
                            if (PlayerPrefs.GetInt("mediumRecord") > time)
                            {
                                PlayerPrefs.SetInt("mediumRecord", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("medium", PlayerPrefs.GetInt("medium") + 1);
                            break;
                        case 3:
                            if (PlayerPrefs.GetInt("hardRecord") > time)
                            {
                                PlayerPrefs.SetInt("hardRecord", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("hard", PlayerPrefs.GetInt("hard") + 1);
                            break;
                        case 4:
                            break;
                        default:
                            Debug.LogError("Wrong dificulty in Generator/victory");
                            break;
                    }
                    if (record)
                    {
                        NewRecord.SetActive(true);
                    }
                }
                else if (PlayerPrefs.GetInt("dimensionX") == 4)
                {
                    switch (PlayerPrefs.GetInt("dificulty"))
                    {
                        case 1:
                            if (PlayerPrefs.GetInt("easy4x4Record") > time)
                            {
                                PlayerPrefs.SetInt("easy4x4Record", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("easy4x4", PlayerPrefs.GetInt("easy4x4") + 1);
                            break;
                        case 2:
                            if (PlayerPrefs.GetInt("medium4x4Record") > time)
                            {
                                PlayerPrefs.SetInt("medium4x4Record", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("medium4x4", PlayerPrefs.GetInt("medium4x4") + 1);
                            break;
                        case 3:
                            if (PlayerPrefs.GetInt("hard4x4Record") > time)
                            {
                                PlayerPrefs.SetInt("hard4x4Record", time);
                                record = true;
                            }
                            PlayerPrefs.SetInt("hard4x4", PlayerPrefs.GetInt("hard4x4") + 1);
                            break;
                        case 4:
                            break;
                        default:
                            Debug.LogError("Wrong dificulty in Generator/victory");
                            break;
                    }
                    if (record)
                    {
                        NewRecord.SetActive(true);
                    }
                }
                else
                {
                    Debug.LogError("Messed up dimension or what? Dimension: " + PlayerPrefs.GetInt("dimensionX"));
                }

                GameObject.Find("TouchManager").SetActive(false);
            }
            else
            {
                StartCoroutine(showNotInterconnectedWarning());
            }
        }
    }

    IEnumerator showNotInterconnectedWarning()
    {
        notInterconnectedWarning.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        notInterconnectedWarning.gameObject.SetActive(false);
    }

    private void unvisitIslands()
    {
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                for (int k = 0; k < dimensionZ; k++)
                {
                    if (grid.getIntersection(i, j, k).hasIsland()) grid.getIntersection(i, j, k).getIsland().setVisited(false);
                }
            }
        }
    }

    private bool interconnectedBridges()
    {
        unvisitIslands();
        int rX, rY, rZ;
        // Elegimos una interseccion que contenga una isla creada en el camino anterior para generar un nuevo camino a partir de esa isla (deberiamos repetir este paso? cuantas veces? De momento se ejecuta una sola vez)
        while (true)
        {
            rX = Random.Range(0, dimensionX); rY = Random.Range(0, dimensionY); rZ = Random.Range(0, dimensionZ);
            if (grid.getIntersection(rX, rY, rZ).hasIsland()) break;
        }
        dfsVisited(grid.getIntersection(rX, rY, rZ).getIsland());

        return checkIfFullyConnected();
    }

    private void dfsVisited(Island island)
    {
        island.setVisited(true);
        foreach (Bridge b in island.getBridgeList())
        {
            if (b.getA().getIntersection().getCoordinates().Equals(island.getIntersection().getCoordinates()))
            {
                if (!b.getB().isVisited())
                {
                    dfsVisited(b.getB());
                }

            }
            else
            {
                if (!b.getA().isVisited())
                {
                    dfsVisited(b.getA());
                }

            }
        }
    }

    private bool checkIfFullyConnected()
    {
        bool result = true;
        for (int i = 0; i < dimensionX; i++)
        {
            for (int j = 0; j < dimensionY; j++)
            {
                for (int k = 0; k < dimensionZ; k++)
                {
                    if (grid.getIntersection(i, j, k).hasIsland())
                    {
                        if (!grid.getIntersection(i, j, k).getIsland().isVisited())
                        {
                            result = false;
                        }

                    }

                }
            }
        }
        return result;
    }

}
