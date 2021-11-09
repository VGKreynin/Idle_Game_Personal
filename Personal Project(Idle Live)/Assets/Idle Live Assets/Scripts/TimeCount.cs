using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    public GameManager gameManagerScr;
    public GameObject reincarnationButton;
    public MenuChooser menuChooserScr;

    public TextMeshProUGUI yearsValueText;
    public TextMeshProUGUI daysValueText;
         
    // Update is called once per frame
    void Update()
    {
        TimeCounter(Time.deltaTime);
        yearsValueText.text = PlayerData.years.ToString("#");
        daysValueText.text = PlayerData.days.ToString("#");
    }

    public void TimeCounter(float addDay)
    {
        PlayerData.days -= addDay;
        if(PlayerData.days < 0)
        {
            PlayerData.years -= 1;
            PlayerData.days = 365;
        }
        ReincarnationButtonAppear();
        Reincarnation();
    }

    private void ReincarnationButtonAppear() //Button Reincarnation should appear at 5th year till the end
    {
        if(PlayerData.years == 19)
        {
            reincarnationButton.SetActive(true);
        }
    }

    private void Reincarnation() //Go to Reincarnation when year is below 0
    {
        if (PlayerData.years < 0)
        {
            menuChooserScr.ReincarnationCanvas();
           
        }
    }
}
