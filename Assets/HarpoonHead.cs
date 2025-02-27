using UnityEngine;
public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun parent;
    public Rigidbody rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
   /* private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Holdable>())
        {
            other.collider.GetComponent<Rigidbody>().AddForce((other.collider.transform.position - PlayerMovement.instance.transform.position));
        }
    }*/
}
