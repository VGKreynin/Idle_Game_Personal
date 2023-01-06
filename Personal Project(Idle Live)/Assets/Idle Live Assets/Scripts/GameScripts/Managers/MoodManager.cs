using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoodManager : MonoBehaviour
{
    [SerializeField] private Slider moodSlider;
    [SerializeField] private TextMeshProUGUI percentValueText;


    // Start is called before the first frame update
    void Start()
    {
        moodSlider.value = 75;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
