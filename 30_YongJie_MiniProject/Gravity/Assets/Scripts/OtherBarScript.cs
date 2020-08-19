using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherBarScript : MonoBehaviour
{
    public Slider bar;
    public int regen;
    public int originalregen;

    void Start()
    {
        originalregen = regen;
    }
    void Update()
    {
        bar.value += Time.deltaTime * regen;

    }
    public void SetOther(int other)
    {
        bar.value = other;
    }

    public void SetMaxother(int other)
    {
        bar.maxValue = other;
        bar.value = other;
    }
}
