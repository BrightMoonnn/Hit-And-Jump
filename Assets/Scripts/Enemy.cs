using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movHor = 1f;
    public float speed = 3f;

    public bool isGroundFloor = true;
    public bool isGroundFront = false;

    public LayerMask groundLayer;
    public float frontGrndRayDist = 0.25f;
    public float floorCheckY = 0.52f;
    public float frontCheck = 0.51f;
    public float frontDist = 0.001f;

    public int scoreGive = 50;

    private RaycastHit2D hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Game.obj.gamePaused)
        {
            return;
        }
       
            //Evitar caer en precipicio
            isGroundFloor = (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - floorCheckY, transform.position.z),
                new Vector3(movHor, 0, 0), frontGrndRayDist, groundLayer));

            if (isGroundFloor)
                movHor = movHor * -1;

            //Choque con pared
            if (Physics2D.Raycast(transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer))
                movHor = movHor * -1;

            //Choque con otro enemigo
            hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck, transform.position.y, transform.position.z),
                new Vector3(movHor, 0, 0), frontDist);

            if (hit != null)
            
                if (hit.transform != null)
                    if (hit.transform.CompareTag("Enemy"))
                        movHor = movHor * -1;            
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movHor * speed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            //Dañar personaje
            Player.obj.getDamage();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            FXManager.obj.showPop(transform.position);
            //Destruir enemigo
            AudioManager.obj.playEnemyHit();
            FXManager.obj.showPop(transform.position);
            getKilled();
        }
    }
    private void getKilled()
    {
        gameObject.SetActive(false);
    }
}
