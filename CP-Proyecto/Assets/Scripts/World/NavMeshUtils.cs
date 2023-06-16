using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshUtils
{
    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas)) return hit.position;
        return center;
    }

    public static Vector3 RunFrom(GameObject self, GameObject enemy, float maxDistance)
    {
        self.transform.rotation = Quaternion.LookRotation(self.transform.position - enemy.transform.position);
        Vector3 direction = self.transform.position + self.transform.forward * maxDistance;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(direction, out hit, maxDistance, NavMesh.AllAreas)) return hit.position;
        return self.transform.position;
    }
}
