using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using PlayMatch;

public class VariationView : MonoBehaviour
{
    //Toggles
    [SerializeField] private List<Toggle> _toggles = new List<Toggle>();

    //Toggle Group
    [SerializeField] private ToggleGroup _toggleGroup;

    //Buttons
    [SerializeField] private Button _btnTapToContinue;


    //Variables
    private int _baseValue = 0;
    private int _variationMultiplier = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetActiveToggleLabel();
        _btnTapToContinue.onClick.AddListener(() => OnTapToContinue());

        foreach (Toggle toggle in _toggles)
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnTapToContinue()
    {
        GameController.NotifyVariationSet(_baseValue, _variationMultiplier);
        Utility.SetActive(this.gameObject, false);
    }

    private void GetActiveToggleLabel()
    {
        Toggle activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();

        if (activeToggle != null)
        {
            string label = activeToggle.GetComponentInChildren<TMP_Text>().text;

            if (!string.IsNullOrEmpty(label) && label.Length >= 2)
            {
                char firstChar = label[0];
                char lastChar = label[label.Length - 1];

                int firstDigit = (int)char.GetNumericValue(firstChar);
                int lastDigit = (int)char.GetNumericValue(lastChar);

                SetVariation(firstDigit, lastDigit);
            }
            else
            {
                Debug.LogError($"Invalid label format");
            }
        }
        else
        {
            Debug.LogError($"No active toggle selected");
        }
    }

    private void SetVariation(int firstDigit, int lastDigit)
    {
        _baseValue = firstDigit;
        _variationMultiplier = lastDigit;
    }

    private void OnToggleValueChanged(bool state)
    {
        GetActiveToggleLabel();
    }
}
