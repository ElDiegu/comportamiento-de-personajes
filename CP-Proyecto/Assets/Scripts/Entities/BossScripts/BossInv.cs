using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossInv : MonoBehaviour
{
    [Header("Perceptions")]
    public bool isPickingObject;
    public bool powerUpDetected;
    public bool isIncreasingVelocity;
    public NavMeshAgent nav;

    [Header("Inventory")]
    public int _totalPowerUp;
    
    [Header("Character View")]
    [SerializeField] BossFieldOfView fow;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        powerUpDetected = fow.powerUp != null;
    }
    public static bool inRange(GameObject self, GameObject obj)
    {
        return Vector3.Distance(self.transform.position, obj.transform.position) <= 1.0f;
    }
    public void PickObject(GameObject obj)
    {
        Debug.Log(gameObject.name + ": PickObject " + obj.name);
        isPickingObject = true;
        StartCoroutine(PickObjectCorroutine(obj, 3.0f));
    }
    IEnumerator PickObjectCorroutine(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (obj.tag == "Power Up") _totalPowerUp++;

        obj.gameObject.transform.position += new Vector3(0, 10000, 0);
        Destroy(obj);
        fow.FindVisibleTargets();
        Debug.Log("holi");
        isPickingObject = false;
    }
    public void IncreaseVelocity()
    {
        Debug.Log(gameObject.name + ": Increasing velocity");
        isIncreasingVelocity = true;
        StartCoroutine(IncreaseVelocityCoroutine(0.2f, 3.0f));
    }
    IEnumerator IncreaseVelocityCoroutine(float vel, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _totalPowerUp -= 5;
        nav.speed += vel;
        isIncreasingVelocity = false;
    }
}
