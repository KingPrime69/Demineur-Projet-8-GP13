using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerTile : MonoBehaviour
{
    [SerializeField] GameObject _original;
    [SerializeField] Vector2 _Size;

    [SerializeField] int _nbBombs;
    // Start is called before the first frame update
    public bool[,] array;

    void Start()
    {
         array = new bool[(int)_Size.x, (int)_Size.y];

        InitArray(array);

        for (int i = 0; i < _Size.x; i++)
        {
            for (int y = 0; y < _Size.y; y++)
            {

                GameObject go = Instantiate(_original, new Vector3(i, y), Quaternion.identity);
                if (array[i, y]) go.GetComponent<tileBehaviour>().SetBomb();
            }
        }
    }

    void InitArray(bool[,] array)
    {
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
                while (!array[x, y]);
                array[x, y] = true;
            }
        }
    }
}
