using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInv : MonoBehaviour
{
    [Header("Perceptions")]
    public bool coinDetected;
    public bool armorDetected;
    public bool weaponDetected;
    public bool isPickingObject;

    [Header("Inventory")]
    [SerializeField] int _totalCoins;
    public int weapon;
    public int armor;

    [Header("Character View")]
    [SerializeField] FieldOfView fow;

    private void Update()
    {
        coinDetected = fow.coin != null;
        armorDetected = fow.armor != null;
        weaponDetected = fow.weapon != null;
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
        if (obj.tag == "Coin") _totalCoins++;
        if (obj.tag == "Armor") armor += obj.GetComponent<Armor>().armor;
        if (obj.tag == "Weapon") weapon += obj.GetComponent<Weapon>().damage;

        obj.gameObject.transform.position += new Vector3(0, 10000, 0);
        Destroy(obj);
        fow.FindVisibleTargets();
        isPickingObject = false;
    }
}
