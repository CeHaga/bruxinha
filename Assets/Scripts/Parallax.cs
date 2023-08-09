using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour{
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    float singleTextureWidth;

    void Start(){
        SetupTexture();
        if(scrollLeft){
            moveSpeed = -moveSpeed;
        }
    }

    private void SetupTexture(){
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }

    private void Scroll(){
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
    }

    private void CheckReset(){
        if((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0){
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }

    void Update(){
        Scroll();
        CheckReset();
    }
}
