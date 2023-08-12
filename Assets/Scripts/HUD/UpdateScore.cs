using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI scoreValue;

    public void UpdateText(int score){
        scoreValue.text = "";
        for(int i = 9; i >= 0; i--){
            scoreValue.text += formatText(score, i);
        }
    }

    private string formatText(int score, int position){
        int number;
        if(position == 9){   //Primeira casa
            number = score / (int)(Mathf.Pow(10, position));
        }else if(position == 0){ // Ultima casa
            number = score % 10;
        }else{  //Demais casas
            number = (score / (int)(Mathf.Pow(10, position))) % 10;
        }
        return "<sprite name=\"" + number.ToString() + "\">";
    }
}
