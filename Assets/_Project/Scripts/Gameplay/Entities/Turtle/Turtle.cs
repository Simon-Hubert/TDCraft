using UnityEngine;

namespace Entities
{
    public class Turtle : MonoBehaviour
    {
        public  Life _life;
        public  Rigidbody2D Rb;
        public Inventory Inventory;

        public Vector3 Movements;
        void Start()
        {
        
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Movements != Vector3.zero)
            {
                transform.position += Movements;
            }
        }
    
    }
}
