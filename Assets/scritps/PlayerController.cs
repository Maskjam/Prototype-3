using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            dirtParticle.Stop();
             playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
             isOnGround = false;
             playerAnim.SetTrigger("Jump_trig");
        }
    }
    private void OnCollisionEnter( Collision Collision)
    {
       if (Collision.gameObject.CompareTag("Ground"))
       {
        isOnGround = true;
        dirtParticle.Play();
       }
       else if (Collision.gameObject.CompareTag("Obstacle"))
       {
        dirtParticle.Stop();
        explosionParticle.Play();
        gameOver = true;
        Debug.Log("Game Over!");
        playerAnim.SetBool("Death_b", true);
        playerAnim.SetInteger("DeathType_int" , 1);
       }
    }
}
