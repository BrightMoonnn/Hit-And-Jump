using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
   void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            FXManager.obj.showPop(transform.position);
        {
            Game.obj.gameOver();
        }
    }
}
