using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class tileBehaviour : MonoBehaviour
{
    bool _isBomb;
    public int numberBombs;
    public bool[,] array { get; set; }

    [SerializeField] Sprite neutral, flag, bomb, clicked;

    SpriteRenderer spriteRenderer;

    Vector2Int pos;

    List<GameObject> Neighbour = new List<GameObject>();

    SpriteRenderer _tempSpriteRenderer;

    TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetNbBombs(int x, int y, Vector2 size)
    {
        pos.x = x; pos.y = y;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if ((i != x || j != y) && (i >= 0 && j >= 0) && (i < size.x && j < size.y))
                {
                    if (array[i, j]) numberBombs++;
                }
            }
        }
    }

    public void SetNeighbours(Vector2 size, GameObject[,] tileArray)
    {
        for (int i = pos.x - 1; i <= pos.x + 1; i++)
        {
            for (int j = pos.y - 1; j <= pos.y + 1; j++)
            {
                if ((i != pos.x || j != pos.y) && (i >= 0 && j >= 0) && (i < (int)size.x && j < (int)size.y))
                {
                    Neighbour.Add(tileArray[i, j]);
                }
            }
        }
    }

    public void HoverStart()
    {
        spriteRenderer.color = Color.white;
    }

    public void HoverEnd()
    {
        spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
    }

    public void LeftClicked()
    {
        if (spriteRenderer.sprite == flag) return;
        if (_isBomb)
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
            else
            {
                FloodFill(gameObject);
            }
        }
    }

    public void RightClick()
    {
        if (spriteRenderer.sprite == flag) spriteRenderer.sprite = neutral;
        else if (spriteRenderer.sprite == neutral) spriteRenderer.sprite = flag;
    }

    public void MiddleClickPress()
    {
        if (spriteRenderer.sprite == neutral) spriteRenderer.color = Color.white;
        foreach (GameObject go in Neighbour)
        {
            if (go.GetComponent<SpriteRenderer>().sprite == neutral) go.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void MiddleClickRelease()
    {
        if (spriteRenderer.sprite == neutral) spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
        foreach (GameObject go in Neighbour)
        {
            if (go.GetComponent<SpriteRenderer>().sprite == neutral) go.GetComponent<SpriteRenderer>().color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
        }
    }

    public void SetBomb()
    {
        _isBomb = true;
    }



    void FloodFill(GameObject n)
    {
        foreach (GameObject go in n.GetComponent<tileBehaviour>().Neighbour)
        {
            tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();

            
            if (tilebehaviour.numberBombs == 0 && !tilebehaviour._isBomb && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour.neutral)
            {
                go.GetComponent<SpriteRenderer>().sprite = clicked;
                FloodFill(go);
            }
            else if (tilebehaviour.numberBombs > 0)
            {
                go.GetComponent<SpriteRenderer>().sprite = clicked;
                tilebehaviour.txt.text = tilebehaviour.numberBombs.ToString();
            }
            //UnityEngine.Debug.Log("2");
        }

    }
}
