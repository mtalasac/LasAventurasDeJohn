using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;
    public AudioClip SoundJump;
    public AudioClip SoundHit;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator; 
    private float LastShoot;
    private int Health = 5;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        //OBTIENE EL VALOR -1 (TECLA A), 0 o 1(TECLA D) 
        Horizontal = Input.GetAxis("Horizontal");

        //MOVIMIENTO JOHN
        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); 
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator. SetBool("running", Horizontal != 0.0f);

        //SALTO Y CONTROL SALTO
        if (Physics2D. Raycast(transform. position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundJump);
        }

        //DISPARO
        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

   private void Shoot()
   {
        //CAMBIA LA DIRECCION DE LA BALA DEPENDIENDO DE DONDE MIRE JOHN
        Vector3 direction; 
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;
        
        GameObject bullet = Instantiate(BulletPrefab, transform. position + direction * 0.1f, Quaternion.identity); 
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump() 
    {
        //ANADIMOS FUERZA PARA EL SALTO. AddForce(Vector2.up) da fuerza en el eje Y 
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal*Speed, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundHit);
        Health = Health - 1;
        if (Health==0) SceneManager.LoadScene("SampleScene");
    }

}
