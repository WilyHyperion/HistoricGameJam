using UnityEngine;

public class ShipHP : MonoBehaviour
{
    RectTransform rt;
    float orgWidth;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        orgWidth = rt.rect.width;
    }

    void Update()
    {
        float prgs = Boat.Instance.HP / Boat.Instance.MaxHP;
        rt.sizeDelta = new Vector2(orgWidth * prgs, rt.sizeDelta.y);
    }
}
