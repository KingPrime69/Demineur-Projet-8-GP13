using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class tileBehaviour : MonoBehaviour
{
    public bool isBomb;
    int numberBombs;
    bool clickHover = false;

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

    public void LeftClicked () 
    {
        if (spriteRenderer.sprite == flag) return;
        if (isBomb)
        {
            //gameOver
            spriteRenderer.sprite = bomb;
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

    public void RightClick()
    {
        if (spriteRenderer.sprite == flag) spriteRenderer.sprite = neutral;
        else if (spriteRenderer.sprite == neutral) spriteRenderer.sprite = flag;
    }

    public void MiddleClick()
    {
        clickHover = false ? clickHover : !clickHover;

        if (clickHover) spriteRenderer.color = Color.white;
        else
        {
            spriteRenderer.color = new Color(100, 100, 100);
            UnityEngine.Debug.Log(clickHover);
        }

    }

    public void SetBomb()
    {
        isBomb = true;
    }
}
