using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Vector3 Vec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;
        Vec.z -= Time.deltaTime * 7;
        transform.localPosition = Vec;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Des")) 
        {
            Destroy(gameObject);
        }
    }
}
