using UnityEngine;

public class PowerUpType : MonoBehaviour
{
    public enum PowerType
    {
        None,
        Speed,
        Slow,
        ScoreBooster
    }

    [SerializeField] private PowerType type;

    public PowerType GetPowerType()
    {
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Snake_Controller>() != null)
        {
            this.gameObject.SetActive(false);
        }
    }
}
