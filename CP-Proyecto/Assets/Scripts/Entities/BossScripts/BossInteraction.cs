using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteraction : MonoBehaviour
{
    public bool isAttacking;

    [Header("Entity Stats")]

    [Header("Character View")]
    [SerializeField] BossFieldOfView fow;


    public void Attack(GameObject enemy)
    {
        if (enemy == null) return;
        enemy.GetComponent<EntityInteraction>().SufferDamage(enemy.GetComponent<EntityInteraction>().maxHP);
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

}
