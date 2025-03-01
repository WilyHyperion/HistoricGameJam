using UnityEngine;

public class Rock : MonoBehaviour
{
    public int dmg = 10;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat")){
            Boat.Instance.HP -= dmg;
            Destroy(gameObject);
            PlayerMovement.TriggerScreenShake(1);
            Boat.Instance.hitsoud.Play();
        }
    }
}
