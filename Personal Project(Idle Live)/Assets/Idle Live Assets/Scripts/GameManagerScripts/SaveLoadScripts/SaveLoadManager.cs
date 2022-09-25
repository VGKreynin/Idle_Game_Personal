using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
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
        public bool jobIsActive;
        public bool[] jobEnabledStatusArray = new bool[8];
        public float[] jobExpCurrentValueArray = new float[8];
        public float[] jobExpMaxValueArray = new float[8];        
        public int[] jobLvlValueArray = new int[8];
        public int jobCurrentReqNumber;
        public int jobCurrentSelectedNumber; //USed to show current job on main screen
            //Income
            public float currentBasicJobPayment; //Use to calculate income
            public float[] jobPayMultiplierArray = new float[8]; //Use to save current income multiplier from lvl of job
            public float currentJobPayMultiplier; //Use to calculate income
            //Other
            public int currentJobSelectedNumber; //USed to show current job on main screen

        //Skill Data
        public bool skillIsActive;
        public bool[] skillEnabledStatusArray = new bool[5]; //Activ or not particular job
        public float[] skillExpCurrentValueArray = new float[5]; //Job current progress values
        public float[] skillExpMaxValueArray = new float[5]; //Job max values        
        public int[] skillLvlValueArray = new int[5];
        public int skillCurrentReqNumber; //According to this number the requiremets shows for next skill
        public int skillCurrentSelectedNumber; //USed to show current job on main screen

        public float enduranceMultiplierSkillExp;
        public float discMultiplierJobExp; //Discipline skill affects exp progress of all jobs
        public float motivMultiplierJobPay; //Motivation skill affects payment progress of all jobs
        public float negotiationMultiplierEcoIncome; //Negotiations skill affects Ecology income
        public float managementMultiplierEcoCostDecr; //Management skill affects Ecology cost decrease

        //Technology Data
        public float[] techMultipliersArray = new float[8]; //Contains multipliers of all Eco technologies

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
        data.days = SavableData.days;
        data.years = SavableData.years;

        //Job Data
        data.jobIsActive = SavableData.jobIsActive;
        data.jobEnabledStatusArray = SavableData.jobEnabledStatusArray;
        data.jobExpCurrentValueArray = SavableData.jobExpCurrentValueArray;
        data.jobExpMaxValueArray = SavableData.jobExpMaxValueArray;
        data.jobLvlValueArray = SavableData.jobLvlValueArray;        
        data.jobCurrentReqNumber = SavableData.jobCurrentReqNumber;   
        data.jobCurrentSelectedNumber = SavableData.jobCurrentSelectedNumber;
            //Income            
            data.jobPayMultiplierArray = SavableData.jobPayMultiplierArray;
            data.currentBasicJobPayment = SavableData.currentBasicJobPayment;
            data.currentJobPayMultiplier = SavableData.currentJobPayMultiplier;            

        //Skill Data
        data.skillIsActive = SavableData.skillIsActive;
        data.skillEnabledStatusArray = SavableData.skillEnabledStatusArray;
        data.skillExpCurrentValueArray = SavableData.skillExpCurrentValueArray;
        data.skillExpMaxValueArray = SavableData.skillExpMaxValueArray;
        data.skillLvlValueArray = SavableData.skillLvlValueArray;
        data.skillCurrentReqNumber = SavableData.skillCurrentReqNumber;
        data.skillCurrentSelectedNumber = SavableData.skillCurrentSelectedNumber;

        data.enduranceMultiplierSkillExp = SavableData.enduranceMultiplierSkillExp;
        data.discMultiplierJobExp = SavableData.discMultiplierJobExp;
        data.motivMultiplierJobPay = SavableData.motivMultiplierJobPay;
        data.negotiationMultiplierEcoIncome = SavableData.negotiationMultiplierEcoIncome;
        data.managementMultiplierEcoCostDecr = SavableData.managementMultiplierEcoCostDecr;

        //Technology Data
        data.techMultipliersArray = SavableData.techMultipliersArray;

        //Reincarnation Data
        data.jobIncMultR = SavableData.jobIncMultR;
        data.jobIncMultLvlR = SavableData.jobIncMultLvlR;
        data.jobExpMultR = SavableData.jobExpMultR;
        data.jobExpMultLvlR = SavableData.jobExpMultLvlR;
        data.skillExpMultR = SavableData.skillExpMultR;
        data.skillExpMultLvlR = SavableData.skillExpMultLvlR;
        data.techExpMultR = SavableData.techExpMultR;
        data.techExpMultLvlR = SavableData.techExpMultLvlR;
        data.techCostMultR = SavableData.techCostMultR;
        data.techCostMultLvlR = SavableData.techCostMultLvlR;

    }
    
    private void LoadDataUpdate()
    {        
        //Timer Data        
        SavableData.days = data.days;
        SavableData.years = data.years;

        //Job Data
        for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
        {           
            StaticFinalData.jobsArray[i].SetActive(SavableData.jobEnabledStatusArray[i]);    
        }
        SavableData.jobIsActive = data.jobIsActive;
        SavableData.jobEnabledStatusArray = data.jobEnabledStatusArray;        
        SavableData.jobExpCurrentValueArray = data.jobExpCurrentValueArray;
        SavableData.jobExpMaxValueArray = data.jobExpMaxValueArray;        
        SavableData.jobLvlValueArray = data.jobLvlValueArray;        
        SavableData.jobCurrentReqNumber = data.jobCurrentReqNumber;
        SavableData.jobCurrentSelectedNumber = data.jobCurrentSelectedNumber;
            //Income
            SavableData.jobPayMultiplierArray = data.jobPayMultiplierArray;
            SavableData.currentBasicJobPayment = data.currentBasicJobPayment;
            SavableData.currentJobPayMultiplier = data.currentJobPayMultiplier;            

        //Skill Data
        for (int i = 0; i < SavableData.skillExpMaxValueArray.Length; i++)
        {
            StaticFinalData.skillsArray[i].SetActive(SavableData.skillEnabledStatusArray[i]);           
        }
        SavableData.skillIsActive = data.skillIsActive;
        SavableData.skillEnabledStatusArray = data.skillEnabledStatusArray;
        SavableData.skillExpCurrentValueArray = data.skillExpCurrentValueArray;
        SavableData.skillExpMaxValueArray = data.skillExpMaxValueArray;
        SavableData.skillLvlValueArray = data.skillLvlValueArray;
        SavableData.skillCurrentReqNumber = data.skillCurrentReqNumber;
        SavableData.skillCurrentSelectedNumber = data.skillCurrentSelectedNumber;

        SavableData.enduranceMultiplierSkillExp = data.enduranceMultiplierSkillExp;
        SavableData.discMultiplierJobExp = data.discMultiplierJobExp;
        SavableData.motivMultiplierJobPay = data.motivMultiplierJobPay;
        SavableData.negotiationMultiplierEcoIncome = data.negotiationMultiplierEcoIncome;
        SavableData.managementMultiplierEcoCostDecr = data.managementMultiplierEcoCostDecr;

        //Technology Data
        data.techMultipliersArray = SavableData.techMultipliersArray;

        //Reincarnation Data
        SavableData.jobIncMultR = data.jobIncMultR;
        SavableData.jobIncMultLvlR = data.jobIncMultLvlR;
        SavableData.jobExpMultR = data.jobExpMultR;
        SavableData.jobExpMultLvlR = data.jobExpMultLvlR;
        SavableData.skillExpMultR = data.skillExpMultR;
        SavableData.skillExpMultLvlR = data.skillExpMultLvlR;
        SavableData.techExpMultR = data.techExpMultR;
        SavableData.techExpMultLvlR = data.techExpMultLvlR;
        SavableData.techCostMultR = data.techCostMultR;
        SavableData.techCostMultLvlR = data.techCostMultLvlR;
    }
        
    private void NewGameDataLoad() //Loading start values of all saveble variables
    {
        //Timer Data
        SavableData.days = 0;
        SavableData.years = 20;

        //Job Data
        for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
        {
            SavableData.jobExpMaxValueArray[i] = 100;           
            SavableData.jobExpCurrentValueArray[i] = 0;
            SavableData.jobLvlValueArray[i] = 0;
            StaticFinalData.jobsArray[i].SetActive(false); //Deactivating all jobs            
            SavableData.jobEnabledStatusArray[i] = false;            
        }
        StaticFinalData.jobsArray[0].SetActive(true); //First job is active from the start
        SavableData.jobEnabledStatusArray[0] = true;
        SavableData.jobCurrentReqNumber = 1;
        SavableData.jobIsActive = false;
        SavableData.jobCurrentSelectedNumber = 0;
        //Income
            for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
            {
                SavableData.jobPayMultiplierArray[i] = 1;
            }
            SavableData.currentBasicJobPayment = 0;
            SavableData.currentJobPayMultiplier = 0;          
            
        //Skill data
        for (int i = 0; i < SavableData.skillExpMaxValueArray.Length; i++)
        {
            SavableData.skillExpMaxValueArray[i] = 100;            
            SavableData.skillExpCurrentValueArray[i] = 0;
            SavableData.skillLvlValueArray[i] = 0;
            StaticFinalData.skillsArray[i].SetActive(false); //Deactivating all skills           
            SavableData.skillEnabledStatusArray[i] = false;
        }
        StaticFinalData.skillsArray[0].SetActive(true); //First skill is active from the start
        SavableData.skillEnabledStatusArray[0] = true;
        SavableData.skillCurrentReqNumber = 1;
        SavableData.skillIsActive = false;
        SavableData.skillCurrentSelectedNumber = 0;

        SavableData.enduranceMultiplierSkillExp = 1;
        SavableData.discMultiplierJobExp = 1;
        SavableData.motivMultiplierJobPay = 1;
        SavableData.negotiationMultiplierEcoIncome = 1;
        SavableData.managementMultiplierEcoCostDecr = 1;

        //Technology Data
        for (int i = 0; i < SavableData.techMultipliersArray.Length; i++)
        {
            SavableData.techMultipliersArray[i] = 1;

        }

        //Reincarnation Data
        SavableData.jobIncMultR = 0;
        SavableData.jobExpMultLvlR = 0;
        SavableData.jobExpMultR = 0;
        SavableData.jobExpMultLvlR = 0;
        SavableData.skillExpMultR = 0;
        SavableData.skillExpMultLvlR = 0;
        SavableData.techExpMultR = 0;
        SavableData.techExpMultLvlR = 0;
        SavableData.techCostMultR = 0;
        SavableData.techCostMultLvlR = 0;
    }

    public static void ReincarnationDataLoad() //Reset all Data, except Reincarnation Data
    {
        //Timer Data
        SavableData.days = 0;
        SavableData.years = 20;

        //Job Data
        for (int i = 0; i < SavableData.jobExpMaxValueArray.Length; i++)
        {
            SavableData.jobExpMaxValueArray[i] = 100;            
            SavableData.jobExpCurrentValueArray[i] = 0;            
            SavableData.jobLvlValueArray[i] = 0;
            StaticFinalData.jobsArray[i].SetActive(false); //Deactivating all jobs            
            SavableData.jobEnabledStatusArray[i] = false;
        }
        StaticFinalData.jobsArray[0].SetActive(true); //First job is active from the start
        SavableData.jobEnabledStatusArray[0] = true;
        SavableData.jobCurrentReqNumber = 1;
        SavableData.jobIsActive = false;
        SavableData.jobCurrentSelectedNumber = 0;
            //Income
            SavableData.currentBasicJobPayment = 0;
            SavableData.currentJobPayMultiplier = 0;                        

        //Skill data
        for (int i = 0; i < SavableData.skillExpMaxValueArray.Length; i++)
        {
            SavableData.skillExpMaxValueArray[i] = 100;            
            SavableData.skillExpCurrentValueArray[i] = 0;
            SavableData.skillLvlValueArray[i] = 0;
            StaticFinalData.skillsArray[i].SetActive(false); //Deactivating all skills            
            SavableData.skillEnabledStatusArray[i] = false;
        }
        StaticFinalData.skillsArray[0].SetActive(true); //First skill is active from the start
        SavableData.skillEnabledStatusArray[0] = true;
        SavableData.skillCurrentReqNumber = 1;
        SavableData.skillIsActive = false;
        SavableData.skillCurrentSelectedNumber = 0;

        SavableData.enduranceMultiplierSkillExp = 1;
        SavableData.discMultiplierJobExp = 1;
        SavableData.motivMultiplierJobPay = 1;
        SavableData.negotiationMultiplierEcoIncome = 1;
        SavableData.managementMultiplierEcoCostDecr = 1;

        //Technology Data
        for (int i = 0; i < SavableData.techMultipliersArray.Length; i++)
        {
            SavableData.techMultipliersArray[i] = 1;
            
        }
    }
    public void LoadingGame() //This method used in GameManager at Start, to load data when loading Scene
    {        
        if (SavableData.newOrContinueGame == 0) //New game from start menu
        {
            NewGameDataLoad();
            SavableData.newOrContinueGame = 10;
        }
        else if (SavableData.newOrContinueGame == 1) //Continue game from start menu, or loading game
        {
            Load();
            SavableData.newOrContinueGame = 10;
        }
        else if (SavableData.newOrContinueGame == 2) //Reincarnation
        {
            ReincarnationDataLoad();
            SavableData.newOrContinueGame = 10;
            Time.timeScale = 1;
        }
    }

    public void NewOrContinueValue(int newOrContinue)
    {
        SavableData.newOrContinueGame = newOrContinue;
        SceneManager.LoadScene("Main Scene");
    }

}
