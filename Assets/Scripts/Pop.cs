using UnityEngine;

public class Pop : MonoBehaviour
{

    public static Pop obj;
   public void Awake()
    {
        obj = this;
    }

    public void show(Vector3 pos)
    {
        transform.position = pos;
        gameObject.SetActive(true);
    }
    public void dissapear()
    {
        gameObject.SetActive(false);

    }

    void OnDestroy()
    {
        obj = null;
    }
}
