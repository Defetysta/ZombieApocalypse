using UnityEngine;

public class FireStrike : Spell
{
    public override void CastSpell(Transform caster)
    {
        //GameObject instance = prefab;
        poolingManager.SpawnFromPool(spellName, caster.position, caster.rotation);
        Debug.Log("Fly fireball!");
    }
}
