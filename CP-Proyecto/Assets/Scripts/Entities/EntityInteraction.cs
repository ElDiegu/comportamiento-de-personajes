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
    public bool SufferDamage(int damage)
    {
        var finalDamage = Mathf.Clamp(damage - gameObject.GetComponent<EntityInv>().armor, 0, 100);
        HP = Mathf.Clamp(HP - finalDamage, 0, maxHP);
        if (HP <= 0) StartCoroutine(GetKilled());
        return HP <= 0;
    }
    public bool GetHealed(int heal)
    {
        HP = Mathf.Clamp(HP + heal, 0, maxHP);
        return HP > maxHP / 2;
    }
    public bool Attack(GameObject enemy)
    {
        StartCoroutine(AttackCoroutine());
        return enemy.GetComponent<EntityInteraction>().SufferDamage(damage + gameObject.GetComponent<EntityInv>().weapon);
    }
    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    public bool Heal(GameObject ally)
    {
        return ally.GetComponent<EntityInteraction>().GetHealed(damage + gameObject.GetComponent<EntityInv>().weapon);
    }
    IEnumerator HealCoroutine()
    {
        isHealing = true;
        yield return new WaitForSeconds(1.0f);
        isHealing = false;
    }
    IEnumerator GetKilled()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
