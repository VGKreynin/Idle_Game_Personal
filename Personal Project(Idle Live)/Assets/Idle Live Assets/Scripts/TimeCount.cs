using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public GameObject reincarnationButton;
    public MenuChooser menuChooserScr;

    public TextMeshProUGUI yearsValueText;
    public TextMeshProUGUI daysValueText;
         
    // Update is called once per frame
    void Update()
    {
        TimeCounter(Time.deltaTime);
        yearsValueText.text = SavableData.years.ToString("#");
        daysValueText.text = SavableData.days.ToString("#");
    }

    public void TimeCounter(float addDay)
    {
        SavableData.days -= addDay;
        if(SavableData.days < 0)
        {
            SavableData.years -= 1;
            SavableData.days = 365;
        }
        ReincarnationButtonAppear();
        //Reincarnation();
    }

    private void ReincarnationButtonAppear() //Button Reincarnation should appear at 5th year till the end
    {
        if(SavableData.years == 19)
        {
            reincarnationButton.SetActive(true);
        }
    }

    /*
    private void Reincarnation() //Go to Reincarnation when year is below 0
    {
        if (SavableData.years < 0)
        {
            menuChooserScr.ReincarnationCanvas();
           
        }
    }
    */
}
