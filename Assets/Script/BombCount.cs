using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombCount : MonoBehaviour
{
    TextMeshProUGUI _txtmesh;

    // Start is called before the first frame update
    void Start()
    {
        _txtmesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _txtmesh.text = Difficulty.nbFlags.ToString();
    }
}
