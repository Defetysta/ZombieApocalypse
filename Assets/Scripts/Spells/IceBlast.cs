using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlast : Spell
{
    public override void CastSpell(Transform caster)
    {
        //GameObject instance = prefab;
        //Instantiate(instance, caster.position, caster.rotation);
        poolingManager.SpawnFromPool(spellName, caster.position, caster.rotation);
        print("brrrr IceBlast");
    }
}
