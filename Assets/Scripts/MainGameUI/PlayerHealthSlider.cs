using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSlider : MonoBehaviour {
    Slider slider;
    [SerializeField] PlayerStatus playerStatus;
    // Use this for initialization
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = playerStatus.MaxHealth;
        slider.value = playerStatus.Health;
    }
}
