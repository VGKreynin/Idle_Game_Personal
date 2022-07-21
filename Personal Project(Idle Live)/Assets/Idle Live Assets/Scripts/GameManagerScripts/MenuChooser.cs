using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuChooser : MonoBehaviour
{
    public CanvasGroup[] menuArray;
    
    //public TextMeshProUGUI ecoPointsText;

    private GameManager gameManagerScr;    

    // Start is called before the first frame update
    void Start()
    {
        
        gameManagerScr = gameObject.GetComponent<GameManager>();            

        MenuActivation(PlayerData.openedMenu);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //cheking win condition and going to winning screen
        if (gameManagerScr.ecologyValue > gameManagerScr.ecologyToWin)
        {
            MenuActivation(menuArray.Length - 1);
            menuCanvasCompArray[0].enabled = false;
            menuGraphicrayCompArray[0].enabled = false;            
            enabled = false; // stop updating method
        }*/
    }
    
    public void MenuActivation(int chosenMenu)
    {
        for (int i = 1; i < menuArray.Length; i++)
        {
            menuArray[i].alpha = 0;
            menuArray[i].interactable = false;
            menuArray[i].blocksRaycasts = false;            
        }
        menuArray[chosenMenu].alpha = 1;
        menuArray[chosenMenu].interactable = true;
        menuArray[chosenMenu].blocksRaycasts = true;
    }

    /*
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
    }*/
}
