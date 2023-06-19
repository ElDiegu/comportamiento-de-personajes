using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInteraction : MonoBehaviour
{
    [Header("Perceptions")]
    public bool enemyDetected;
    public bool allyHurtDetected;
    public bool isHealing;
    public bool isAttacking;

    [Header("Entity Stats")]
    public int maxHP;
    public int HP;
    [SerializeField] int damage;
    public int team;

    [Header("Character View")]
    [SerializeField] FieldOfView fov;

    // Events used in display control of actions carried by the Character
    public event Action onAttacking, onHealing, onDying;

    private void Awake()
    {
        HP = maxHP;
    }
    private void Update()
    {
        enemyDetected = fov.enemy != null;
        allyHurtDetected = fov.allyHurt != null;
    }
    public void Attack(GameObject enemy)
    {
        if (enemy == null) return;
        Debug.Log(gameObject.name + ": Attack " + enemy.name);
        transform.rotation = Quaternion.LookRotation(EntityMovement.Direction2D(enemy, gameObject), transform.up);
        enemy.GetComponent<EntityInteraction>().SufferDamage(damage + gameObject.GetComponent<EntityInv>().weapon);
        if (onAttacking != null) onAttacking();
        StartCoroutine(AttackCoroutine(enemy));
    }
    IEnumerator AttackCoroutine(GameObject enemy)
    {
        if (enemy.GetComponent<EntityInteraction>().isDead()) gameObject.GetComponent<EntityInv>().totalCoins += enemy.GetComponent<EntityInv>().totalCoins;
        isAttacking = true;
        yield return new WaitForSeconds(0.21f);
        isAttacking = false;
    }
    IEnumerator GetKilled()
    {
        Debug.Log(gameObject + " is dead");
        GetComponent<Collider>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<EntityInv>().enabled = false;
        GetComponentInChildren<Animator>().enabled = false;
        if (onDying != null) onDying();
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    public void SufferDamage(int damage)
    {
        var finalDamage = Mathf.Clamp(damage - gameObject.GetComponent<EntityInv>().armor, 0, 100);
        HP = Mathf.Clamp(HP - finalDamage, 0, maxHP);
        if (HP <= 0) StartCoroutine(GetKilled());
    }
    public void Heal(GameObject ally)
    {
        Debug.Log(gameObject.name + ": Healing " + ally.name);
        if(onHealing != null) onHealing();
        StartCoroutine(HealCoroutine(ally));
    }
    IEnumerator HealCoroutine(GameObject ally)
    {
        isHealing = true;
        ally.GetComponent<EntityInteraction>().GetHealed(damage + gameObject.GetComponent<EntityInv>().weapon);
        yield return new WaitForSeconds(1.0f);
        isHealing = false;
    }
    public void GetHealed(int heal)
    {
        HP = Mathf.Clamp(HP + heal, 0, maxHP);
    }
    public bool isDead()
    {
        return this.HP <= 0;
    }
}
