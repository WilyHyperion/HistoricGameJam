using UnityEngine;

public class BoatArea : MonoBehaviour
{
    public GameObject Player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Player.GetComponent<PlayerMovement>().OnBoat = true;
        }
        else
        {

            Debug.Log(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerMovement>().OnBoat = false;
        }
    }
}
