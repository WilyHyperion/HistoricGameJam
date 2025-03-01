using UnityEngine;
public class HarpoonHead : MonoBehaviour
{
    public bool CanGrab;
    public Rigidbody hit;
    public HarpoonGun parent;
    public Rigidbody rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.forceRenderingOff = true;
        }
        }
    public void Update()
    {
        if (hit != null)
        {
            hit.position = rb.position;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.GetComponent<Holdable>() && CanGrab)
        {
            hit = other.collider.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            hit.linearVelocity = Vector3.zero;
            other.collider.enabled = false;
        }
    }
}
