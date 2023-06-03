using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    bool isDead = false;

    int maxHP = 3;
    int currentHP = 3;

    int maxShield = 3;
    int currentShield = 3;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;

    [SerializeField] int shieldIncreaseCombo = 5;
    int currentShieldCombo = 0;
    [SerializeField] Image shieldGauge = null;

    Result theResult;
    NoteManager theNote;
    [SerializeField] MeshRenderer playerMesh = null;

    private void Start()
    {
        theResult = FindObjectOfType<Result>();
    }

    public void Initialized()
    {
        currentHP = maxHP;
        currentShield = 0;
        currentShieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHPImage();
        SettingShieldImage();
    }

    public void CheckShield()
    {
        currentShieldCombo++;

        if(currentShieldCombo >= shieldIncreaseCombo )
        {
            currentShieldCombo = 0;
            InCreaseShield();
        }

        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public void InCreaseShield()
    {
        currentShield++;

        if(currentShield >= maxShield )
            currentShield = maxShield;

        SettingShieldImage();
    }

    public void IncreaseHP(int p_num)
    {
        currentHP += p_num;
        if (currentHP >= maxHP)
            currentHP = maxHP;

        SettingHPImage();
    }

    public void DecreaseShield(int p_num)
    {
        currentShield -= p_num;

        if(currentShield <= 0 )
            currentShield = 0;

        SettingShieldImage();
    }

    public void DecreaseHP(int p_num)
    {
        if (!isBlink)
        {
            if (currentShield > 0)
                DecreaseShield(p_num);
            else
            {
                currentHP -= p_num;

                if (currentHP <= 0)
                {
                    Debug.Log("Game Over");
                    theResult.ShowResult();
                }
                else
                {
                    StartCoroutine(BlinkCo());
                }

                SettingHPImage();
            }
        }
    }

    void SettingHPImage()
    {
        for(int i =0; i<hpImage.Length; i++)
        {
            if(i<currentHP)
                hpImage[i].gameObject.SetActive(true);
            else
                hpImage[i].gameObject.SetActive(false);
        }
    }

    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
                shieldImage[i].gameObject.SetActive(true);
            else
                shieldImage[i].gameObject.SetActive(false);
        }
    }

    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / shieldIncreaseCombo;
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCo()
    {
        isBlink = true;

        while(currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blinkSpeed);
            currentBlinkCount++;
        }

        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
