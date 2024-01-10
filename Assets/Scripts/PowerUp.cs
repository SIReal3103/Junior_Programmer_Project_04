using UnityEngine;

public enum PowerUpType
{
    None, Pushback, Missle
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
}
