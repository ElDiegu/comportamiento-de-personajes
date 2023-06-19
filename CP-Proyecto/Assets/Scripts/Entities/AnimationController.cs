using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator Character;
    public EntityMovement entityMovement;
    public EntityInv entityInv;
    public EntityInteraction entityInteraction;

    public string isMoving;
    public string isPickingObject;
    public string isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Character.SetBool("isMoving", (entityMovement.isMoving || entityMovement.isFollowing || entityMovement.isFleeing) && !entityMovement.isResting);
        Character.SetBool("isPickingObject", entityInv.isPickingObject);
        Character.SetBool("isAttacking", entityInteraction.isAttacking);
        Character.SetBool("isHealing", entityInteraction.isHealing);
    }
}
