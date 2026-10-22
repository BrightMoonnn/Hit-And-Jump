using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int scoreGive = 30;

         void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);

            AudioManager.obj.playCoin();

            UIManager.obj.updateScore();

            FXManager.obj.showPop(transform.position);

            gameObject.SetActive(false);
        }
    }
    
}
