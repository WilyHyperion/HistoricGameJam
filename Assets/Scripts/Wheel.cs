using System;
using Unity.VisualScripting;
using UnityEngine;

public class Wheel : Interactable
{
    public float TurnSpeed;
    public float Turn;
    public PlayerMovement Player = PlayerMovement.instance;
    public GameObject WheelObj;
    public GameObject SnapPoint;
    public float WheelMulti;
    public override void OnInteract()
    {
        Player.AtWheel = !Player.AtWheel;
    }
    public override void ChildUpdate()
    {
        if (Player.AtWheel)
        {
            float turn = Input.GetAxisRaw("Horizontal");
            Turn += turn * Time.deltaTime* TurnSpeed;
            Quaternion rotation = Quaternion.AngleAxis(turn * Time.deltaTime * TurnSpeed, Vector3.up);
            Boat.Instance.transform.rotation *= rotation;
            Quaternion wr = Quaternion.AngleAxis(turn * Time.deltaTime * TurnSpeed * WheelMulti * -1, Vector3.up);
            WheelObj.transform.rotation *= wr;
            Player.GetComponent<CharacterController>().enabled = false;
            Player.transform.position = SnapPoint.transform.position;
            Player.GetComponent<CharacterController>().enabled = true;
            if(Math.Abs(turn )> 90)
            {

            }
        }
    }
}
