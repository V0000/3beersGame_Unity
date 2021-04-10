using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameLogic : MonoBehaviour
{
    public Liquid bottle;
    public Liquid[] glasses;
    [Space]
    public float outflowSpeedPerSec = 50f;
    [Space]
    public TextMeshProUGUI bottleText;
    public TextMeshProUGUI[] glassText;
    [Space]
    public GameObject puddlePrefub;
    [HideInInspector]
    public GameObject puddle;
    public GameObject spilledGuy;
    public GameObject loseGuy;
    public GameObject winGuy;

    [Space]
    public float maxDeviation4Win = 50;
    public float restInBottle4Win = 5;

    private Vector3 biassForBottle;
    private Vector3 rotateForBottle;
    private bool isPressed = false;
    private bool isPilled = false; 
    private int numOfGlass;
    private Vector3 startPositionOfBottle;
    private Vector3 startAnglesOfBottle;
    private bool gameIsActive = true;
    private Transform bottleObject;


    void Start()
    {
        numOfGlass = 0;
        biassForBottle = new Vector3(-1.2f, 1.6f, 0f);
        rotateForBottle = new Vector3(-15, 20, -118.2f);
        bottleObject = bottle.transform.parent.transform;
        startPositionOfBottle = bottleObject.position;
        startAnglesOfBottle = bottleObject.transform.eulerAngles;

    }


    void Update()
    {
        //tap is happening
        if (Input.GetMouseButtonDown(0) && bottle.litres > 0 && gameIsActive && numOfGlass < glasses.Length) 
        {
            isPressed = true;
            
            bottleObject.position = glasses[numOfGlass].transform.parent.parent.position + biassForBottle;
            bottleObject.transform.eulerAngles += rotateForBottle;
            bottle.flowFromBottle.SetActive(true);
        }

        //tap is finishd
        if (Input.GetMouseButtonUp(0) && gameIsActive && isPressed)
        {
            StopOutflowing();
            ++numOfGlass;

        }
        //beer is over
        if (bottle.litres < 0 && gameIsActive)
        {
            StopOutflowing();
            bottle.flowFromBottle.SetActive(false);
            gameIsActive = false;
        }

        //beer filling a glass
        if (isPressed && bottle.litres > 0)
        {
            bottle.litres -= outflowSpeedPerSec * Time.deltaTime;
            glasses[numOfGlass].litres += outflowSpeedPerSec * Time.deltaTime;
            bottleText.text = string.Format("{0:0.}", bottle.litres) + "мл";
        }


        //glass is overflowed
        if (numOfGlass < glasses.Length &&  glasses[numOfGlass].litres > glasses[numOfGlass].maxLitres && gameIsActive)
        {
            GGspilled(glasses[numOfGlass].transform.parent.parent.position + new Vector3(0, -1.1f, 0));
            gameIsActive = false;
            isPilled = true;
            StopOutflowing();
        }
        //all glasses are filled
        else if ((!gameIsActive && !isPilled) || numOfGlass == glasses.Length)
        {
            gameIsActive = false;
            if (MaxDeviation() < maxDeviation4Win && bottle.litres<restInBottle4Win)
            {
                GGwin();
            }
            else
            {
                GGlose();
            }
        }
        //restart game after results 
        if (!gameIsActive && Input.GetMouseButtonDown(0))
        {
            Restart();
        }

    }

    void GGspilled(Vector3 puddlePos)
    {
        ShowResults();
        puddle = Instantiate(puddlePrefub, puddlePos, Quaternion.identity);
        spilledGuy.SetActive(true);
    }

    void GGlose()
    {
        ShowResults();
        loseGuy.SetActive(true);
    }

    void GGwin()
    {
        ShowResults();
        winGuy.SetActive(true);
    }

    void Restart()
    {
        SceneManager.LoadScene("Scene");
    }

    void StopOutflowing()
    {
        isPressed = false;
        bottleObject.position = startPositionOfBottle;
        bottle.flowFromBottle.SetActive(false);
        bottleObject.transform.eulerAngles = startAnglesOfBottle;
        glassText[numOfGlass].text = string.Format("{0:0.}", glasses[numOfGlass].litres) + "мл";

    }

    float MaxDeviation()
    {
        float max = 0;
        float min = glasses[0].maxLitres;
        foreach (Liquid volume in glasses)
        {
            if (volume.litres > max) max = volume.litres;
            if (volume.litres < min) min = volume.litres;
        }

        return max - min;
    }

    void ShowResults()
    {

        bottleText.gameObject.SetActive(true);
        foreach (TextMeshProUGUI glass in glassText)
        {
            glass.gameObject.SetActive(true);
        }

    }
}
