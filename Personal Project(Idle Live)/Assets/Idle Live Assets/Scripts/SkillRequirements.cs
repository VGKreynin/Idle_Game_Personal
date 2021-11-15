using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillRequirements : MonoBehaviour
{
    [HideInInspector] public StartParameters startParameters;
    [HideInInspector] public GameManager gameManager;
    private TextMeshProUGUI gameObjectText;   

    [HideInInspector] public bool skillLvlChangeTrigger;    


    // Start is called before the first frame update
    void Start()
    {
        startParameters = GameObject.Find("GameManager").GetComponent<StartParameters>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        skillLvlChangeTrigger = true;

        gameObjectText = gameObject.GetComponent<TextMeshProUGUI>();
        gameObjectText.text = "";

        SkillsDeactivation(); //Deactivate all skills except endurance        

    }

    // Update is called once per frame
    void Update()
    {
        SkillsRequirements();
    }

    private void SkillsRequirements()
    {
        if (skillLvlChangeTrigger == true)
        {
            gameObjectText.text = "";            

            if (PlayerData.currentSkillReqNumber < GameManager.skillsArray.Length) //check if all skills are opened
            {
                if (IsSkillRequirementsMet() == true) //logical part of requirements
                {
                    GameManager.skillsArray[PlayerData.currentSkillReqNumber].SetActive(true); //activating new job
                    PlayerData.skillEnabledStatus[PlayerData.currentSkillReqNumber] = true;
                    PlayerData.currentSkillReqNumber += 1;
                }

                if (PlayerData.currentSkillReqNumber < GameManager.skillsArray.Length) //visual part of requirements
                {                    
                    for (int i = 0; i < GameManager.skillsArray.Length; i++)
                    {
                        if (PlayerData.skillLvlValue[i] < startParameters.skillRequiremetsMultiArray[PlayerData.currentSkillReqNumber, i])
                        {
                            gameObjectText.text += startParameters.skillsNamesArray[i] + " " + PlayerData.skillLvlValue[i] + "/" + startParameters.skillRequiremetsMultiArray[PlayerData.currentSkillReqNumber, i] + " ";
                        }
                    }
                }
            }
            skillLvlChangeTrigger = false;
        }
    }

    private void SkillsDeactivation() //Deactivating all skills accept Endurance
    {
       for (int i = 1; i < GameManager.skillsArray.Length; i++)
        {
            GameManager.skillsArray[i].SetActive(false);
            
        }                   
    }

    private bool IsSkillRequirementsMet()
    {
        bool triggerX = false;        
        int x = 1;
        
        for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
        {
            if ((PlayerData.skillLvlValue[i] - startParameters.skillRequiremetsMultiArray[PlayerData.currentSkillReqNumber, i]) < 0)
            {
                x = 0;
            }
        }

        if (x == 1)
        {
            triggerX = true;
        }

        return triggerX;
    }
}

