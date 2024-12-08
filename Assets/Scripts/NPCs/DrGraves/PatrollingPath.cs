using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingPath : MonoBehaviour
{
    public List<Transform> nodes = new();
    void Start()
    {
        nodes.Clear();

        Transform[] childNodes = GetComponentsInChildren<Transform>();
        for (int i = 1; i < childNodes.Length; i++)
        {
            nodes.Add(childNodes[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
