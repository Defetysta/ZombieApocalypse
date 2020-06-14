using UnityEngine;

public class IceBlast : Spell
{
    public float speedMultiplier;
    public float duration;
    public override void CastSpell(Transform caster)
    {
        poolingManager.SpawnFromPool(spellName, caster.position, caster.rotation);
        print("brrrr IceBlast");
    }
}
