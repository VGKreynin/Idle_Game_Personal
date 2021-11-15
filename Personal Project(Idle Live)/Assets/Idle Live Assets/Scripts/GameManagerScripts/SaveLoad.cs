using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public GameManager gameManagerScr;
    private SaveGameData data = new SaveGameData();      

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Savegame.save");

        SaveDataUpdate();

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {        
        if (File.Exists(Application.persistentDataPath + "/Savegame.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Savegame.save", FileMode.Open);
            data = (SaveGameData)bf.Deserialize(file);
            file.Close();                 
            
            LoadDataUpdate();            
        }
    }    

    [Serializable]
    class SaveGameData
    {
        //Time data        
        public float days;
        public float years;

        //Job Data
        public bool isJobActive;
        public float[] jobExpCurrentValue = new float[8];
        public float[] jobExpMaxValue = new float[8];
        public bool[] jobEnabledStatus = new bool[8];
        public bool[] jobLvlLoading = new bool[8];
        public int[] jobLvlValue = new int[8];
        public int currentJobReqNumber;
            //Income
            public float currentBasicJobPayment; //Use to calculate income
            public float[] jobPayMultiplier = new float[8]; //Use to save current income multiplier from lvl of job
            public float currentJobPayMultiplier; //Use to calculate income
            //Other
            public int currentJobSelectedNumber; //USed to show current job on main screen

        //Skill Data
        public bool isSkillActive;
        public bool[] skillEnabledStatus = new bool[5]; //Activ or not particular skill
        public float[] skillExpCurrentValue = new float[5]; //Skill current progress values
        public float[] skillExpMaxValue = new float[5]; //Skill max values
        //public static bool[] skillLvlLoading = new bool[5]; //Used to show lvl of skills first time, when loading the game
        public int[] skillLvlValue = new int[5]; //Skills levels
        public int currentSkillReqNumber; //According to this number the requiremets shows for next skill
            //Effects
            public float[] skillMultipliersArray = new float[5];

        //Reincarnation Data
        public float jobIncMultR; //Stores the multipluer of job income from Reincarnation upgrade
        public int jobIncMultLvlR;//Stores lvl of job income upgrade
        public float jobExpMultR; //Stores the multipluer of job experience boost from Reincarnation upgrade
        public int jobExpMultLvlR;//Stores lvl of job experience boost upgrade
        public float skillExpMultR; //Stores the multipluer of skill experience boost from Reincarnation upgrade
        public int skillExpMultLvlR;//Stores lvl of skill experience boost upgrade
        public float techExpMultR; //Stores the multipluer of technology experience boost from Reincarnation upgrade
        public int techExpMultLvlR;//Stores lvl of technology experience boost upgrade
        public float techCostMultR; //Stores the multipluer of technology cost reduction from Reincarnation upgrade
        public int techCostMultLvlR;//Stores lvl of technology cost reduction upgrade

    }

    private void SaveDataUpdate()
    {
        //Timer Data
        data.days = PlayerData.days;
        data.years = PlayerData.years;

        //Job Data
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            data.jobExpCurrentValue[i] = PlayerData.jobExpCurrentValue[i];
            data.jobExpMaxValue[i] = PlayerData.jobExpMaxValue[i];            
            data.jobLvlValue[i] = PlayerData.jobLvlValue[i];
            data.jobEnabledStatus[i] = PlayerData.jobEnabledStatus[i];           
        }
        data.isJobActive = PlayerData.isJobActive;
        data.currentJobReqNumber = PlayerData.currentJobReqNumber;
            //Income
            for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
            {
                data.jobPayMultiplier[i] = PlayerData.jobPayMultiplier[i];
            }
            data.currentBasicJobPayment = PlayerData.currentBasicJobPayment;
            data.currentJobPayMultiplier = PlayerData.currentJobPayMultiplier;
            //Other
            data.currentJobSelectedNumber = PlayerData.currentJobSelectedNumber;

        //Skill Data
        for (int i = 0; i < PlayerData.skillExpMaxValue.Length; i++)
        {
            data.skillExpCurrentValue[i] = PlayerData.skillExpCurrentValue[i];
            data.skillExpMaxValue[i] = PlayerData.skillExpMaxValue[i];
            data.skillLvlValue[i] = PlayerData.skillLvlValue[i];
            data.skillEnabledStatus[i] = PlayerData.skillEnabledStatus[i];
        }
        data.isSkillActive = PlayerData.isSkillActive;
        data.currentSkillReqNumber = PlayerData.currentSkillReqNumber;
            //Effects
            for (int i = 0; i < PlayerData.skillMultipliersArray.Length; i++)
                {           
                    data.skillMultipliersArray[i] = PlayerData.skillMultipliersArray[i];
                }

        //Reincarnation Data
        data.jobIncMultR = PlayerData.jobIncMultR;
        data.jobIncMultLvlR = PlayerData.jobIncMultLvlR;
        data.jobExpMultR = PlayerData.jobExpMultR;
        data.jobExpMultLvlR = PlayerData.jobExpMultLvlR;
        data.skillExpMultR = PlayerData.skillExpMultR;
        data.skillExpMultLvlR = PlayerData.skillExpMultLvlR;
        data.techExpMultR = PlayerData.techExpMultR;
        data.techExpMultLvlR = PlayerData.techExpMultLvlR;
        data.techCostMultR = PlayerData.techCostMultR;
        data.techCostMultLvlR = PlayerData.techCostMultLvlR;

    }
    
    private void LoadDataUpdate()
    {        
        //Timer Data        
        PlayerData.days = data.days;
        PlayerData.years = data.years;

        //Job Data
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            PlayerData.jobEnabledStatus[i] = data.jobEnabledStatus[i];
            GameManager.jobsArray[i].SetActive(PlayerData.jobEnabledStatus[i]);  
            PlayerData.jobExpCurrentValue[i] = data.jobExpCurrentValue[i];
            PlayerData.jobExpMaxValue[i] = data.jobExpMaxValue[i];
            PlayerData.jobLvlLoading[i] = true;
            PlayerData.jobLvlValue[i] = data.jobLvlValue[i];                                 
        }
        PlayerData.isJobActive = data.isJobActive;
        PlayerData.currentJobReqNumber = data.currentJobReqNumber;
            //Income
            for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
            {
                PlayerData.jobPayMultiplier[i] = data.jobPayMultiplier[i];
            }
            PlayerData.currentBasicJobPayment = data.currentBasicJobPayment;
            PlayerData.currentJobPayMultiplier = data.currentJobPayMultiplier;
            //Other
            PlayerData.currentJobSelectedNumber = data.currentJobSelectedNumber;

        //Skill Data
        for (int i = 0; i < PlayerData.skillExpMaxValue.Length; i++)
        {
            PlayerData.skillEnabledStatus[i] = data.skillEnabledStatus[i];
            GameManager.skillsArray[i].SetActive(PlayerData.skillEnabledStatus[i]);
            PlayerData.skillExpCurrentValue[i] = data.skillExpCurrentValue[i];
            PlayerData.skillExpMaxValue[i] = data.skillExpMaxValue[i];
            //PlayerData.skillLvlLoading[i] = true; not sure that needed
            PlayerData.skillLvlValue[i] = data.skillLvlValue[i];            
        }
        PlayerData.isSkillActive = data.isSkillActive;
        PlayerData.currentSkillReqNumber = data.currentSkillReqNumber;
            //Effects
            for (int i = 0; i < PlayerData.skillMultipliersArray.Length; i++)
            {
            PlayerData.skillMultipliersArray[i] = data.skillMultipliersArray[i];
            }

        //Reincarnation Data
        PlayerData.jobIncMultR = data.jobIncMultR;
        PlayerData.jobIncMultLvlR = data.jobIncMultLvlR;
        PlayerData.jobExpMultR = data.jobExpMultR;
        PlayerData.jobExpMultLvlR = data.jobExpMultLvlR;
        PlayerData.skillExpMultR = data.skillExpMultR;
        PlayerData.skillExpMultLvlR = data.skillExpMultLvlR;
        PlayerData.techExpMultR = data.techExpMultR;
        PlayerData.techExpMultLvlR = data.techExpMultLvlR;
        PlayerData.techCostMultR = data.techCostMultR;
        PlayerData.techCostMultLvlR = data.techCostMultLvlR;
    }
        
    private void NewGameDataLoad() //Loading start values of all saveble variables
    {
        //Timer Data
        PlayerData.days = 0;
        PlayerData.years = 20;

        //Job Data
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            PlayerData.jobExpMaxValue[i] = 100;
            PlayerData.jobLvlLoading[i] = true;
            PlayerData.jobExpCurrentValue[i] = 0;
            PlayerData.jobLvlValue[i] = 0;
            GameManager.jobsArray[i].SetActive(false); //Deactivating all jobs            
            PlayerData.jobEnabledStatus[i] = false;            
        }
        GameManager.jobsArray[0].SetActive(true); //First job is active from the start
        PlayerData.jobEnabledStatus[0] = true;
        PlayerData.currentJobReqNumber = 1;
        PlayerData.isJobActive = false;
        //Income
            for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
            {
                PlayerData.jobPayMultiplier[i] = 1;
            }
            PlayerData.currentBasicJobPayment = 0;
            PlayerData.currentJobPayMultiplier = 0;
            //Other
            PlayerData.currentJobSelectedNumber = 0;

        //Skill data
        for (int i = 0; i < PlayerData.skillExpMaxValue.Length; i++)
        {
            PlayerData.skillExpMaxValue[i] = 100;
            //PlayerData.skillLvlLoading[i] = true; not sure that needed
            PlayerData.skillExpCurrentValue[i] = 0;
            PlayerData.skillLvlValue[i] = 0;
            GameManager.skillsArray[i].SetActive(false); //Deactivating all skills            
            PlayerData.skillEnabledStatus[i] = false;
        }
        GameManager.skillsArray[0].SetActive(true); //First skill is active from the start
        PlayerData.skillEnabledStatus[0] = true;
        PlayerData.currentSkillReqNumber = 1;
        PlayerData.isSkillActive = false;
            //Effects
            for (int i = 0; i < PlayerData.skillMultipliersArray.Length; i++)
            {
                PlayerData.skillMultipliersArray[i] = 1;
            }

        //Reincarnation Data
        PlayerData.jobIncMultR = 0;
        PlayerData.jobExpMultLvlR = 0;
        PlayerData.jobExpMultR = 0;
        PlayerData.jobExpMultLvlR = 0;
        PlayerData.skillExpMultR = 0;
        PlayerData.skillExpMultLvlR = 0;
        PlayerData.techExpMultR = 0;
        PlayerData.techExpMultLvlR = 0;
        PlayerData.techCostMultR = 0;
        PlayerData.techCostMultLvlR = 0;
    }

    public static void ReincarnationDataLoad() //Reset all Data, except Reincarnation Data
    {
        //Timer Data
        PlayerData.days = 0;
        PlayerData.years = 20;

        //Job Data
        for (int i = 0; i < PlayerData.jobExpMaxValue.Length; i++)
        {
            PlayerData.jobExpMaxValue[i] = 100;
            PlayerData.jobLvlLoading[i] = true;
            PlayerData.jobExpCurrentValue[i] = 0;            
            PlayerData.jobLvlValue[i] = 0;
            GameManager.jobsArray[i].SetActive(false); //Deactivating all jobs            
            PlayerData.jobEnabledStatus[i] = false;
        }
        GameManager.jobsArray[0].SetActive(true); //First job is active from the start
        PlayerData.jobEnabledStatus[0] = true;
        PlayerData.currentJobReqNumber = 1;
        PlayerData.isJobActive = false;
        //Income
        PlayerData.currentBasicJobPayment = 0;
        PlayerData.currentJobPayMultiplier = 0;
        //Other
        PlayerData.currentJobSelectedNumber = 0;

        //Skill data
        for (int i = 0; i < PlayerData.skillExpMaxValue.Length; i++)
        {
            PlayerData.skillExpMaxValue[i] = 100;
            //PlayerData.skillLvlLoading[i] = true;
            PlayerData.skillExpCurrentValue[i] = 0;
            PlayerData.skillLvlValue[i] = 0;
            GameManager.skillsArray[i].SetActive(false); //Deactivating all skills            
            PlayerData.skillEnabledStatus[i] = false;
        }
        GameManager.skillsArray[0].SetActive(true); //First skill is active from the start
        PlayerData.skillEnabledStatus[0] = true;
        PlayerData.currentSkillReqNumber = 1;
        PlayerData.isSkillActive = false;
            //Effects
            for (int i = 0; i < PlayerData.skillMultipliersArray.Length; i++)
            {
                PlayerData.skillMultipliersArray[i] = 1;
            }

    }
    public void LoadingGame() //This method used in GameManager at Start, to load data when loading Scene
    {        
        if (PlayerData.newOrContinueGame == 0) //New game from start menu
        {
            NewGameDataLoad();
            PlayerData.newOrContinueGame = 10;
        }
        else if (PlayerData.newOrContinueGame == 1) //Continue game from start menu, or loading game
        {
            Load();
            PlayerData.newOrContinueGame = 10;
        }
        else if (PlayerData.newOrContinueGame == 2) //Reincarnation
        {
            ReincarnationDataLoad();
            PlayerData.newOrContinueGame = 10;
            Time.timeScale = 1;
        }
    } 
}
