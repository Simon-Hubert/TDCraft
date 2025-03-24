using PlasticPipe.PlasticProtocol.Client;
using UnityEngine;

namespace Entities
{
    public class ClosestTargetAI : AI
    {
        private Attack _attack; //TODO currently supports only one attack, could be more
        
        protected override void Start() {
            base.Start();
            _attack = GetComponent<Attack>();
        }

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
        protected override Vector2 ChooseDestination() {
            return _attack.IsTargetInRange() ? transform.position : _mainTarget.position;
        }
    }
}