using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controls : MonoBehaviour
{
    public Button fireButton;
    public TMP_InputField functionInputField;
    public TMP_InputField parsedFunctionInputField;

    public GameObject selectedUnit; // TODO: Make this private and define the setter to SetSelectedUnit()
    private Gun gun;

    // Start is called before the first frame update
    void Start()
    {
        // SetSelectedUnit(selectedUnit);
        gun = selectedUnit.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedUnit(GameObject newSelectedUnit)
    {
        selectedUnit = newSelectedUnit;
        gun = selectedUnit.GetComponent<Gun>();
    }

    public void FireButtonOnClick()
    {
        if (gun.validExpression)
        {
            gun.Shoot();
        }
    }

    public void FunctionInputFieldOnValueChanged(string s)
    {
        gun.SetExpression(s);
        parsedFunctionInputField.text = gun.parsedExpression;

        ColorBlock buttonColors = fireButton.GetComponent<Button>().colors;
        if (gun.validExpression)
        {
            buttonColors.normalColor = new Color32(5, 142, 63, 255);
        }
        else
        {
            buttonColors.normalColor = new Color32(209, 16, 58, 255);
        }
        fireButton.GetComponent<Button>().colors = buttonColors;
    }
}
