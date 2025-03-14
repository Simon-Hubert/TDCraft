using UnityEngine;

namespace Enemies
{
    public class ClosestTargetAI : AI
    {
        protected override Transform ChooseTarget() {
            Transform closest = transform;
            if (targets.Count <= 0) return closest;
            closest = targets[0];
            float mindist = ((Vector2)(closest.position - transform.position)).sqrMagnitude;
            foreach (Transform t in targets) {
                float dist = ((Vector2)(t.position - transform.position)).sqrMagnitude;
                if (!(dist < mindist)) continue;
                mindist = dist;
                closest = t;
            }
            return closest;
        }
    }
}