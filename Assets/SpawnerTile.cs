using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTile : MonoBehaviour
{
    [SerializeField] GameObject original;
    [SerializeField] Vector2 Size;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for(int y = 0; y < Size.y; y++)
            {
                Instantiate(original, new Vector3(i, y), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
