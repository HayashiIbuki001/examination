using UnityEngine;

public interface IDamageDealer
{
    /// <summary> Player側に送るダメージ値 </summary>
    int Damage { get; }
}
