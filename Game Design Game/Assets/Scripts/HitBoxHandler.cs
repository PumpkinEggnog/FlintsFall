using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HitBoxHandler : MonoBehaviour
{
    private HealthSystem health = new HealthSystem(6);
    public Text textbox;

    private bool isInvincible;
    private float iFrameLength;
    private float iFrameStart;


    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    // Start is called before the first frame update
    void Start()
    {
        setInvincible(false);   
    }

    // Update is called once per frame
    void Update()
    {
        

        //textbox.text = "Health: " + health.getHealth();
        if(isInvincible && ((Time.time - iFrameStart) >= iFrameLength))
        {
            setInvincible(false);
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health.getHealth())
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if(health.getHealth()<=0)
        {
            SceneManager.LoadScene("DiedScene");
        }


    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") && !isInvincible)
        {
            HitBox hitbox = other.gameObject.GetComponent<HitBox>();
            this.health.changeHealth(hitbox.value);
            Debug.Log("health "+health.getHealth());
            setInvincible(true);
        }
        else if (other.gameObject.CompareTag("EnemyProjectile") && !isInvincible)
        {
            this.health.changeHealth(-1);
            Debug.Log("health "+health.getHealth());
            setInvincible(true);
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("HealthPickup"))
        {
            HitBox hitbox = other.gameObject.GetComponent<HitBox>();
            this.health.changeHealth(hitbox.value);
            Debug.Log("health "+health.getHealth());
        }
        else if (other.gameObject.CompareTag("InstantKill"))
        {
            HitBox hitbox = other.gameObject.GetComponent<HitBox>();
            this.health.changeHealth(hitbox.value);
            Debug.Log("health "+health.getHealth());
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Acid") && !isInvincible)
        {
            HitBox hitbox = other.gameObject.GetComponent<HitBox>();
            this.health.changeHealth(hitbox.value);
            Debug.Log("health "+health.getHealth());
            setInvincible(true);
        }
    }

    public void setInvincible(bool input, float length = 1)
    {
        isInvincible = input;
        iFrameLength = length;
        iFrameStart = Time.time;
        Debug.Log("invincibility is set to " + isInvincible);
    }
}
