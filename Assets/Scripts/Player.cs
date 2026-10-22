using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting; // 1. IMPORTANTE: Añade esta línea arriba

public class Player : MonoBehaviour
{
    public static Player obj;

    public int lives = 3;

    public bool isGrounded = false;
    public bool isMoving = false;
    public bool isInmune = false;

    public float speed = 5f;
    public float jumpforce = 7f;
    public float movHor;

    public float inmuneTimeCnt = 0f;
    public float inmuneTime = 0.5f;

    public LayerMask groundLayer;
    public float radius = 0.3f;
    public float groundRayDist = 0.55f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;

    void Awake()
    {
        obj = this;

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Game.obj.gamePaused)
        {
            movHor = 0f;
            return;
        }
        // 2. Así se lee el teclado/mando en el nuevo sistema de forma directa
        if (Keyboard.current != null)
        {
            float izquierda = Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed ? 1f : 0f;
            float derecha = Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed ? 1f : 0f;

            // Calculamos el eje neto (-1, 0 o 1)
            movHor = derecha - izquierda;

            isMoving = movHor != 0f;
            
        }

        isGrounded = Physics2D.CircleCast(transform.position, radius, Vector3.down, groundRayDist, groundLayer);

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            jump(); // Llama a tu función de salto

        if(isInmune)
        {
            spr.enabled = !spr.enabled;

            inmuneTimeCnt -= Time.deltaTime;

            if (inmuneTimeCnt <= 0)
            {
                isInmune = false;
                spr.enabled = true;
            }
        }

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);

        flip(movHor);
        
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movHor * speed, rb.linearVelocity.y);
      
    }

    private void goInmune()
    {
        isInmune = true;
        inmuneTimeCnt = inmuneTime;
    }
    public void jump()
    {
        if (!isGrounded) return;

        rb.linearVelocity = Vector2.up * jumpforce;

        AudioManager.obj.playJump();
   
    }

    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;
        else
            if (_xValue > 0)
                theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;
    }

    public void getDamage()
    {
        lives--;
        
        AudioManager.obj.playHit();

        UIManager.obj.updateLives();

        goInmune();

        if (lives <= 0)
        {
            FXManager.obj.showPop(transform.position);
            Game.obj.gameOver();
        }
    }

    public void addLive()
    {
        lives++;

        if (lives > Game.obj.maxLives)
            lives = Game.obj.maxLives;
    }
    void OnDestroy()
    {
        obj = null;
    }
}
