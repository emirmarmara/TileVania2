using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidbobdy;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    void Start()
    {
     myRigidbobdy = GetComponent<Rigidbody2D>();   
     myAnimator = GetComponent<Animator>(); 
     myBodyCollider = GetComponent<CapsuleCollider2D>(); 
     myFeetCollider = GetComponent<BoxCollider2D>();
     gravityScaleAtStart = myRigidbobdy.gravityScale; 
    }

    void Update()
    {
        if (!isAlive) { return; }
       Run();
       FlipSprite(); 
       ClimbLadder();
       Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value) 
    {
        if (!isAlive) { return; }
        //Eğer paltforma atadığımız ground layer'ına dokunmuyorsa adamımız o zaman boş return et yani zıplama
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed){
            Debug.Log("zıppıycam");
            myRigidbobdy.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run() 
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbobdy.velocity.y);
        myRigidbobdy.velocity = playerVelocity;

        // eğer x ekseninde ivme sıfırdan büyükse true döndürcek değilse false
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbobdy.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        //velocity == ivme
        // Math.Abs == negatif yada pozitif değerlerin hepsini pozitif şekilde gösterir
        // Mathf.Epsilon == 0'a çok yakın bir değer
        // Mathf.Sign == 0'a eşit veya büyükse değer 1 return eder küçükse -1 return eder.

        //Yani eğer oyuncu hareket ediyorsa scale ini hareket ettiği yöne değiştiriyoruz 
        //yani oyuncu yüzünü hareket ettiği yöne dönüyo.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbobdy.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed) {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbobdy.velocity.x),1f);
        }

    }

    void ClimbLadder()
    {
        //Eğer paltforma atadığımız ground layer'ına dokunmuyorsa adamımız o zaman boş return et yani zıplama
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbobdy.gravityScale = gravityScaleAtStart;
             myAnimator.SetBool("isClimbing" ,false); 
            return;
        }
        Vector2 climbVelocity = new Vector2 (myRigidbobdy.velocity.x, moveInput.y * climbSpeed);
        myRigidbobdy.velocity = climbVelocity;
        myRigidbobdy.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbobdy.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing" ,playerHasVerticalSpeed); 
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbobdy.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}
