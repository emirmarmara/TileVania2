using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        Debug.Log(player);
        xSpeed = player.transform.localScale.x * bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2 (xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Polygon collider ı is trigger yptık ondan sanırım çarpışıyolar bu bizi engelliyo
        if (other.tag != "Background"){
        //Enemy tag ı verdik goober a dokunursa mermi goober nanay
        if (other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        //Burda da diyoruz ki herhangi bir temas da kendisini de yok et merminin
        Debug.Log(other);
        Destroy(gameObject);
        }
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
