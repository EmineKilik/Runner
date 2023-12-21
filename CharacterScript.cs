using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float shift;
    [SerializeField] float jumpForce;
    [SerializeField] Animator anim;
    bool isGameOver;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text score;
    float roundScore;
    bool isShield;
    [SerializeField] AudioClip itemSFX, shieldSFX, obstacleSFX, destroySFX;
    [SerializeField] AudioSource sound, music;
    [SerializeField] GameObject itemVFX, shieldVFX, obstacleVFX;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            roundScore += Time.deltaTime;
            score.text = "Score: " + roundScore.ToString("f1");

            if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 2 || Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 2)
            {
                transform.Translate(shift, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -2 || Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -2)
            {
                transform.Translate(-shift, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            if (rb.velocity.y > 0)
            {
                anim.SetBool("Jump", true);
            }

            if (rb.velocity.y == 0)
            {
                anim.SetBool("Jump", false);
            }
        }      
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        }       
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (isShield)
            {
                Destroy(other.gameObject);
                sound.clip = destroySFX;
                sound.Play();
            }
            else
            {
                isGameOver = true;
                menu.SetActive(true);
                anim.SetBool("Death", true);

                GameObject vfx = Instantiate(obstacleVFX, transform.position, transform.rotation);
                Destroy(vfx, 3f);

                sound.clip = obstacleSFX;
                sound.Play();
                music.Stop();
            }
       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Money"))
        {
            Destroy(other.gameObject);
            roundScore += 5;

            GameObject vfx = Instantiate(itemVFX, other.transform.position, other.transform.rotation);
            Destroy(vfx, 2f);

            sound.clip = itemSFX;
            sound.Play();
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            isShield = true;
            Destroy(other.gameObject);
            Invoke("DeactiveShield", 5);

            GameObject vfx = Instantiate(shieldVFX, transform.position + transform.up, transform.rotation);
            vfx.transform.SetParent(transform);
            Destroy(vfx, 5f);

            sound.clip = shieldSFX;
            sound.Play();
        }

    }

    void DeactiveShield()
    {
        isShield = false;
    }

}
