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
    [SerializeField] bool genObjects;

    [Header("Objects")]
    [SerializeField] GameObject coin;
    [SerializeField] GameObject armor;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject bugCharacter;
    [SerializeField] GameObject objectOrganizer;
    [SerializeField] GameObject characterOrganizer;
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
            if (!genObjects) continue;
            Vector3 point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
            if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length > 0) continue;
            Instantiate(coin, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating coin in position: " + point);
        }
    }

    IEnumerator ArmorGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime + 1));
            if (!genObjects) continue;
            Vector3 point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
            if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length > 0) continue;
            Instantiate(armor, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating armor in position: " + point);
        }
    }

    IEnumerator WeaponGenerator()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minGenerationTime, maxGenerationTime + 1));
            if (!genObjects) continue;
            Vector3 point = NavMeshUtils.GetRandomPoint(transform.position, maxDistance);
            if (Physics.OverlapSphere(transform.position, distanceBetweenObjects, objectLayer).Length > 0) continue;
            Instantiate(weapon, point, transform.rotation, objectOrganizer.transform);
            Debug.Log("Generating weapon in position: " + point);
        }
    }

    public void SwapGeneration()
    {
        genObjects = !genObjects;
    }
    public void SpawnBug()
    {
        Instantiate(bugCharacter, characterOrganizer.transform);
    }
}
