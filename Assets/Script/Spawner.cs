using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    [SerializeField] GameObject[] pat = new GameObject[4];
    int x;
    void Start()
    {
        if (!isLocalPlayer)
        {
            InvokeRepeating("RandomX", 6, 6);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void RandomX() 
    {
        x = Random.Range(0, 4);
        CmdCreate(x);
    }
    [Command]
    void CmdCreate(int rad) 
    {
        Rand(rad);
        RpcCreate(rad);
    }
    [ClientRpc]
    void RpcCreate(int rad)
    {
        if (!isServer) 
        {
            Rand(rad);
        }
    }
    void Rand(int rad)
    {
        Instantiate(pat[rad], this.gameObject.transform.position, this.transform.rotation);
    }
}
