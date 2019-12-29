using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEffect : MonoBehaviour
{

    public float GetAngle(Vector3 vStart, Vector3 vEnd)
    {
        Vector3 v = vEnd - vStart;
        return Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
    }

    private void UmbrellaIdle()
    {
        float angle = GetAngle(transform.position, new Vector3(0f, 0f, 0f));
        Vector3 euler = new Vector3(0f, 0f, angle + 90f);
        transform.rotation = Quaternion.Euler(euler);
    }

    // Start is called before the first frame update
    void Start()
    {
        UmbrellaIdle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
