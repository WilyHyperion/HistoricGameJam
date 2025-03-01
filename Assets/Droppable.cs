
using UnityEngine;

public class Droppable : MonoBehaviour
{
    public GameObject Drop;
    public int amount;
    public int UpwardVariation;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Opener"))
        {
            amount += Random.Range(0, UpwardVariation);
            for(int i = 0; i < amount; i++)
            {
                var g = Instantiate(Drop);
                g.transform.position = this.transform.position;
            }
            Destroy(this.gameObject);
            PlayerMovement.instance.HeldItem = null;
        }   
    }
}
