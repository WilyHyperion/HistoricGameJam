using System;
using UnityEngine;

public class Holdable : MonoBehaviour
{
    public bool LockRotationFront = false;
    public virtual void ChildUpdate()
    {

    }
    public Vector3 HoldOffset = new Vector3(1, 1);
    public bool OnBoat = false;
    public Collider col;
    void Start()
    {
        col = GetComponent<Collider>();
        ChildStart();
    }

    public virtual void ChildStart()
    {
    }

    public void UnHold()
    {

        Debug.Log("removing");
        col.enabled = true;
        this.held = false;
        PlayerMovement.instance.HeldItem = null;
    }
    public virtual void Use()
    {

    }
    public bool held = false;
    void Update()
    {
        ChildUpdate();
        if (held && Input.GetMouseButtonDown(0))
        {
            this.Use();
        }
        
        if (OnBoat)
        {
            this.transform.position += Boat.Delta;
        }
        if(held && LockRotationFront)
        {
            this.transform.forward = PlayerMovement.instance.playerCamera.transform.forward;
            this.transform.rotation *= Quaternion.AngleAxis(90, transform.up);
        }
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoatArea"))
        {
            OnBoat = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BoatArea"))
        {
            OnBoat = false;
        }
    }
}
