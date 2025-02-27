
using System.Collections.Generic;
using UnityEngine;

public class CoalBurner : MonoBehaviour
{
    public List<FuelSource> sources = new List<FuelSource>();
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float maxSpeedAt = 10f;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("raa");
        if(other.GetComponent<FuelSource>() != null)
        {
            sources.Add(other.GetComponent<FuelSource>());
            totalFuel += other.GetComponent<FuelSource>().Fuel;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FuelSource>() != null)
        {
            sources.Remove(other.GetComponent<FuelSource>());

            totalFuel -= other.GetComponent<FuelSource>().Fuel;
        }
    }
    public float totalFuel;
    void Update() {
        if (totalFuel <= 0)
        {

            Boat.Instance.Speed = 0;
            return;
        }

        sources[0].Fuel -= Time.deltaTime;
        totalFuel -= Time.deltaTime;
        if (sources[0].Fuel < 0)
        {
            if (sources.Count > 2) {
                sources[1].Fuel -= Time.deltaTime + sources[0].Fuel;
            }
            Destroy(sources[0].gameObject);
            sources.RemoveAt(0);
        }
        
        else {
            Boat.Instance.Speed = Mathf.Lerp(minSpeed, maxSpeed, totalFuel/maxSpeedAt);
        }
    }
}
