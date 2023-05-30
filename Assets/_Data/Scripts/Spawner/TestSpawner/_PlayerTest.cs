using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerTest : MonoBehaviour
{
    public void CreateSquare()
    {
        SquareSpawner.Instance.Spawn(transform.position, transform.rotation);
    }
    
}
