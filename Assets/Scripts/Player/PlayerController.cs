using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private IntVariable maxHP = null;
    private int currentHP;

    [SerializeField]
    private Spell[] avaliableSpells = null;
    [SerializeField]
    private Dictionary<Spell, float> cooldowns;

    [SerializeField]
    private GameObject spellGroups = null;
    private Text[] cooldownTexts = null;
    private Image[] spellImages = null;
    private void OnEnable()
    {
        currentHP = maxHP.Value;
        cooldownTexts = new Text[spellGroups.transform.childCount];
        spellImages = new Image[spellGroups.transform.childCount];
        cooldowns = new Dictionary<Spell, float>();
        foreach (var item in avaliableSpells)
        {
            cooldowns.Add(item, 0);
        }
        for (int i = 0; i < spellGroups.transform.childCount; i++)
        {
            spellImages[i] = spellGroups.transform.GetChild(i).GetComponent<Image>();
            cooldownTexts[i] = spellImages[i].GetComponentInChildren<Text>();
        }
    }

    public void HitPlayer(int damage)
    {
        currentHP -= damage;
    }

    private void Start()
    {
        StartCoroutine(AwaitPlayerDying());
    }
    private IEnumerator AwaitPlayerDying()
    {
        yield return new WaitUntil(() => currentHP <= 0);
        Debug.Log("Player died!");
    }


    private void Update()
    {
        HandleSpellInput();
        ReduceCooldowns();
    }



    private void HandleSpellInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            CastSpell(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            CastSpell(1);
    }
    private void ReduceCooldowns()
    {
        foreach (var item in cooldowns.ToList())
        {
            if (item.Value > 0)
            {
                cooldowns[item.Key] -= Time.deltaTime;

            }
        }
    }

    private void CastSpell(int id)
    {
        if (cooldowns[avaliableSpells[id]] <= 0)
        {
            avaliableSpells[id].CastSpell(transform);
            cooldowns[avaliableSpells[id]] = avaliableSpells[id].maxCooldown;
            spellImages[id].color = Color.black;
            StartCoroutine(HandleSpellsUIChange(cooldownTexts[id], spellImages[id], cooldowns[avaliableSpells[id]]));
        }
        else
        {
            Debug.Log("Spell " + avaliableSpells[id].spellName + " is on cooldown!");
        }
    }

    private IEnumerator HandleSpellsUIChange(Text cdText, Image img, float cd)
    {
        while(cd >= 0)
        {
            cd -= Time.deltaTime;
            cdText.text = cd.ToString("0.00");
            yield return null;
        }
        img.color = Color.white;
        cdText.text = "0";
    }
}
