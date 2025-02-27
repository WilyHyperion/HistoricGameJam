using System;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.Video;

public class Holdable : MonoBehaviour
{
    public bool LockRotationFront = false;
    public virtual void ChildUpdate()
    {

    }
    public Vector3 HoldOffset = new Vector3(1, 1);
    public bool OnBoat = false;
    public float GrabDistance = 25f;
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
        if ((PlayerMovement.instance.transform.position - this.transform.position).magnitude < Mathf.Pow(GrabDistance, 2)  &&  Input.GetKeyDown(KeyCode.F)){
            if (held)
            {
                this.transform.position = PlayerMovement.instance.transform.position + PlayerMovement.instance.transform.forward * 1;
                UnHold();

            }
            else
            {
                if (PlayerMovement.instance.HeldItem != null)
                {
                    PlayerMovement.instance.HeldItem.UnHold();
                }
                PlayerMovement.instance.HeldItem = this;
                col.enabled = false;
            }
            this.held = !held;
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, GrabDistance);
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
