﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private IntVariable difficultyLevel = null;
    private int currentHP;

    [SerializeField]
    private Spell[] avaliableSpells = null;
    [SerializeField]
    private Dictionary<Spell, float> cooldowns;


    [SerializeField]
    private GameEventRaiser onPlayerDied = null;
    [SerializeField]
    private GameObject spellGroups = null;
    private Text[] cooldownTexts = null;
    private Image[] spellImages = null;
    private void OnEnable()
    {
        switch (difficultyLevel.Value)
        {
            case 0:
                currentHP = 50000;
                break;
            case 1:
                currentHP = 40000;
                break;
            case 2:
                currentHP = 30000;
                break;
            default:
                Debug.LogError("Invalid difficulty level");
                break;
        }
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
        onPlayerDied.RaiseEvent();
        Debug.Log("Player died!");
        Time.timeScale = 0;
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
        img.color = Color.green;
        cdText.text = "0";
    }
}
