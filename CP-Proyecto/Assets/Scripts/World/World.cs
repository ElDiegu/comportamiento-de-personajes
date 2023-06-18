using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class World : MonoBehaviour
{
    [Header("World Parameters")]
    [SerializeField] float minGenerationTime;
    [SerializeField] float maxGenerationTime;
    [SerializeField] float distanceBetweenObjects;
    public float maxDistance;

    [Header("Objects")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject armor;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject objectOrganizer;
    [SerializeField] LayerMask objectLayer;
    private void Start()
    {
        StartCoroutine(CoinGenerator());
        StartCoroutine(ArmorGenerator());
        StartCoroutine(WeaponGenerator());
    }
    IEnumerator CoinGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime + 1));
            Vector3 point = new Vector3(0, 0, 0);
            while (true)
            {
                point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
                if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length <= 0) break;
            }
            Instantiate(coin, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating coin in position: " + point);
        }
    }

    IEnumerator ArmorGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime + 1));
            Vector3 point = new Vector3(0, 0, 0);
            while (true)
            {
                point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
                if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length <= 0) break;
            }
            Instantiate(armor, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating armor in position: " + point);
        }
    }

    IEnumerator WeaponGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime + 1));
            Vector3 point = new Vector3(0, 0, 0);
            while (true)
            {
                point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
                if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length <= 0) break;
            }
            Instantiate(weapon, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating weapon in position: " + point);
        }
    }
}
