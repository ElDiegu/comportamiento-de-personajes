using System;
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

    // Events used in display control of actions carried by the Character
    public event Action onPicking;

    private void Update()
    {
        coinDetected = fow.coin != null;
        armorDetected = fow.armor != null;
        weaponDetected = fow.weapon != null;
    }
    public static bool inRange(GameObject self, GameObject obj)
    {
        if(self == null || obj == null) return false;
        return Vector3.Distance(self.transform.position, obj.transform.position) <= 2.0f;
    }
    public void PickObject(GameObject obj)
    {
        if(obj == null) return;
        Debug.Log(gameObject.name + ": PickObject " + obj.name);
        isPickingObject = true;
        if(onPicking != null) onPicking();
        StartCoroutine(PickObjectCorroutine(obj, 3.0f));
    }
    IEnumerator PickObjectCorroutine(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (obj == null)
        {
            isPickingObject = false;
            StopAllCoroutines();
        }
        Debug.Log(gameObject.name + " Picked " + obj.name);

        if (obj.tag == "Coin") { totalCoins++; fow.coin = null; coinDetected = false; }
        if (obj.tag == "Armor") { armor += obj.GetComponent<Armor>().armor; fow.armor = null; armorDetected = false; }
        if (obj.tag == "Weapon") { weapon += obj.GetComponent<Weapon>().damage; fow.weapon = null; weaponDetected = false; }

        if(obj != null) Destroy(obj);
        isPickingObject = false;
    }
}
