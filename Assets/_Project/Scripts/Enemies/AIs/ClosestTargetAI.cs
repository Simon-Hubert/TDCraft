using UnityEngine;

namespace Enemies
{
    public class ClosestTargetAI : AI
    {
        protected override Vector2 ChooseTarget() {
            Vector2 closest = transform.position;
            if (targets.Count <= 0) return closest;
            closest = targets[0];
            float mindist = (closest - (Vector2)transform.position).sqrMagnitude;
            foreach (Vector2 t in targets) {
                float dist = (t - (Vector2)transform.position).sqrMagnitude;
                if (!(dist < mindist)) continue;
                mindist = dist;
                closest = t;
            }
            return closest;
        }
    }
}