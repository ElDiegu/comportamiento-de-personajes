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
    [SerializeField] FieldOfView fow;

    private void Awake()
    {
        HP = maxHP;
    }
    private void Update()
    {
        enemyDetected = fow.enemy != null;
        allyHurtDetected = fow.allyHurt != null;
    }
    public void Attack(GameObject enemy)
    {
        Debug.Log(gameObject.name + ": Attack " + enemy.name);
        enemy.GetComponent<EntityInteraction>().SufferDamage(damage + gameObject.GetComponent<EntityInv>().weapon);
        StartCoroutine(AttackCoroutine(enemy));
    }
    IEnumerator AttackCoroutine(GameObject enemy)
    {
        if (enemy.GetComponent<EntityInteraction>().isDead()) gameObject.GetComponent<EntityInv>().totalCoins += enemy.GetComponent<EntityInv>().totalCoins;
        isAttacking = true;
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    IEnumerator GetKilled()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<EntityInv>().enabled = false;
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
