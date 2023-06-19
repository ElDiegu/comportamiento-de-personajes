using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    [Header("World")]
    [SerializeField] World world;

    [Header("Controls Parameters")]
    [SerializeField] LayerMask targetLayer;
    [SerializeField] GameObject prefab;

    private void Update()
    {
        ClickEvent();
    }
    void ClickEvent()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;
        if (hit.collider.gameObject.layer != targetLayer) return;
        Instantiate(prefab, NavMeshUtils.GetRandomPoint(hit.point, 0.1f), new Quaternion());
    }
    public void SetPrefab(GameObject newPrefab)
    {
        prefab = newPrefab;
    }
}
