using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camer_Follow : MonoBehaviour
{
    [SerializeField]public static GameObject player;
    Vector3 offset;
    float offsetz;
    bool first_run = true;

    public GameObject SetGetPlayer
    {
        set { player = value; }
        get { return player; }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && first_run)
        {
            transform.position = new Vector3(player.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            offset = this.transform.position;
            offsetz = player.transform.position.z + Mathf.Abs(offset.z);
            first_run = false;
        }
        if (player != null && !first_run) 
        {
            transform.position = new Vector3(player.transform.position.x, offset.y, player.transform.position.z - offsetz);
        }
    }
}
