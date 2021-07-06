using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapHead : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }
}
