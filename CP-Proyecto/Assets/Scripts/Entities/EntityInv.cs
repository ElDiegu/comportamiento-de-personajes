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
    public int totalCoins;
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
        return Vector3.Distance(self.transform.position, obj.transform.position) <= 0.5f;
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
        Debug.Log(gameObject.name + " Picked " + obj.name);

        if (obj.tag == "Coin") { totalCoins++; fow.coin = null; coinDetected = false; }
        if (obj.tag == "Armor") { armor += obj.GetComponent<Armor>().armor; fow.armor = null; armorDetected = false; }
        if (obj.tag == "Weapon") { weapon += obj.GetComponent<Weapon>().damage; fow.weapon = null; weaponDetected = false; }

        Destroy(obj);
        isPickingObject = false;
    }
}
