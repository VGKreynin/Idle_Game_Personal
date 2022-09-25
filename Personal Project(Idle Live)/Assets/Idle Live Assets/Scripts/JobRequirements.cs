using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobRequirements : MonoBehaviour
{
    private StartParameters startParameters;
    private TextMeshProUGUI gameObjectText; 
    [HideInInspector]public bool skillLvlChangeTrigger; 
    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Jobreq Start");
        startParameters = GameObject.Find("GameManager").GetComponent<StartParameters>();        

        skillLvlChangeTrigger = true;                     

        gameObjectText = gameObject.GetComponent<TextMeshProUGUI>();
        gameObjectText.text = ""; 
    }

    // Update is called once per frame
    void Update()
    {
        JobsRequirements();
    }

    private void JobsRequirements()
    {
        if (skillLvlChangeTrigger == true)
        {
            gameObjectText.text = "";

            if (SavableData.jobCurrentReqNumber < StaticFinalData.jobsArray.Length) //check that not all jobs are opened
            {
                if (IsJobRequirementsMet() == true) //logical part of requirements
                {
                    StaticFinalData.jobsArray[SavableData.jobCurrentReqNumber].SetActive(true); //activating new job
                    SavableData.jobEnabledStatusArray[SavableData.jobCurrentReqNumber] = true;
                    SavableData.jobCurrentReqNumber += 1;                    
                }

                if (SavableData.jobCurrentReqNumber < StaticFinalData.jobsArray.Length) //visual part of requirements
                {
                    if(SavableData.jobLvlValueArray[SavableData.jobCurrentReqNumber - 1] < 10) 
                    {
                        gameObjectText.text += StaticFinalData.jobsNamesArray[SavableData.jobCurrentReqNumber - 1] + " " + SavableData.jobLvlValueArray[SavableData.jobCurrentReqNumber - 1] + "/10 ";
                    }
                    
                    for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
                    {
                        if (SavableData.skillLvlValueArray[i] < startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i])
                        {

                            gameObjectText.text += StaticFinalData.skillsNamesArray[i] + " " + SavableData.skillLvlValueArray[i] + "/" + startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i] + " ";
                        }
                    }
                }
            }

            
            
            skillLvlChangeTrigger = false;
        }
    }    

    private bool IsJobRequirementsMet()
    {
        bool triggerX = false;        
        int job = SavableData.jobLvlValueArray[SavableData.jobCurrentReqNumber - 1];
        int x = 1;
        if (job < 10)
        {
            x = 0;
        }
        for (int i = 0; i < StaticFinalData.skillsNamesArray.Length; i++)
        {
            if ((SavableData.skillLvlValueArray[i] - startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i]) < 0)
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
