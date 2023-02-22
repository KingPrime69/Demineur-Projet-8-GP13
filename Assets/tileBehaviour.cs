using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class tileBehaviour : MonoBehaviour
{
    bool isBomb;
    int numberBombs;

    [SerializeField] Sprite neutral, flag ,bomb, clicked;
    SpriteRenderer spriteRenderer;
    
    TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        txt= GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clicked () 
    { 
        if (isBomb)
        {
            //gameOver
        }
        else 
        {
            spriteRenderer.sprite = clicked;
            if (numberBombs > 0)
            {
                txt.text = numberBombs.ToString();
            }
        }
    }
}
