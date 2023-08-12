using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateSpecial : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI specialValue;

    public void UpdateText(int count){
        int number1 = count / 10;
        int number2 = count % 10;
        specialValue.text = "<sprite name=\"" + number1.ToString() + "\"><sprite name=\"" + number2.ToString() + "\">";
    }
}
