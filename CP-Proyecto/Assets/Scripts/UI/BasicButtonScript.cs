using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicButtonScript : MonoBehaviour
{
    [Header("Button Parameters")]
    [SerializeField] Color disabledColor;
    [SerializeField] Color enabledColor;
    [SerializeField] bool isEnabled;
    private ColorBlock colors;

    private void Awake()
    {
        colors = GetComponent<Button>().colors;
    }

    public void SwapColor()
    {
        isEnabled = !isEnabled;
        if (isEnabled) colors.normalColor = enabledColor;
        else colors.normalColor = disabledColor;

        GetComponent<Button>().colors = colors;
    }
}
