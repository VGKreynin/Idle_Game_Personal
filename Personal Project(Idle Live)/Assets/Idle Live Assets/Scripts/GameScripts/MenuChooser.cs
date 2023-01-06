using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuChooser : MonoBehaviour
{
    public CanvasGroup[] menuArray;
    
    [SerializeField]private TextMeshProUGUI reincarnationPointsText;

    

    // Start is called before the first frame update
    void Start()
    {
        MenuActivation(SavableData.openedMenu);        
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

    
    public void ReincarnationCanvas()
    {
        for (int i = 0; i < menuArray.Length; i++)
        {
            menuArray[i].alpha = 0;
            menuArray[i].interactable = false;
            menuArray[i].blocksRaycasts = false;
        }
        menuArray[5].alpha = 1;
        menuArray[5].interactable = true;
        menuArray[5].blocksRaycasts = true;
        Time.timeScale = 0;  
        
        SavableData.reincarnationPoints += Mathf.Round(SavableData.ecologyPoints / 100);         
        reincarnationPointsText.text = SavableData.reincarnationPoints.ToString();        
    }
}
