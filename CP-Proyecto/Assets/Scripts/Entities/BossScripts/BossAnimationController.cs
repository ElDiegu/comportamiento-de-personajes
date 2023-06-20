using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    public Animator Character;
    public BossMovement bossMovement;
    public BossInv BossInv;
    public BossInteraction bossInteraction;

    void Update()
    {
        Character.SetBool("isMoving", (bossMovement.isMoving || bossMovement.isFollowing || bossMovement.isFleeing) && !bossMovement.isResting);
        Character.SetBool("isPickingObject", BossInv.isPickingObject);
        Character.SetBool("isAttacking", bossInteraction.isAttacking);
        Character.SetBool("isHealing", false);
    }
}
