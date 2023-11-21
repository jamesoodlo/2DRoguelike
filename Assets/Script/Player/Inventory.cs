using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    InputHandle inputHandle;
    PlayerController playerController;
    PlayerStats playerStats;
    public Info info;
    public int atkOrb, defOrb, spdOrb;
    public bool isAtkBonus, isDefBonus, isSpdBonus;
    public float timeSinceAtkBonus = 5.0f, timeSinceDefBonus = 5.0f, timeSinceSpdBonus = 5.0f;

    [Header("Text")]
    public TextMeshProUGUI atkTimeText;
    public TextMeshProUGUI defTimeText;
    public TextMeshProUGUI spdTimeText;
    public TextMeshProUGUI atkOrbText;
    public TextMeshProUGUI defOrbText;
    public TextMeshProUGUI spdOrbText;

    private void Awake() 
    {
        playerController = GetComponent<PlayerController>();
        inputHandle = GetComponent<InputHandle>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void Update() 
    {
        atkOrb = info.atkOrb;
        defOrb = info.defOrb;
        spdOrb = info.spdOrb;

        atkOrbText.text = atkOrb.ToString();
        defOrbText.text = defOrb.ToString();
        spdOrbText.text = spdOrb.ToString();

        if(timeSinceAtkBonus < 5.0f)
        {
            atkTimeText.text = timeSinceAtkBonus.ToString("F1");
        }
        else if(timeSinceAtkBonus >= 5.0f)
        {
            atkTimeText.text = "";
        }

        if(timeSinceDefBonus < 5.0f)
        {
            defTimeText.text = timeSinceDefBonus.ToString("F1");
        }
        else if(timeSinceDefBonus >= 5.0f)
        {
            defTimeText.text = "";
        }
        
        if(timeSinceSpdBonus < 5.0f)
        {
            spdTimeText.text = timeSinceSpdBonus.ToString("F1");
        }
        else if(timeSinceSpdBonus >= 5.0f)
        {
            spdTimeText.text = "";
        }

        AtkBonus();
        DefBonus();
        SpdBonus();
    }

    public void AtkBonus()
    {
        if(inputHandle.item1 && timeSinceAtkBonus >= 5.0f)
        {
            if(atkOrb > 0)
            {
                info.atkOrb -= 1;
                isAtkBonus = true;
                playerController.bonusAttack += 30;
            }
        }

        if(isAtkBonus)
        {
            timeSinceAtkBonus -= Time.deltaTime;
        }

        if(timeSinceAtkBonus <= 0)
        {
            playerController.bonusAttack = 0;
            timeSinceAtkBonus = 5.0f;
            isAtkBonus = false;
        }
        
    }

    public void DefBonus()
    {
        if(inputHandle.item2 &&  timeSinceSpdBonus >= 5.0f) 
        {
            if(defOrb > 0)
            {
                info.defOrb -= 1;
                isDefBonus = true;
                playerStats.bonusDef += 1.8f;
            }
        }

        if(isDefBonus)
        {
            timeSinceDefBonus -= Time.deltaTime;
        }

        if(timeSinceDefBonus <= 0)
        {
            playerStats.bonusDef = 0;
            timeSinceDefBonus = 5.0f;
            isDefBonus = false;
        }
    }

    public void SpdBonus()
    {
        if(inputHandle.item3 && timeSinceSpdBonus >= 5.0f)
        {
            if(spdOrb > 0)
            {
                info.spdOrb -= 1;
                isSpdBonus = true;
                playerController.bonusSpeed += 5;
            }
        }

        if(isSpdBonus)
        {
            timeSinceSpdBonus -= Time.deltaTime;
        }

        if(timeSinceSpdBonus <= 0)
        {
            playerController.bonusSpeed += 0;
            timeSinceSpdBonus = 5.0f;
            isSpdBonus = false;
        }
    }
}
