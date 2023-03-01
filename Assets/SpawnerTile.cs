using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerTile : MonoBehaviour
{
    [SerializeField] GameObject _original;
    [SerializeField] GameObject _camera;
    Vector2 _Size;
    int _nbBombs;
    // Start is called before the first frame update

    public bool firstClick;


    public bool[,] array;
    public GameObject[,] tileArray;
    void Start()
    {
        _Size = Difficulty.Size;
        _nbBombs = Difficulty.nbBombs;
        array = new bool[(int)_Size.x, (int)_Size.y];
        tileArray = new GameObject[(int)_Size.x, (int)_Size.y];


        for (int i = 0; i < _Size.x; i++)
        {
            for (int y = 0; y < _Size.y; y++)
            {

                GameObject go = Instantiate(_original, new Vector2(i, y) - _Size / 2, Quaternion.identity);
                go.GetComponent<tileBehaviour>().array = array;

                //if (array[i, y]) go.GetComponent<tileBehaviour>().SetBomb();
                go.GetComponent<tileBehaviour>().SetNbBombs(i, y, _Size);

                float calc = ((_Size.x * _Size.y / 100) + 1) * 3;

                _camera.GetComponent<Camera>().orthographicSize = calc;

                tileArray[i, y] = go;

            }
        }

        for (int i = 0; i < _Size.x; i++)
        {
            for (int y = 0; y < _Size.y; y++)
            {
                tileArray[i, y].GetComponent<tileBehaviour>().SetNeighbours(_Size, tileArray);
            }
        }
    }

    public void InitBomb(bool[,] array, Vector2Int posclick)
    {
        InitArray(array, posclick);

        for (int i = 0; i < _Size.x; i++)
        {
            for (int y = 0; y < _Size.y; y++)
            {

                //GameObject go = Instantiate(_original, new Vector2(i, y) - _Size / 2, Quaternion.identity);
                if (array[i, y]) tileArray[i, y].GetComponent<tileBehaviour>().SetBomb();
                tileArray[i, y].GetComponent<tileBehaviour>().array = array;
                tileArray[i, y].GetComponent<tileBehaviour>().SetNbBombs(i, y, _Size);

                float calc = ((_Size.x * _Size.y / 100) + 1) * 3;

                _camera.GetComponent<Camera>().orthographicSize = calc;

                //tileArray[i, y] = go;

            }
        }
    }

    public void InitArray(bool[,] array, Vector2Int posclick)
    {
        UnityEngine.Debug.Log(posclick);
        if (_nbBombs < _Size.x * _Size.y)
        {
            for (int i = 0; i < _nbBombs; i++)
            {
                int x, y;
                do
                {
                    x = Random.Range(0, (int)_Size.x);
                    y = Random.Range(0, (int)_Size.y);
                }
                while (array[x, y] || x == posclick.x && y == posclick.y || x >= posclick.x - 1 && x <= posclick.x + 1 && y >= posclick.y - 1 && y <= posclick.y + 1);
                array[x, y] = true;
            }
        }
    }
}
