using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class tileBehaviour : MonoBehaviour
{
    bool _isBomb;
    public int numberBombs;
    public bool[,] array { get; set; }

    [SerializeField] Sprite neutral, flag, bomb, clicked;


    SpriteRenderer spriteRenderer;

    SpawnerTile spawnerTile;

    Vector2Int pos;

    List<GameObject> Neighbour = new List<GameObject>();

    List<GameObject> _Bombs = new List<GameObject>();

    //SpriteRenderer _tempSpriteRenderer;

    TextMeshPro txt;
    // Start is called before the first frame update
    void Start()
    {
        spawnerTile = GameObject.Find("TileSpawner").GetComponent<SpawnerTile>();
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
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    public void HoverEnd()
    {
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
    }


    public void LeftClicked()
    {
        if (!spawnerTile.firstClick)
        {
            spawnerTile.InitBomb(array, pos);
            spawnerTile.firstClick = true;
            WinLose.isPlaying = true;   // Starts considering player actions
        }
        if (spriteRenderer.sprite != neutral) return;
        if (_isBomb)
        {
            WinLose.isPlaying = false;   // Stop considering player actions 
            WinLose.isBombed = true;     // Indicates that player has lost
            spriteRenderer.sprite = bomb;
            StartCoroutine(BombReveal(gameObject, 0));
            foreach (GameObject go in spawnerTile.tileArray)
            {
                if (go.GetComponent<tileBehaviour>()._isBomb)
                {
                    if (go.GetComponent<SpriteRenderer>().sprite != go.GetComponent<tileBehaviour>().flag)
                        _Bombs.Add(go);
                    //FloodFillBomb(gameObject);
                    Shuffle<GameObject>();
                }
            }

            int i = 0;
            foreach(GameObject go in _Bombs)
            {
                i++;
                if (go.GetComponent<tileBehaviour>().pos == pos) continue;
                StartCoroutine(BombReveal(go, i));
            }
            foreach (GameObject go in _Bombs)
            {
                if (!go.transform.GetChild(1).gameObject.IsDestroyed())
                {
                    go.transform.GetChild(1).gameObject.SetActive(false);
                }
            }
            StartCoroutine(LoadEndScene(6f));

        }
        else
        {
            Difficulty.nbReveal++;
            if (Difficulty.nbReveal == (Difficulty.Size.x * Difficulty.Size.y) - Difficulty.nbBombs)
            {
                WinLose.winLoseTitle = "Victory !";
                
                SceneManager.LoadScene("EndGame");
            }

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

    private IEnumerator LoadEndScene(float time)
    {
        yield return new WaitForSeconds(time);
        WinLose.winLoseTitle = "Defeat";

        SceneManager.LoadScene("EndGame");
    }

    private IEnumerator BombReveal(GameObject go, int i)
    {
        yield return new WaitForSeconds((5f / spawnerTile.nbBombs) * i);

        go.GetComponent<SpriteRenderer>().sprite = go.GetComponent<tileBehaviour>().bomb;
        go.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void RightClick()
    {
        if (spriteRenderer.sprite == flag) spriteRenderer.sprite = neutral;
        else if (spriteRenderer.sprite == neutral) spriteRenderer.sprite = flag;
    }

    public void MiddleClickPress()
    {
        if (spriteRenderer.sprite == clicked)
        {
            int flagInNeighbour = 0;
            foreach (GameObject go in Neighbour)
            {
                tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();
                if (go.GetComponent<SpriteRenderer>().sprite == tilebehaviour.flag)
                {
                    flagInNeighbour++;
                }
            }

            foreach (GameObject go in Neighbour)
            {
                tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();
                if (flagInNeighbour == numberBombs && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour.neutral)
                {
                    tilebehaviour.LeftClicked();
                }
            }
        }

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
                Difficulty.nbReveal++;
                FloodFill(go);
            }
            else if (tilebehaviour.numberBombs > 0 && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour.neutral)
            {
                go.GetComponent<SpriteRenderer>().sprite = clicked;
                Difficulty.nbReveal++;
                tilebehaviour.txt.text = tilebehaviour.numberBombs.ToString();
            }
        }
    }

    void Shuffle<T>()
    {
        int n = _Bombs.Count;
        while(n>1)
        {
            n--;
            int k = Random.Range(0, n+1);
            GameObject value = _Bombs[k];
            _Bombs[k] = _Bombs[n];
            _Bombs[n] = value;
        }
    }
}
