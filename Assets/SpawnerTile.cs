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
    
    void Start()
    {
        bool[,] array = new bool[(int)_Size.x, (int)_Size.y];

        InitArray(array);

        for (int i = 0; i < _Size.x; i++)
        {
            for(int y = 0; y < _Size.y; y++)
            {
                
                GameObject go = Instantiate(_original, new Vector3(i, y), Quaternion.identity);
                if (array[i,y]) go.GetComponent<tileBehaviour>().SetBomb();
            }
        }
    }

    void InitArray(bool[,] array)
    {
        for (int i = 0; i < _nbBombs; i++)
        {
            int x = Random.Range(0, (int)_Size.x);
            int y = Random.Range(0, (int)_Size.y);
            array[x, y] = true;
        }
    }
}
