using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptForPlayer : MonoBehaviour
{
    
   [Header("Stats")]
    public float max_hp = 100f;
    [SerializeField] private float attackSpeed = 0.5f;
    [SerializeField] private float coolDown;
    [SerializeField] private float projectileSpeed = 500;
    [SerializeField] private GameObject bul;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform up_fire;
    [SerializeField] private Transform down_fire;
    [SerializeField] private Transform left_fire;
    [SerializeField] private Transform right_fire;

    [Header("Status")]
    public bool invulnerable = false;

    [Header("Movement")]
    public float move_accel = 1f;
    public float move_deccel = 1f;
    public float move_max = 5f;

    // public UnityAction onDeath;
    // public UnityAction onHit;

    private Rigidbody2D rigid;
    [SerializeField]private Animator animator;

    [Header("power loosing")] [SerializeField]
    private float timerToShoot;

    [SerializeField] private float timerToBomb = 1;
    private float timerBomb;
    private float timerFire;
    
    [SerializeField] private float timerToArrow = 1;
    private float timerArrow;
    
    
    
    private float hp;
    private bool is_dead = false;
    private Vector2 move;
    private Vector2 move_input;
    private Vector2 lookat = Vector2.zero;
    private float side = 1f;
    private bool disable_controls = false;
    private float hit_timer = 0f;
    private PlayerControler m_Controler;
    private Vector3 initpos;
    private float timeToWaitForFire;
    private Vector2 Direction;
    [SerializeField] private float WalkSpeed = 4;
    [SerializeField] private KinematicTopDownController Controller_k;
    [Header("sword")] [SerializeField] private GameObject swordLeft;
    [SerializeField] private GameObject swordRight;
    [SerializeField] private GameObject swordUp;
    [SerializeField] private GameObject swordDown;
    
    [SerializeField] private float timerToSword;
    private float timerSword;

    private bool bombActive =true;
    private bool shootActive =true;
    private bool arrowActive = true;
    public Slide slide;
    [SerializeField] private AudioSource blast;
    [SerializeField] private AudioSource blade;
    [SerializeField] private AudioSource hurt;
    // [SerializeField] private AudioSource shootArrow;
    


    // Start is called before the first frame update
    void Start()
    {
        initpos = gameObject.transform.position;
    }
    
    void Awake()
    {
        m_Controler = GetComponent<PlayerControler>();
        rigid = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();
        hp = max_hp;
        slide.slider.value = (hp) / max_hp;
        
    }

    // Update is called once per frame
    void Update()
    {
        hit_timer += Time.deltaTime;
        move_input = Vector2.zero;
        timerFire += Time.deltaTime;
        timerBomb += Time.deltaTime;
        timerArrow += Time.deltaTime;
        timerSword += Time.deltaTime;
        ;

        //Controls
        if (!disable_controls)
        {
            //Controls
            // PlayerControls controls = PlayerControls.Get(player_id);
            move_input = m_Controler.GetMove();
                
        }
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        
        //Update lookat side
        if (move.magnitude > 0.1f)
            lookat = move.normalized;
        if (Mathf.Abs(lookat.x) > 0.02)
            side = Mathf.Sign(lookat.x);
        
        // if(m_Controler.GetActionHold())
        // {
        //     Fire();
        // } 
        Vector2 movement = Vector2.zero;
        if (horizontalMovement != 0.0f)
        {
            movement.x = horizontalMovement;
        }
        else if (verticalMovement != 0.0f)
        {
            movement.y = verticalMovement;
        }

        // Update the Animator only when motion
        if (movement != Vector2.zero)
        {
            Direction = movement;
        }

        if (hp <= 0f)
        {
            Death();
        }
        
        if(m_Controler.GetActionHold() && shootActive)
        {
            Fire();
        }
        if(m_Controler.GetActionHold1())
        {
            useSword();
        }
        
        if(m_Controler.GetActionHold2() && bombActive)
        {
            useBomb();
        }
        if(m_Controler.GetActionHold3() && arrowActive)
        {
            ArrowUse();
        }
    }
    
    void FixedUpdate()
    {
        //Movement velocity
        // float desiredSpeedX = Mathf.Abs(move_input.x) > 0.1f ? move_input.x * move_max : 0f;
        // float accelerationX = Mathf.Abs(move_input.x) > 0.1f ? move_accel : move_deccel;
        // move.x = Mathf.MoveTowards(move.x, desiredSpeedX, accelerationX * Time.fixedDeltaTime);
        // float desiredSpeedY = Mathf.Abs(move_input.y) > 0.1f ? move_input.y * move_max : 0f;
        // float accelerationY = Mathf.Abs(move_input.y) > 0.1f ? move_accel : move_deccel;
        // move.y = Mathf.MoveTowards(move.y, desiredSpeedY, accelerationY * Time.fixedDeltaTime);
        //
        // //Move
        // rigid.velocity = move;
        Controller_k.MovePosition(Direction, WalkSpeed);
        
            
    }
    
    void Fire()
    {
        if (timerFire < timerToShoot)
        {
            return;
        }

        timerFire = 0;
        // GameObject star = Instantiate(bul, new Vector3(transform.position.x, transform.position.y, transform.position.z),transform.rotation) as GameObject;
        Vector3 direction = Vector2.zero;
        Transform pos;
        switch (m_Controler.GetDirect())
        {
            case 1:
            {
                direction = transform.right;
                pos = right_fire;
                break;
            }
            case -2:
            {
                direction = -1*transform.up;
                pos = down_fire;
                break;
            }
            case -1:
            {
                direction = -1*transform.right;
                pos = left_fire;
                break;
            }
            case 2:
            {
                direction =transform.up;
                pos = up_fire;
                break;
            }
            default:
                direction = transform.position;
                pos = transform;
                return;
                break;
            
        }
        // GameObject star = Instantiate(bul, new Vector3(transform.position.x+direction.x, transform.position.y+direction.y, transform.position.z),transform.rotation) as GameObject;
        GameObject star = Instantiate(bul, pos.position,pos.rotation) as GameObject;
        star.GetComponent<Rigidbody2D>().AddForce(direction * projectileSpeed);
        blast.Play();
           
        coolDown = Time.time + attackSpeed;
    }

    public void hit(float ht)
    {
        if(hit_timer > 0f)
        {
            hit_timer = -1f;
            hp -= ht;
            rigid.position = initpos;
            animator.SetTrigger("hit");
            hurt.Play();
        }
        slide.slider.value = (hp) / max_hp;
    }

    private void useSword()
    {
        if (timerSword < timerToSword)
        {
            return;
        }

        timerSword = 0;
        Vector2 direction = Vector2.zero;
        Vector3 pos;
        switch (m_Controler.GetDirect())
        {
            case 1:
            {
                direction = transform.right;
                pos = right_fire.position;
                break;
            }
            case -2:
            {
                direction = (-1*transform.up);
                pos = down_fire.position;
                break;
            }
            case -1:
            {
                direction = (-1*transform.right);
                pos = left_fire.position;
                break;
            }
            case 2:
            {
                direction =transform.up;
                pos = up_fire.position;
                break;
            }
            default:
                direction = transform.position;
                pos = transform.position;
                return;
                break;
            
        
        }
        GameObject star = Instantiate(swordDown, new Vector3(transform.position.x+direction.x, transform.position.y+direction.y, transform.position.z),transform.rotation) as GameObject;
        // GameObject star = Instantiate(swordDown, transform.position,transform.rotation) as GameObject;
        swordDown.SetActive(true);
        blade.Play();
        // switch (m_Controler.GetDirect())
        // {
        //     case 1:
        //     {
        //         direction = transform.right;
        //         swordRight.SetActive(true);
        //         
        //         break;
        //     }
        //     case -2:
        //     {
        //         direction = -1 * transform.up;
        //         swordDown.SetActive(true);
        //         break;
        //     }
        //     case -1:
        //     {
        //         direction = -1 * transform.right;
        //         swordLeft.SetActive(true);
        //         break;
        //     }
        //     case 2:
        //     {
        //         direction = transform.up;
        //         swordUp.SetActive(true);
        //         break;
        //     }
        //     default:
        //         direction = transform.position;
        //         return;
        //         break;
        // }
        animator.SetTrigger("sword");
        
        
        

    }
    
    private void useBomb()
    {
        if (timerBomb < timerToBomb)
        {
            return;
        }

        timerBomb = 0;
        Vector2 direction = Vector2.zero;
        Vector3 pos;
        switch (m_Controler.GetDirect())
        {
            case 1:
            {
                direction = transform.right;
                pos = right_fire.position;
                break;
            }
            case -2:
            {
                direction = (-1*transform.up);
                pos = down_fire.position;
                break;
            }
            case -1:
            {
                direction = (-1*transform.right);
                pos = left_fire.position;
                break;
            }
            case 2:
            {
                direction =transform.up;
                pos = up_fire.position;
                break;
            }
            default:
                direction = transform.position;
                pos = transform.position;
                return;
                break;
            
        
        }
        GameObject star = Instantiate(bomb, new Vector3(transform.position.x+direction.x, transform.position.y+direction.y, transform.position.z),transform.rotation) as GameObject;

    }
    
    private  void Death()
    {
        animator.SetBool("dead", true);
        SceneManager.LoadScene("theEnd");
        // gameObject.SetActive(false);
    }

    void ArrowUse()
    {
        if (timerArrow < timerToArrow)
        {
            return;
        }

        timerArrow = 0;
        // GameObject star = Instantiate(arrow, new Vector3(transform.position.x, transform.position.y, transform.position.z),transform.rotation) as GameObject;
        Vector2 direction = transform.forward;
        Transform pos;
        float deg;
        switch (m_Controler.GetDirect())
        {
            case 1:
            {
                // star.GetComponent<Rigidbody2D>().SetRotation(180f);
                deg = 180f;
                // star.GetComponent<Rigidbody2D>().SetRotation(90f);
                direction = transform.right;
                pos = right_fire;
                break;
            }
            case -2:
            {
                // star.GetComponent<Rigidbody2D>().SetRotation(90f);
                deg = 90f;
                direction = -1 * transform.up;
                pos = down_fire;
                break;
            }
            case -1:
            {
                direction = -1 * transform.right;
                pos = left_fire;
                break;
            }
            case 2:
            {
                // star.GetComponent<Rigidbody2D>().SetRotation(-90f);
                deg = -90f;
                direction = transform.up;
                pos = up_fire;
                break;
            }
            default:
                direction = transform.position;
                return;
                break;

        }

        GameObject star = Instantiate(arrow, pos.position,
        pos.rotation) as GameObject;

        star.GetComponent<Rigidbody2D>().AddForce( direction * projectileSpeed);

        coolDown = Time.time + attackSpeed;
    }
    
    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
        move = Vector2.zero;
    }


    public void setBombFalse()
    {
        bombActive = false;
    }
    
    public void setGunFalse()
    {
        shootActive = false;
    }
    
    public void setArrowFalse()
    {
        arrowActive = false;
    }

    public bool  getMove()
    {
        return Direction != Vector2.zero;
    }

}
