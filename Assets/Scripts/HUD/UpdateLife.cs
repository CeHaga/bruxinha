using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateLife : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI lifeValue;

    public void UpdateText(int count){
        int number1 = count / 10;
        int number2 = count % 10;
        lifeValue.text = "<sprite name=\"" + number1.ToString() + "\"><sprite name=\"" + number2.ToString() + "\">";
    }
}
