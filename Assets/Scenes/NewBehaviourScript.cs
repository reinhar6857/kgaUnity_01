using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monster : MonoBehaviour
{
       
    public TextAsset testDataFile;

    // 몬스터 기본 정보
    public string monName;
    public float monMaxHp;
    public float monAtkPower;

    public Image hpImage;
    public Monster target;
    public TextMeshProUGUI hpText;
    public int reCnt;

    public float monCurHp;
    public float coolTime;
    string die = "으앙 쥬금 ㅠ";

    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = monName;
        monCurHp = monMaxHp;
        hpText.text = monCurHp + " / " + monMaxHp;
    }


    float cTime;

    void Update()
    {
        if (monCurHp > 0)
        {
            if (cTime >= coolTime)
            {
                target.OnDamage(monAtkPower);
                cTime = 0;
            }
            else
            {
                cTime += Time.deltaTime;
            }
        }
    }

    public void OnDamage(float _monAtkPower)
    {
        monCurHp -= _monAtkPower;
        if (monCurHp <= 0)
        {
            if (reCnt > 0)
            {
                monCurHp = monMaxHp / 2f;
                reCnt--;
                hpText.text = monCurHp + " / " + monMaxHp;
            }
            else
            {
                hpText.text = die;
            }
        }
        else
        {
            hpText.text = monCurHp + " / " + monMaxHp;
        }
        float fill = monCurHp / monMaxHp;
        hpImage.fillAmount = fill;
    }
}