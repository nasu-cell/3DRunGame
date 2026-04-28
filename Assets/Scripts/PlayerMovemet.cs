
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float acceleration;
    public float leftRightSpeed = 4f;
    public float limit = 5f;
    public Animator animator;
 
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;

    public bool isGamePlaying;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }
 
    void OnCollisionStay()
    {
        isGrounded = true;
    }
 
    void OnCollisionExit()
    {
        isGrounded = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if (isGamePlaying)
        {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
        moveSpeed += Time.deltaTime * acceleration;
 
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (transform.position.x > -limit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
            }
        }
 
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (transform.position.x < limit)
            {
                transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed * -1);
            }
        }
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("a_Jump");
        }
   
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("a_Jump");
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        }
        else
        {
            StartCoroutine(nameof(EndGame));
        }
 
    }
    IEnumerator EndGame()
    {
        animator.SetBool("a_Idle", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
 
}