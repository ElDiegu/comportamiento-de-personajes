using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FieldOfView : MonoBehaviour
{
    [Header("Field of View")]
    public float viewRadius;
    public float awarenessRadius;
    [Range(0, 360)]
    public float viewAngle;
    [SerializeField] List<GameObject> allies;

    [Header("Masks")]
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask obstacleMask;

    [Header("Character Scripts")]
    [SerializeField] EntityInteraction entityInteraction;

    [Header("Detected objects")]
    public GameObject coin;
    public GameObject armor;
    public GameObject weapon;
    public GameObject enemy;
    public GameObject allyHurt;

    private void Awake()
    {
        //foreach (EntityInteraction entity in FindObjectsOfType<EntityInteraction>().Where((x) => x.team == GetComponent<EntityInteraction>().team)) allies.Add(entity.gameObject);
    }
    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    public void FindAlliesHurt() 
    {
        foreach (GameObject ally in allies)
            if (ally.GetComponent<EntityInteraction>().HP <= ally.GetComponent<EntityInteraction>().maxHP / 2)
            {
                allyHurt = ally;
                break;
            }
    }
    public void FindVisibleTargets()
    {
        coin = null;
        weapon = null;
        armor = null;
        enemy = null;

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        FilterColliders(targetsInViewRadius);

        Collider[] targetsInAwarenessRadius = Physics.OverlapSphere(transform.position, awarenessRadius, targetMask);
        FilterAwarenessColliders(targetsInAwarenessRadius);
    }
    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            FindVisibleTargets();
            //FindAlliesHurt();
            yield return new WaitForSeconds(delay);
        }
    }
    void FilterColliders(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            Transform target = collider.transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) continue;

                if (collider.transform.parent.gameObject.tag == "Coin" && coin == null) coin = collider.transform.parent.gameObject;
                if (collider.transform.parent.gameObject.tag == "Weapon" && weapon == null) weapon = collider.transform.parent.gameObject;
                if (collider.transform.parent.gameObject.tag == "Armor" && armor == null) armor = collider.transform.parent.gameObject;
                if (collider.gameObject.tag == "Entity" && enemy == null &&
                    collider.gameObject.GetComponent<EntityInteraction>().team != entityInteraction.team) enemy = collider.gameObject;
            }
        }
    }
    void FilterAwarenessColliders(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.transform.parent.gameObject.tag == "Coin" && coin == null) coin = collider.transform.parent.gameObject;
            if (collider.transform.parent.gameObject.tag == "Weapon" && weapon == null) weapon = collider.transform.parent.gameObject;
            if (collider.transform.parent.gameObject.tag == "Armor" && armor == null) armor = collider.transform.parent.gameObject;
            if (collider.gameObject.tag == "Entity" && enemy == null &&
                collider.gameObject.GetComponent<EntityInteraction>().team != entityInteraction.team) enemy = collider.gameObject;
        }
    }
    public Vector3 DirFromAngle(float angle, bool global)
    {
        if (!global) angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad), 0, Mathf.Cos(angle*Mathf.Deg2Rad));
    }
}
