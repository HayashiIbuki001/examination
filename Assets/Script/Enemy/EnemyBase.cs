using UnityEngine;

public class EnemyBase: MonoBehaviour, IDamageDealer
{
    [SerializeField] private int damage = 30;
    public int Damage => damage;
}
