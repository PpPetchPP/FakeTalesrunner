using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckIP : NetworkBehaviour
{
    PlayerCon player;
    Rank rank;
    GameObject staminabar;
    GameObject ultimatebar;
    [SerializeField] Text CountTime;
    [SerializeField] GameObject finishbar;
    [SerializeField] Slider MapSlider;
    [SerializeField] GameObject MapPos;
    [SerializeField] Slider x;
    [SerializeField] public string Name_Player;

    //private void Awake()
    //{
    //    player = GetComponent<PlayerCon>();
    //    rank = GetComponent<Rank>();
    //    camv = Camera.main.GetComponent<Camer_Follow>();
    //}
    void Start()
    {
        Debug.Log("Run");
        if (!isLocalPlayer)
        {
            Debug.Log("Run1");
            player = GetComponent<PlayerCon>();
            player.Name_Player = Name_Player;
            rank = GetComponent<Rank>();
            MapPos = GameObject.Find("Pos");
            x = Instantiate(MapSlider, new Vector3(0, MapPos.transform.position.y, MapPos.transform.position.z), MapPos.transform.rotation) as Slider;
            x.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
            rank.SetGetFinishPoint = x.GetComponent<Slider>();
            player.enabled = false;

        }
        else if (isLocalPlayer)
        {
            Debug.Log("Run2");
            Debug.Log(ultimatebar);
            //Camer_Follow camv = Camera.main.GetComponent<Camer_Follow>();
            //camv.SetGetPlayer = this.gameObject;
            Camer_Follow.player = this.gameObject;
            ultimatebar = GameObject.FindGameObjectWithTag("UltimateBar");
            Debug.Log(ultimatebar);
            staminabar = GameObject.FindGameObjectWithTag("StaminaBar");
            Debug.Log(finishbar);
            finishbar = GameObject.FindGameObjectWithTag("FinishBar");
            CountTime = GameObject.Find("CountTime").GetComponent<Text>();
            Debug.Log(CountTime);
            rank = GetComponent<Rank>();
            player = GetComponent<PlayerCon>();
            player.SetGetStamina = staminabar.GetComponent<Slider>();
            player.SetGetultimatebar = ultimatebar.GetComponent<Slider>();
            rank.SetGetFinishPoint = finishbar.GetComponent<Slider>();
            player.SetGetSpeedText = GameObject.Find("Speed").GetComponent<Text>();
            player.SetGetContTimetext = CountTime;
            player.Name_Player = Name_Player;
            Debug.Log("Run3");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
