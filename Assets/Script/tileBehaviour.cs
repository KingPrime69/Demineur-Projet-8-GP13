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

    [SerializeField] Sprite _neutral, _flag, _bomb, _clicked;

    AudioSource _audioSrc;
    [SerializeField] AudioClip _lclick, _rclick, _rclikcReload, _mclick;

    SpriteRenderer _spriteRenderer;

    SpawnerTile _spawnerTile;

    Vector2Int _pos;

    List<GameObject> _Neighbour = new List<GameObject>();

    List<GameObject> _Bombs = new List<GameObject>();

    //SpriteRenderer _tempSpriteRenderer;

    bool _midClick = false;

    TextMeshPro _txt;
    // Start is called before the first frame update
    void Start()
    {
        _spawnerTile = GameObject.Find("TileSpawner").GetComponent<SpawnerTile>();
        _txt = GetComponentInChildren<TextMeshPro>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
       _audioSrc = GetComponent<AudioSource>();
    }

    public void SetNbBombs(int x, int y, Vector2 size)
    {
        _pos.x = x; _pos.y = y;
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
        for (int i = _pos.x - 1; i <= _pos.x + 1; i++)
        {
            for (int j = _pos.y - 1; j <= _pos.y + 1; j++)
            {
                if ((i != _pos.x || j != _pos.y) && (i >= 0 && j >= 0) && (i < (int)size.x && j < (int)size.y))
                {
                    _Neighbour.Add(tileArray[i, j]);
                }
            }
        }
    }

    public void HoverStart()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = Color.white;
    }

    public void HoverEnd()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
    }


    public void LeftClicked()
    {
        if (!_spawnerTile.firstClick)
        {
            _spawnerTile.InitBomb(array, _pos);
            _spawnerTile.firstClick = true;
            WinLose.isPlaying = true;   // Starts considering player actions
        }


        if (_spriteRenderer.sprite != _neutral) return;
        if(!_midClick) 
            _audioSrc.PlayOneShot(_lclick);
        _midClick = false;
        if (_isBomb)
        {
            WinLose.isPlaying = false;   // Stop considering player actions 
            WinLose.isBombed = true;     // Indicates that player has lost
            _spriteRenderer.sprite = _bomb;
            StartCoroutine(BombReveal(gameObject, 0));
            foreach (GameObject go in _spawnerTile.tileArray)
            {
                if (go.GetComponent<tileBehaviour>()._isBomb)
                {
                    if (go.GetComponent<SpriteRenderer>().sprite != go.GetComponent<tileBehaviour>()._flag)
                        _Bombs.Add(go);
                    //FloodFillBomb(gameObject);
                    Shuffle<GameObject>();
                }
            }

            int i = 0;
            foreach(GameObject go in _Bombs)
            {
                i++;
                if (go.GetComponent<tileBehaviour>()._pos == _pos) continue;
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

            _spriteRenderer.sprite = _clicked;
            if (numberBombs > 0)
            {
                _txt.text = numberBombs.ToString();
            }
            else
            {
                FloodFill(gameObject);
            }

        }
    }

    IEnumerator LoadEndScene(float time)
    {
        yield return new WaitForSeconds(time);
        WinLose.winLoseTitle = "Defeat";

        SceneManager.LoadScene("EndGame");
    }

    IEnumerator BombReveal(GameObject go, int i)
    {
        yield return new WaitForSeconds((5f / _spawnerTile.nbBombs) * i);

        go.GetComponent<SpriteRenderer>().sprite = go.GetComponent<tileBehaviour>()._bomb;
        go.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void RightClick()
    {
        if (_spriteRenderer.sprite == _flag)
        {
            _spriteRenderer.sprite = _neutral;
            _audioSrc.PlayOneShot(_rclikcReload);
            Difficulty.nbFlags++;
        }
        else if (_spriteRenderer.sprite == _neutral)
        {
            _spriteRenderer.sprite = _flag;
            _audioSrc.PlayOneShot(_rclick);
            Difficulty.nbFlags--;
        }
    }

    public void MiddleClickPress()
    {
        if (_spriteRenderer.sprite == _clicked)
        {
            int flagInNeighbour = 0;
            foreach (GameObject go in _Neighbour)
            {
                tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();
                if (go.GetComponent<SpriteRenderer>().sprite == tilebehaviour._flag)
                {
                    flagInNeighbour++;
                }
            }
            bool one = false;
            foreach (GameObject go in _Neighbour)
            {
                tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();
                if (flagInNeighbour == numberBombs && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour._neutral)
                {
                    if(!one)
                    {
                        _audioSrc.PlayOneShot(_mclick);
                        _midClick = true;
                        one = true;
                    }
                    tilebehaviour.LeftClicked();
                }
            }
        }

        if (_spriteRenderer.sprite == _neutral) _spriteRenderer.color = Color.white;
        foreach (GameObject go in _Neighbour)
        {
            if (go.GetComponent<SpriteRenderer>().sprite == _neutral) go.GetComponent<SpriteRenderer>().color = Color.white;
        }

    }

    public void MiddleClickRelease()
    {
        if (_spriteRenderer.sprite == _neutral) _spriteRenderer.color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
        foreach (GameObject go in _Neighbour)
        {
            if (go.GetComponent<SpriteRenderer>().sprite == _neutral) go.GetComponent<SpriteRenderer>().color = new Color(168f / 255f, 168f / 255f, 168f / 255f);
        }
    }

    public void SetBomb()
    {
        _isBomb = true;
    }

    void FloodFill(GameObject n)
    {
        foreach (GameObject go in n.GetComponent<tileBehaviour>()._Neighbour)
        {
            tileBehaviour tilebehaviour = go.GetComponent<tileBehaviour>();


            if (tilebehaviour.numberBombs == 0 && !tilebehaviour._isBomb && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour._neutral)
            {
                go.GetComponent<SpriteRenderer>().sprite = _clicked;
                Difficulty.nbReveal++;
                FloodFill(go);
            }
            else if (tilebehaviour.numberBombs > 0 && go.GetComponent<SpriteRenderer>().sprite == tilebehaviour._neutral)
            {
                go.GetComponent<SpriteRenderer>().sprite = _clicked;
                Difficulty.nbReveal++;
                tilebehaviour._txt.text = tilebehaviour.numberBombs.ToString();
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
