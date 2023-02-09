using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    [Header("Components")]
    public BaseUnitSO unitSO;

    private Rigidbody2D rigBody;
    private SpriteRenderer spritRend;

    [Header("Variables")]
    public int currentHealth;
    public int currentDamage;
    public bool isDead = false;

    //public int ID;
    void Start()
    {
        if (unitSO == null)
            Debug.LogWarning("Missing scriptable Object on " + gameObject.name);

        rigBody = GetComponent<Rigidbody2D>();
        spritRend = GetComponent<SpriteRenderer>();

        rigBody.simulated = false;

        spritRend.sprite = unitSO.sprite;
        gameObject.name = unitSO.name;

        currentDamage = unitSO.damage;
        currentHealth = unitSO.health;
    }

    public void TakeDamage(int damageAmmount, UnitTypes damageType)
    {
        // damage trigger here

        if (damageType != unitSO.Weakness)
            currentHealth -= damageAmmount;
        else
            currentHealth -= damageAmmount * 2;


        if (currentHealth <= 0)
        {
            isDead = true;
            //do death trigger here
            rigBody.simulated = true;

            float xForce = Random.Range(0, 2) * 2 - 1;
            float speedForce = Random.Range(6, 15);

            rigBody.AddForce(new Vector2(xForce, Random.Range(1f, 0)).normalized * speedForce, ForceMode2D.Impulse);

            rigBody.AddTorque(-xForce * speedForce, ForceMode2D.Impulse);

            Invoke(nameof(HideObject), 3);
        }
    }


    public void HideObject()
    {
        rigBody.simulated = false;
    }

}
