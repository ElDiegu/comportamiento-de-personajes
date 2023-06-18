using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInteraction : MonoBehaviour
{
    public bool isAttacking;

    [Header("Entity Stats")]

    [Header("Character View")]
    [SerializeField] BossFieldOfView fow;


    public bool Attack(GameObject enemy)
    {
        StartCoroutine(AttackCoroutine());
        return enemy.GetComponent<EntityInteraction>().SufferDamage(enemy.GetComponent<EntityInteraction>().maxHP);
    }
    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

}
