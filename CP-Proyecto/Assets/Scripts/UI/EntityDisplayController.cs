using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityDisplayController : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI state;

    [Header("Character Scripts")]
    [SerializeField] EntityMovement entityMovement;
    [SerializeField] EntityInv entityInv;
    [SerializeField] EntityInteraction entityInteraction;
    [SerializeField] FieldOfView fov;

    private void Awake()
    {
        entityMovement.onMoving += SetMovingState;
        entityMovement.onResting += SetRestingState;
        entityMovement.onFleeing += SetFleeingState; ;
        entityMovement.onFollowing += SetFollowingState;
        entityInv.onPicking += SetPickingState; ;
        entityInteraction.onAttacking += SetAttackingState;
        entityInteraction.onHealing += SetHealingState;
    }
    private void Start()
    {
        healthBar.maxValue = entityInteraction.maxHP;
    }

    private void Update()
    {
        healthBar.value = entityInteraction.HP;
    }

    private void SetMovingState()
    {
        state.text = $"State: moving";
    }
    private void SetRestingState()
    {
        state.text = $"State: resting";
    }
    private void SetFollowingState()
    {
        state.text = $"State: following {entityMovement.followingObject.name}";
    }
    private void SetFleeingState()
    {
        state.text = $"State: fleeing {entityMovement.fleeingEnemy.name}";
    }
    private void SetAttackingState()
    {
        state.text = $"State: attacking {fov.enemy.name}";
    }
    private void SetHealingState()
    {
        state.text = $"State: healing {fov.allyHurt.name}";
    }
    private void SetPickingState()
    {
        state.text = $"State: picking {entityMovement.followingObject.name}";
    }
}
