using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnimation : MonoBehaviour
{
    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }

    private void OnEnable()
    {
        Destroy(this.gameObject, 2);
    }
}
