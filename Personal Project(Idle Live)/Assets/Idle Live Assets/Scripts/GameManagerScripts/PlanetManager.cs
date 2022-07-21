using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetManager : MonoBehaviour
{
    public GameObject[] planetStatusArray; //contains all different status objects
    public Slider[] sliderArray;//contains sliders of different condition objects
    private Slider planetSlider;
    public TextMeshProUGUI[] valueText; // 0 is planet
    public MenuChooser menuChooserScr;

    private float atmosphereKoef, oceanKoef, soilKoef; //how fast condition objects automatically decreased
    private float planetKoef; //How planet condition affects all conditions


    public bool isGameActive;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        planetSlider = gameObject.GetComponent<Slider>();
        sliderArray = new Slider[planetStatusArray.Length];        
        SliderLoadMethod(); //Method of getting component slider from all status objects
        LoadStartValues(); //Load starting value of statuses

        StartCoroutine(PlanetConditionValue());
        StartCoroutine(StatusObjectsReduction());

    }

    private void SliderLoadMethod()
    {
        for(int i = 0; i < planetStatusArray.Length; i ++)
        {
            sliderArray[i] = planetStatusArray[i].GetComponent<Slider>();
        }
        
    }
    
    private void LoadStartValues()
    {
        sliderArray[0].value = 500000; //Loading starting values of conditions 5000 - 50%
        sliderArray[1].value = 500000;
        sliderArray[2].value = 500000;
        sliderArray[3].value = 500000;
        sliderArray[4].value = 5000020;
        sliderArray[5].value = 500000;
        sliderArray[6].value = 500000;

        atmosphereKoef = 1; //how fast atmosphere automatically decreased koef
        soilKoef = 1;
        oceanKoef = 1;
        planetKoef = 1;
    }
    IEnumerator PlanetConditionValue()
    {
                
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);            

            planetSlider.value = 0;

            float value = 0;
            //loading visualisation in % of statuses
            valueText[1].text = (sliderArray[0].value / sliderArray[0].maxValue * 100).ToString("##.#") + "%";
            valueText[2].text = (sliderArray[1].value / sliderArray[1].maxValue * 100).ToString("##.#") + "%";
            valueText[3].text = (sliderArray[2].value / sliderArray[2].maxValue * 100).ToString("##.#") + "%";
            valueText[4].text = (sliderArray[3].value / sliderArray[3].maxValue * 100).ToString("##.#") + "%";
            valueText[5].text = (sliderArray[4].value / sliderArray[4].maxValue * 100).ToString("##.#") + "%";
            valueText[6].text = (sliderArray[5].value / sliderArray[5].maxValue * 100).ToString("##.#") + "%";
            valueText[7].text = (sliderArray[6].value / sliderArray[6].maxValue * 100).ToString("##.#") + "%";


            for (int i = 0; i < planetStatusArray.Length; i++)
            {
                value += sliderArray[i].value;
            }

            planetSlider.value = value / planetStatusArray.Length;
            valueText[0].text = (planetSlider.value / planetSlider.maxValue * 100).ToString("##.#") + "%";

            if((planetSlider.value / planetSlider.maxValue * 100) < 20)
            {
                //menuChooserScr.ReincarnationCanvas();
            }
            
        }
    }

    IEnumerator StatusObjectsReduction()
    {
                
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);

            ConditionDependencies(); //Calculating dependencies

            sliderArray[0].value -= 200 * atmosphereKoef * planetKoef;
            sliderArray[1].value -= 200 * planetKoef;
            sliderArray[2].value -= 200 * planetKoef;
            sliderArray[3].value -= 200 * planetKoef;
            sliderArray[4].value -= 400 * oceanKoef * planetKoef; //Ocean Decrease slower basically because MaxValue is 10x bigger
            sliderArray[5].value -= 200 * soilKoef * planetKoef;
            sliderArray[6].value -= 200 * planetKoef;

        }
    }

    

    private void ConditionDependencies()
    {
        //Atmosphere from Forrest dependecy
        if (300000 < sliderArray[1].value && sliderArray[1].value < 800000)
        {
            atmosphereKoef = 1;
        }
        else if (sliderArray[1].value < 300000)
        {
            atmosphereKoef = 2;
        }
        else if (sliderArray[1].value > 799999)
        {
            atmosphereKoef = 0.5f;
        }

        //Ocaen from Rivers dependecy
        if (300000 < sliderArray[3].value && sliderArray[3].value < 800000)
        {
            oceanKoef = 1;
        }
        else if (sliderArray[3].value < 300000)
        {
            oceanKoef = 2;
        }
        else if (sliderArray[3].value > 799999)
        {
            oceanKoef = 0.5f;
        }

        //Soil from Garbage dependecy
        if (300000 < sliderArray[6].value && sliderArray[6].value < 800000)
        {
            soilKoef = 1;
        }
        else if (sliderArray[6].value < 300000)
        {
            soilKoef = 2;
        }
        else if (sliderArray[6].value > 799999)
        {
            soilKoef = 0.5f;
        }

        //All from Planet dependecy
        if (300000 < planetSlider.value && planetSlider.value < 800000)
        {
            planetKoef = 1;
        }
        else if (planetSlider.value < 300000)
        {
            planetKoef = 2;
        }
        else if (planetSlider.value > 799999)
        {
            planetKoef = 0.5f;
        }
    }
}
