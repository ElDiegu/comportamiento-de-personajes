using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
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
        if (transform.position != destination && !isResting && !isFleeing) isMoving = true;
        else if (OnLocation(destination)) isMoving = false;

        if (isFollowing)
        {
            transform.rotation = Quaternion.LookRotation(Direction2D(followingObject, gameObject), transform.up);
            destination = followingObject.transform.position;
        }

        if(isFollowing && EntityInv.inRange(gameObject, followingObject))
        {
            isFollowing = false;
            destination = transform.position;
        }

        if (isFleeing)
        {
            transform.rotation = Quaternion.LookRotation(Direction2D(gameObject, fleeingEnemy), transform.up);
            agent.Move((transform.position - fleeingEnemy.transform.position).normalized * agent.speed * Time.deltaTime);
        }

        if (isMoving) agent.destination = destination;
    }

    /* Movement */
    public void MoveTo(Vector3 destination)
    {
        this.destination = destination;
    }
    public void MoveRandom()
    {
        Debug.Log(gameObject.name + ": Move Random");
        destination = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
    }
    public void Follow(GameObject target)
    {
        Debug.Log(gameObject.name + ": Follow " + target.name);
        isFollowing = true;
        isFleeing = false;
        followingObject = target;
        //StartCoroutine(FollowCoroutine(target));
    }
    IEnumerator FollowCoroutine(GameObject target)
    {
        while (!EntityInv.inRange(gameObject, target)) yield return new WaitForSeconds(0.5f);
        isFollowing = false;
        //agent.ResetPath();
        agent.destination = transform.position;
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

    /* Fleeing */
    public void Flee(GameObject obj)
    {
        Debug.Log(gameObject.name + ": Flee from " + obj.name);
        gameObject.GetComponent<EntityInv>().StopAllCoroutines();
        isFleeing = true;
        isMoving = false;
        isFollowing = false;
        agent.ResetPath();
        fleeingEnemy = obj;
        StartCoroutine(FleeCoroutine(stamina));
    }
    IEnumerator FleeCoroutine(int stamina)
    {
        yield return new WaitForSeconds(stamina);
        isFleeing = false;
    }

    /* Directions */
    public static Vector3 Direction2D(GameObject A, GameObject B)
    {
        return (new Vector3(A.transform.position.x, 0, A.transform.position.z) - new Vector3(B.transform.position.x, 0, B.transform.position.z));
    }
}
