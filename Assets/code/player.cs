using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class player : MonoBehaviour  //public class = 檔案名
{
    private Rigidbody2D rb;//鋼體(me)
    private Animator an;//動畫(me)
    public Collider2D co;//碰撞體(腳)
    public Collider2D cco;//碰撞體(頭)
    public LayerMask ground;//地板(tilemap)
    public LayerMask spike;
    public float speed; //跑步速度(me)
    public float jumpforce;//跳躍力(me)
    
    public float time;

    public GameObject myBag;    //背包變量
<<<<<<< HEAD
    bool isOpen = false;
=======
    private Renderer render;
    bool isOpen;
    private bool ishurt;//默認為false
    public int Health;
    public int damage;
>>>>>>> b57f70eabe29897621dc41a53a5d7ba8636c7c14
  

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        an=GetComponent<Animator>();
<<<<<<< HEAD

        myBag.SetActive(isOpen);
=======
        render=GetComponent<Renderer>();
        Health_bar.healthmax=Health;
        Health_bar.healthnow=Health;
        
        
>>>>>>> b57f70eabe29897621dc41a53a5d7ba8636c7c14
    }

    // Update is called once per frame
    void Update()
    {
        jump();
        OpenMyBag();
    }
    void FixedUpdate() {
        swichan();
        Movement();
    }
    void Movement()//移動製作
    {
        float move = Input.GetAxis("Horizontal");//左右移動速率
        float directionX = Input.GetAxisRaw("Horizontal");//左右移動的面向

        
        rb.velocity = new Vector2(move * speed * Time.fixedDeltaTime , rb.velocity.y); //velocity=速率   rb.velocity=鋼體的速率
        
        an.SetFloat("running", Mathf.Abs(directionX));
        
        if (directionX > 0)//角色面向
        {
            if(transform.localScale.x>0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            
        }
        else if(directionX < 0)
        {
            if(transform.localScale.x>0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
            }
        }
    }
    void jump()
    {
        if (Input.GetButton("Jump"))//角色跳躍
        {
            if (co.IsTouchingLayers(ground)  ||  co.IsTouchingLayers(spike))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
                
                an.SetBool("jumping", true);
            }
            
        }
    }
    void swichan()//動畫製作
    {
    //an.SetBool("idle", false);    //先將站立定義為否
    if( rb.velocity.y <0.1f && !co.IsTouchingLayers(ground))
    {
            an.SetBool("downing",true);//自由落體
        }
        if (an.GetBool("jumping"))    //是否在跳躍
        {
            if (rb.velocity.y < 0)    //是否上升速率小於0
            {
                an.SetBool("jumping", false);//將跳躍定義為否
                an.SetBool("downing", true);//將落下定義為是
            }
        }
       
        else if (co.IsTouchingLayers(ground)||co.IsTouchingLayers(spike))//是否碰觸到指定物ground
        {
            an.SetBool("downing", false);//將落下定義為否
            //    an.SetBool("idle", true);//將站立定義為是
        }
    }

    private void OnCollisionEnter2D(Collision2D other) //消滅敵人
    {
        if(other.gameObject.tag=="enemy")
        {
            // enemy=other.gameObject.GetComponent<enemy>();
            if(an.GetBool("downing"))
            {
                //.kill_enemy();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                an.SetBool("jumping", true);
            }
            
            else if(transform.position.x < other.gameObject.transform.position.x)
            {
                rb.velocity=new Vector2(-12,rb.velocity.y);
                ishurt=true;
                Health-=damage;
                if (Health<0)
                {
                    Health=0;
                }
                Health_bar.healthnow=Health;
                //auh.Play();
            }
            else if(transform.position.x > other.gameObject.transform.position.x)
            {
                rb.velocity=new Vector2(12,rb.velocity.y);
                ishurt=true;
                Health-=damage;
                if (Health<0)
                {
                    Health=0;
                }
                Health_bar.healthnow=Health;
                //auh.Play();
            }
            
        }
    }

    void OpenMyBag()    //背包開關
    {
        if (Input.GetKeyDown(KeyCode.O))    //按下O鍵
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
        }
    }
    ///以下for測試
    float damagement =233f;
    public float GetDamage()
    {
        return damagement;
    }
}
