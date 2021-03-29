using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    List<GameObject> Player = new List<GameObject>();
    [SerializeField] float Distance;
    [SerializeField] GameObject FinishPoint;
    [SerializeField] Slider finishbar;

    public Slider SetGetFinishPoint
    {
        set { finishbar = value; }
        get { return finishbar; }
    }

    public GameObject AddPlayer
    {
        set { Player.Add(value); }
    }
    void Awake()
    {
        FinishPoint = GameObject.Find("Finish");

    }

    private void Start()
    {
        Distance = 700 - this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        finishbar.maxValue = Distance;
        finishbar.value = this.transform.position.z;
    }
}
