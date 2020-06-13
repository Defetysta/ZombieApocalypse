using UnityEngine;
[CreateAssetMenu(menuName = "Custom/Enemy")]
public class EnemyType : ScriptableObject
{
    public float enemySpeed;
    public int enemyHP;
    public int enemyDamage;
    public string enemyTag;
}
