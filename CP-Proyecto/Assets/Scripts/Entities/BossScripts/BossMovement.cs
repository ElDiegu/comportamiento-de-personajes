using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    [Header("AI Navigation")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Vector3 destination;
    public GameObject followingObject;
    public GameObject fleeingEnemy;

    [Header("Variables")]
    [SerializeField] float maxDistance;
    [SerializeField] int stamina;

    [Header("Perceptions")]
    public bool isMoving;
    public bool isResting;
    public bool isFleeing;
    public bool isFollowing;

    private void Awake()
    {
        destination = transform.position;
    }
    private void Update()
    {
        if (transform.position != destination) isMoving = true;
        else if (OnLocation(destination)) isMoving = false;

        if (isFollowing) destination = followingObject.transform.position;

        if (isMoving) agent.destination = destination;
    }

    /* Movement */
    public void MoveTo(Vector3 destination)
    {
        this.destination = destination;
    }

    public void MoveRandom()
    {
        Debug.Log("Move Random");
        destination = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
    }

    public void Follow(GameObject target)
    {
        Debug.Log(gameObject.name + ": Follow " + target.name);
        isFollowing = true;
        isFleeing = false;
        followingObject = target;
        StartCoroutine(FollowCoroutine(target));
    }

    IEnumerator FollowCoroutine(GameObject target)
    {
        while (!EntityInv.inRange(gameObject, target)) yield return new WaitForSeconds(0.2f);
        isFollowing = false;
        agent.ResetPath();
    }

    /* Rest */
    public void Rest(float seconds)
    {
        StartCoroutine(RestCorroutine(seconds));
    }

    IEnumerator RestCorroutine(float seconds)
    {
        isResting = true;
        isMoving = false;
        yield return new WaitForSeconds(seconds);
        isResting = false;
    }

    public bool OnLocation(Vector3 destination)
    {
        return transform.position.x == destination.x && transform.position.z == destination.z;
    }

}
