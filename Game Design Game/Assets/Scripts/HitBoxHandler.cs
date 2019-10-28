using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitBoxHandler : MonoBehaviour
{
    private HealthSystem health = new HealthSystem(3);
    public Text textbox;


    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //textbox.text = "Health: " + health.getHealth();


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


    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") || other.gameObject.CompareTag("HealthPickup"))
        {
            HitBox hitbox = other.gameObject.GetComponent<HitBox>();
            this.health.changeHealth(hitbox.value);
            Debug.Log("health "+health.getHealth());
        }
    }
}
