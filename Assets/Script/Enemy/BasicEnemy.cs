using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageDealer
{
    [SerializeField] private int damage = 30;
    public int Damage => damage;
}
