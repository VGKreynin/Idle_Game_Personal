using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobRequirements : MonoBehaviour
{
    [HideInInspector]public StartParameters startParameters;
    [HideInInspector]public GameManager gameManager;

    private TextMeshProUGUI gameObjectText;
    public TextMeshProUGUI currentJobText;
    public TextMeshProUGUI currentJobLvlText;
    

    [HideInInspector]public bool skillLvlChangeTrigger;   

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Jobreq Start");
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

            if (PlayerData.currentJobReqNumber < GameManager.jobsArray.Length) //check that not all jobs are opened
            {
                if (IsJobRequirementsMet() == true) //logical part of requirements
                {
                    GameManager.jobsArray[PlayerData.currentJobReqNumber].SetActive(true); //activating new job
                    PlayerData.jobEnabledStatus[PlayerData.currentJobReqNumber] = true;
                    PlayerData.currentJobReqNumber += 1;                    
                }

                if (PlayerData.currentJobReqNumber < GameManager.jobsArray.Length) //visual part of requirements
                {
                    if(PlayerData.jobLvlValue[PlayerData.currentJobReqNumber - 1] < 10) 
                    {
                        gameObjectText.text += startParameters.jobsNamesArray[PlayerData.currentJobReqNumber - 1] + " " + PlayerData.jobLvlValue[PlayerData.currentJobReqNumber - 1] + "/10 ";
                    }
                    
                    for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
                    {
                        if (PlayerData.skillLvlValue[i] < startParameters.jobRequiremetsMultiArray[PlayerData.currentJobReqNumber, i])
                        {

                            gameObjectText.text += startParameters.skillsNamesArray[i] + " " + PlayerData.skillLvlValue[i] + "/" + startParameters.jobRequiremetsMultiArray[PlayerData.currentJobReqNumber, i] + " ";
                        }
                    }
                }
            }

            currentJobText.text = startParameters.jobsNamesArray[PlayerData.currentJobSelectedNumber];
            currentJobLvlText.text = PlayerData.jobLvlValue[PlayerData.currentJobSelectedNumber].ToString();
            skillLvlChangeTrigger = false;
        }
    }    

    private bool IsJobRequirementsMet()
    {
        bool triggerX = false;        
        int job = PlayerData.jobLvlValue[PlayerData.currentJobReqNumber - 1];
        int x = 1;
        if (job < 10)
        {
            x = 0;
        }
        for (int i = 0; i < startParameters.skillsNamesArray.Length; i++)
        {
            if ((PlayerData.skillLvlValue[i] - startParameters.jobRequiremetsMultiArray[PlayerData.currentJobReqNumber, i]) < 0)
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
