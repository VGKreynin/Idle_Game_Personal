using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobRequirements : MonoBehaviour
{
    [HideInInspector]public StartParameters startParameters;
    [HideInInspector]public GameManager gameManager;

    private TextMeshProUGUI gameObjectText;
    
    

    [HideInInspector]public bool skillLvlChangeTrigger;   

    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Jobreq Start");
        startParameters = GameObject.Find("GameManager").GetComponent<StartParameters>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        skillLvlChangeTrigger = true;                     

        gameObjectText = gameObject.GetComponent<TextMeshProUGUI>();
        gameObjectText.text = "";             

        //JobsDeactivation();        
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
                    SavableData.jobEnabledStatus[SavableData.jobCurrentReqNumber] = true;
                    SavableData.jobCurrentReqNumber += 1;                    
                }

                if (SavableData.jobCurrentReqNumber < StaticFinalData.jobsArray.Length) //visual part of requirements
                {
                    if(SavableData.jobLvlValue[SavableData.jobCurrentReqNumber - 1] < 10) 
                    {
                        gameObjectText.text += startParameters.jobsNamesArray[SavableData.jobCurrentReqNumber - 1] + " " + SavableData.jobLvlValue[SavableData.jobCurrentReqNumber - 1] + "/10 ";
                    }
                    
                    for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
                    {
                        if (gameManager.skillsCurrentLvlArray[i] < startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i])
                        {

                            gameObjectText.text += startParameters.skillsNamesArray[i] + " " + gameManager.skillsCurrentLvlArray[i] + "/" + startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i] + " ";
                        }
                    }
                }
            }

            
            
            skillLvlChangeTrigger = false;
        }
    }
    
    private void JobsDeactivation() //Deactivating all jobs accept janitor
    {
        for (int i = 1; i < StaticFinalData.jobsArray.Length; i++)
        {
            StaticFinalData.jobsArray[i].SetActive(false);            
        }
    }

    private bool IsJobRequirementsMet()
    {
        bool triggerX = false;        
        int job = SavableData.jobLvlValue[SavableData.jobCurrentReqNumber - 1];
        int x = 1;
        if (job < 10)
        {
            x = 0;
        }
        for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
        {
            if ((gameManager.skillsCurrentLvlArray[i] - startParameters.jobRequiremetsMultiArray[SavableData.jobCurrentReqNumber, i]) < 0)
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
