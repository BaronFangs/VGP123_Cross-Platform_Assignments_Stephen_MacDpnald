using UnityEngine;

public interface IPickup
{
    public void Pickup(GameObject Player);
}



public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        JumpBoost,
        Shrink,
        Score
    }

    public PickupType type;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();

            switch (type)
            {
                case PickupType.Life:
                    pc.lives++;
                    break;
                case PickupType.JumpBoost:
                    pc.JumpPowerUp();
                    break;
                case PickupType.Shrink: break;
                case PickupType.Score:
                    pc.score++;
                    break;
            }
            Destroy(gameObject);
        }
    }
}