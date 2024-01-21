using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider hpSlider;

    private CharacterStats _stats;
    public static HealthBarUI instance;

    private void OnEnable()
    {
        CharacterStats.onHealthChanged += UpdateHealthBar;
    }

    private void Awake()
    {
        if (instance != null)
        {
            //Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _stats = GetComponent<CharacterStats>();
        hpSlider = FindObjectOfType<Slider>();
        UpdateHealthBar();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        hpSlider.maxValue = _stats.GetMaxHealthValue();
        hpSlider.value = _stats.currentHealth;
        
    }

    private void OnDisable()
    {
        CharacterStats.onHealthChanged -= UpdateHealthBar;
    }
}
