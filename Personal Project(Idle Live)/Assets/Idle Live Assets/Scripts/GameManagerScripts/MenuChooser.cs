using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuChooser : MonoBehaviour
{
    public GameObject[] menuArray;
    private Canvas[] menuCanvasCompArray; //Array of components Canvas from object of menuArray
    private GraphicRaycaster[] menuGraphicrayCompArray; //Array of components GraphicRaycaster from object of menuArray
    public TextMeshProUGUI ecoPointsText;

    private GameManager gameManagerScr;    

    // Start is called before the first frame update
    void Start()
    {
        
        menuCanvasCompArray = new Canvas[menuArray.Length];
        menuGraphicrayCompArray = new GraphicRaycaster[menuArray.Length];

        gameManagerScr = gameObject.GetComponent<GameManager>();        

        for (int i = 0; i< menuArray.Length; i++) //Getting components of Menu Canvases
        {
            menuCanvasCompArray[i] = menuArray[i].GetComponent<Canvas>();
            menuGraphicrayCompArray[i] = menuArray[i].GetComponent<GraphicRaycaster>();

        }
           
    }

    // Update is called once per frame
    void Update()
    {
        //cheking win condition and going to winning screen
        if (gameManagerScr.ecologyValue > gameManagerScr.ecologyToWin)
        {
            MenuActivation(menuArray.Length - 1);
            menuCanvasCompArray[0].enabled = false;
            menuGraphicrayCompArray[0].enabled = false;            
            enabled = false; // stop updating method
        }
    }
    
    public void MenuActivation(int chosenMenu)
    {
        for (int i = 1; i < menuArray.Length; i++)
        {
            menuCanvasCompArray[i].enabled = false;
            menuGraphicrayCompArray[i].enabled = false;
        }
        menuCanvasCompArray[chosenMenu].enabled = true;
        menuGraphicrayCompArray[chosenMenu].enabled = true;
    }

    public void ReincarnationCanvas()
    {
        for (int i = 0; i < menuArray.Length; i++)
        {
            menuCanvasCompArray[i].enabled = false;
            menuGraphicrayCompArray[i].enabled = false;
        }
        menuCanvasCompArray[7].enabled = true;
        menuGraphicrayCompArray[7].enabled = true;
        Time.timeScale = 0;        
        int x = PlayerPrefs.GetInt("Ecology Points");
        x += Mathf.RoundToInt(gameManagerScr.ecologyValue) / 10;        
        PlayerPrefs.SetInt("Ecology Points", x);
        ecoPointsText.text = x.ToString();
    }
}
