using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillRequirements : MonoBehaviour
{
    private StartParameters startParameters;    
    private TextMeshProUGUI gameObjectText;
    public GameObject[] skillsArray;

    public string[] skillNameArray; //Storing names of all skills
    public int[] skillCurrentLvlArray; //Storing current lvls of all skills

    [HideInInspector] public bool skillLvlChangeTrigger;

    private int currentSkillNumber; //Variable where we store number current job of which requirements should be shown. Cashier 0, president 6.


    // Start is called before the first frame update
    void Start()
    {
        startParameters = GameObject.Find("GameManager").GetComponent<StartParameters>();       

        skillLvlChangeTrigger = true;

        currentSkillNumber = 1;

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

            if (currentSkillNumber < skillsArray.Length) //check if all skills are opened
            {
                if (IsSkillRequirementsMet() == true) //logical part of requirements
                {
                    skillsArray[currentSkillNumber].SetActive(true); //activating new job
                    currentSkillNumber += 1;
                }

                if (currentSkillNumber < skillsArray.Length) //visual part of requirements
                {                    
                    for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
                    {
                        if (SavableData.skillLvlValueArray[i] < startParameters.skillRequiremetsMultiArray[currentSkillNumber, i])
                        {
                            gameObjectText.text += StaticFinalData.skillsNamesArray[i] + " " + SavableData.skillLvlValueArray[i] + "/" + startParameters.skillRequiremetsMultiArray[currentSkillNumber, i] + " ";
                        }
                    }
                }
            }
            skillLvlChangeTrigger = false;
        }
    }

    private void SkillsDeactivation() //Deactivating all skills accept Endurance
    {
       for (int i = 1; i < skillsArray.Length; i++)
        {
            skillsArray[i].SetActive(false);
            
        }                   
    }

    private bool IsSkillRequirementsMet()
    {
        bool triggerX = false;        
        int x = 1;
        
        for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
        {
            if ((SavableData.skillLvlValueArray[i] - startParameters.skillRequiremetsMultiArray[currentSkillNumber, i]) < 0)
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

