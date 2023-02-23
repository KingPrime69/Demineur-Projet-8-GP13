using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class tileBehaviour : MonoBehaviour
{
    bool _isBomb;
    int numberBombs;
    bool clickOver = false;
    public bool[,] array { get; set; }

    [SerializeField] Sprite neutral, flag, bomb, clicked;

    SpriteRenderer spriteRenderer;

    Vector2Int pos;

    List<GameObject> Neighbours;

    TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<TextMeshPro>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
    }

    public void RightClick()
    {
        if (spriteRenderer.sprite == flag) spriteRenderer.sprite = neutral;
        else if (spriteRenderer.sprite == neutral) spriteRenderer.sprite = flag;
    }

    public void MiddleClickPress()
    {
        // clickHover = false ? clickHover : !clickHover;
        foreach (GameObject go in Neighbours)
        {
           go.GetComponent<SpriteRenderer>().color = Color.white;
            // else go.GetComponent<SpriteRenderer>().color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
        }
    }

    public void MiddleClickRelease()
    {
        spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
    }

        public void SetBomb()
    {
        _isBomb = true;
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
                    UnityEngine.Debug.Log(tileArray[i, j]);
                    Neighbours.Add(tileArray[i, j]);
                }
            }
        }
    }
}
