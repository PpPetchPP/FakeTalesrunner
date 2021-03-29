using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerCon : NetworkBehaviour
{
    [SerializeField] public string Name_Player;
    Vector3 pos;
    private float default_speed = 5;
    [SerializeField] private float speed = 5;
    private float max_stamina = 100;
    [SerializeField] private float stamina = 100;
    private float f_jump = 8;
    private bool db_jump;
    private int db_jump_max = 2;
    private int db_jump_count = 0;
    private int Ulti = 100;
    public bool Dash;
    public bool Dash_Effect_Activate = false;
    public bool Hit_anim;
    public bool fail = false;
    private bool use_stamina;
    private bool speed_drop;
    [SerializeField] public bool FinishRun = false;
    [SyncVar(hook = "StartCountTime")]
    [SerializeField] public bool getFinish;
    [SerializeField] private bool Imu = false;
    private Vector3 VecRotate;
    //[SerializeField] float Distance;
    //[SerializeField] GameObject FinishPoint;
    [SerializeField] Animator anim;
    [SerializeField] Slider staminabar;
    [SerializeField] Slider ultimatebar;
    [SerializeField] Text ContTimetext;
    //[SerializeField] Slider finishbar;
    [SerializeField] Text speed_text;
    [SyncVar(hook = "runAnimation")]
    public string name_anim = null;
    Vector3 Vec;
    Rigidbody rb;

    public Text SetGetContTimetext
    {
        set { ContTimetext = value; }
        get { return ContTimetext; }
    }
    public Slider SetGetStamina
    {
        set { staminabar = value; }
        get { return staminabar; }
    }

    public Slider SetGetultimatebar
    {
        set { ultimatebar = value; }
        get { return ultimatebar; }
    }
    //public Slider SetGetFinishPoint
    //{
    //    set { finishbar = value; }
    //    get { return finishbar; }
    //}

    public Text SetGetSpeedText
    {
        set { speed_text = value; }
        get { return speed_text; }
    }
    void Start()
    {
        pos = this.gameObject.transform.position;
        //FinishPoint = GameObject.Find("Finish");
        stamina = max_stamina;
        rb = GetComponent<Rigidbody>();
        //Distance = FinishPoint.transform.position.z - this.transform.position.z;
    }

    void Update()
    {
        //finishbar.maxValue = Distance;
        //finishbar.value = this.transform.position.z;
        speed_text.text = "Name : "+ Name_Player;
        if (!fail)
        {
            movement();
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Z)) 
        {
            use_Ultimage();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Dash == true && Hit_anim == true)
            {
                StartCoroutine(Dash_Effect());
            }
            else if (stamina > 0)
            {
                speed = default_speed + 10;
                use_stamina = true;
            }
        }
        restamina();
    }
    private void movement()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.gameObject.transform.position = pos;
        }
        if (Input.GetKeyUp(KeyCode.Z) && Dash_Effect_Activate == false)
        {
            use_stamina = false;
            if (speed_drop == true)
            {
                speed = default_speed;
            }
        }
        Vec = transform.localPosition;
        if (Hit_anim == false)
        {
            if (Input.GetKey("up"))
            {
                Vec.z += Time.deltaTime * speed;
                Rotate("up");
            }
            if (Input.GetKey("down"))
            {
                Vec.z -= Time.deltaTime * speed;
                Rotate("down");
            }
            if (Input.GetKey("left"))
            {
                Vec.x -= Time.deltaTime * speed;
                Rotate("left");
            }
            if (Input.GetKey("right"))
            {
                Vec.x += Time.deltaTime * speed;
                Rotate("right");
            }
            if (Input.GetKeyDown(KeyCode.LeftControl) && db_jump == true)
            {
                Vec.y += Time.deltaTime * speed;
                rb.velocity = Vector3.zero;
                if (db_jump_count == 1)
                {
                    rb.AddForce(new Vector3(0, f_jump * 0.8f, 0), ForceMode.Impulse);
                    CmdrunAnumation("DbJump");
                }
                else
                {
                    rb.AddForce(new Vector3(0, f_jump, 0), ForceMode.Impulse);
                    speed_drop = false;
                }
                check_db_jump();
            }
        }
        if (Dash_Effect_Activate == true)
        {
            //rb.velocity = Vector3.forward * 5;
            Vec.z += Time.deltaTime * speed;
            Debug.Log("Dashhhh");
        }
        if (speed > default_speed && db_jump_count != 0 && Dash_Effect_Activate == false)
        {
            if (Input.GetKey(KeyCode.Z)) { }
            else
            {
                speed -= Time.deltaTime * 10;
            }
        }
        //rb.velocity = Vec;
        transform.localPosition = Vec;
    }

    private void Rotate(string vecter)
    {
        if (vecter == "up")
        {
            VecRotate = new Vector3(0, 0, 0);
        }
        else if (vecter == "down")
        {
            VecRotate = new Vector3(0, 180, 0);
        }
        else if (vecter == "left")
        {
            VecRotate = new Vector3(0, 270, 0);
        }
        else if (vecter == "right")
        {
            VecRotate = new Vector3(0, 90, 0);
        }
        LoseSpeed();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(VecRotate), 5 * Time.deltaTime);
    }

    private void LoseSpeed()
    {
        //bool x = this.transform.rotation.eulerAngles.y < VecRotate.y + 10 && this.transform.rotation.eulerAngles.y > VecRotate.y - 10;
        //if (!x)
        //{
        //    if (speed > default_speed)
        //    {
        //        speed = default_speed + default_speed / 8;
        //    }
        //    else
        //    {
        //        speed = default_speed - default_speed / 10;
        //    }
        //}
    }
    private void restamina()
    {
        if (stamina <= 0 && use_stamina == true)
        {
            use_stamina = false;
            speed = default_speed;
        }
        if (use_stamina == false)
        {
            if (stamina < max_stamina)
            {
                stamina += Time.deltaTime * 15;
            }
        }
        else if (use_stamina == true)
        {
            if (Dash_Effect_Activate == false)
            {
                stamina -= Time.deltaTime * 20;
            }
        }
        staminabar.value = stamina;
        ultimatebar.value = Ulti;
    }

    private void check_db_jump()
    {
        if (db_jump_count < db_jump_max - 1)
        {
            db_jump_count += 1;
        }
        else { db_jump = false; };
    }

    private IEnumerator Dash_Effect()
    {
        CmdrunAnumation("Dash");
        fail = false;
        speed = default_speed + 5;
        Dash_Effect_Activate = true;
        yield return new WaitForSeconds(1);
        Dash_Effect_Activate = false;
    }

    public void TakeDamage()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector3(0, f_jump / 2, 0), ForceMode.Impulse);
        //rb.AddForce(Vector3.back*5, ForceMode.Impulse);
        rb.AddRelativeForce(Vector3.back * 3, ForceMode.Impulse);
        if (Ulti < 100) 
        {
            Ulti += 10;
        }
    }

    public void use_Ultimage() 
    {
        if (Ulti >= 100) 
        {
            Ulti = 0;
            StartCoroutine(Ultimage_Skill());
        }
    }

    public IEnumerator Ultimage_Skill() 
    {
        Imu = true;
        Debug.Log(Imu);
        yield return new WaitForSeconds(10);
        Imu = false;
    }

    public IEnumerator fail_dash()
    {
        fail = true;
        Imu = true;
        yield return new WaitForSeconds(1.5f);
        fail = false;
        yield return new WaitForSeconds(1.5f);
        Imu = false;
    }
    [Command]
    public void CmdWhenFinish(bool x) 
    {
        StartCountTime(x);
    }

    public void StartCountTime(bool x) 
    {
        if (!isLocalPlayer)
        {
            StartCoroutine(CountTime());
        }
        else { StartCoroutine(GetText()); }
    }

    public IEnumerator GetText() 
    {
        ContTimetext.text = "Winner";
        this.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(2);
    }

    public IEnumerator CountTime() 
    {
        ContTimetext = GameObject.Find("CountTime").GetComponent<Text>();
        //anim.SetTrigger("DbJump");
        ContTimetext.text = "10";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "9";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "8";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "7";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "6";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "5";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "4";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "3";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "2";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "1";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "0";
        yield return new WaitForSeconds(1);
        ContTimetext.text = "TimeOut";
        SceneManager.LoadScene(3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            db_jump = true;
            db_jump_count = 0;
            Dash = false;
            Hit_anim = false;
            speed_drop = true;
            if (fail == true)
            {
                StartCoroutine(fail_dash());
            }
            if (!Input.GetKey(KeyCode.Z))
            {
                speed = default_speed;
            }
            CmdrunAnumation("idel");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FloorDash"))
        {
            Dash = true;
        }
        if (other.CompareTag("Bunger") && Imu == false)
        {
            CmdrunAnumation("Hit");
            fail = true;
            Hit_anim = true;
            TakeDamage();
        }
        if (other.CompareTag("Finish")) 
        {
            getFinish = true;
            FinishRun = true;
            CmdWhenFinish(getFinish);
        }
    }

    [Command]
    private void CmdrunAnumation(string name)
    {
        runAnimation(name);
    }

    public void runAnimation(string name) //hook
    {
        //if (isLocalPlayer) return;
        updateAnim(name);
    }

    public void updateAnim(string name) 
    {
        name_anim = name;
        if (name_anim == "idel") return;
        else if (name_anim == "Hit")
        {
            anim.SetTrigger("Hit");
        }
        else if (name_anim == "Dash")
        {
            anim.SetTrigger("Dash");
        }
        else if (name_anim == "DbJump")
        {
            anim.SetTrigger("DbJump");
        }
    }
}
