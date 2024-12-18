using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Study : MonoBehaviour
{
    [SerializeField] TextAsset gachaTable;
    [SerializeField] TextAsset chaTable;
    [SerializeField] TextAsset textTable;

    Dictionary<int, Gacha> gachaRateDic = new Dictionary<int, Gacha>();
    Dictionary<int, List<Character>> CharacterGradeDic = new Dictionary<int, List<Character>>();
    Dictionary<int, Character> chaDic = new Dictionary<int, Character>();
    Dictionary<string, TextString> textDic = new Dictionary<string, TextString>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI criChanText;
    public TextMeshProUGUI criMulText;

    private void Start()
    {
        SettingGachaRate(CSVReader.Read(gachaTable));
        SettingCha(CSVReader.Read(chaTable));
        SettingTextData(CSVReader.Read(textTable));
    }

    void SettingGachaRate(List<Dictionary<string, object>> csvFile)
    {
        gachaRateDic.Clear();
        for (int i = 0; i < csvFile.Count; i++)
        {
            Gacha newGacha = new Gacha();
            int _id = (int)csvFile[i]["id"]; // 정수형으로 바꿔
            int _rate = (int)csvFile[i]["rate"];

            newGacha.gachaId = _id;
            newGacha.gradeRate.Add(_rate);

            if (!gachaRateDic.ContainsKey(_id))
            {
                gachaRateDic.Add(_id, newGacha);
            }
            else
            {
                gachaRateDic[_id].gradeRate.Add(_rate);
            }
        }
    }

    void SettingCha(List<Dictionary<string, object>> readData)
    {
        chaDic.Clear();
        for (int i = 0; i < readData.Count; i++)
        {
            int _id = (int)readData[i]["id"];
            string _name = readData[i]["name"].ToString();
            int _grade = (int)readData[i]["grade"];
            int _hp = (int)readData[i]["hp"];
            int _atkPower = (int)readData[i]["atkPower"];
            int _criChan = (int)readData[i]["criChan"];
            int _criMul = (int)readData[i]["criMul"];

            Character newChar = new Character(_id, _grade, _hp, _atkPower,
                _criChan, _criMul, _name);

            chaDic.Add(_id, newChar);

            if (!CharacterGradeDic.ContainsKey(_grade))
            {
                List<Character> _list = new List<Character>();
                _list.Add(newChar);
                CharacterGradeDic.Add(_grade, _list);
            }
            else
            {
                CharacterGradeDic[_grade].Add(newChar);
            }
        }
    }

    void SettingTextData(List<Dictionary<string, object>> readData)
    {
        textDic.Clear();
        for (int i = 0; i < readData.Count; i++)
        {
            string _id = readData[i]["id"].ToString();
            string _ko = readData[i]["ko"].ToString();
            string _en = readData[i]["en"].ToString();
            TextString textString = new TextString();
            textString.ko = _ko;
            textString.en = _en;

            textDic.Add(_id, textString);
        }

    }

    public void GachaGradeRateTest()
    {
        int testGradeValue_01 = 0;
        int testGradeValue_02 = 0;
        int testGradeValue_03 = 0;
        int testGradeValue_04 = 0;
        int testGradeValue_05 = 0;

        int grade = 0;

        int kjGValue = 0;
        for (int i = 0; i < gachaRateDic[1].gradeRate.Count; i++)
        {
            kjGValue += gachaRateDic[1].gradeRate[i];
        }


        for (int cnt = 0; cnt < 100000; cnt++)
        {
            int gradeRandValue = Random.Range(0, kjGValue);
            for (int i = 0; i < gachaRateDic[1].gradeRate.Count; i++)
            {
                int minValue = 0;

                if (i != 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        minValue += gachaRateDic[1].gradeRate[j];
                    }
                }

                int maxValue = minValue + gachaRateDic[1].gradeRate[i];

                if (gradeRandValue >= minValue && gradeRandValue < maxValue)
                {
                    grade = i;
                    switch (grade)
                    {
                        case 0:
                            testGradeValue_01++;
                            break;
                        case 1:
                            testGradeValue_02++;
                            break;
                        case 2:
                            testGradeValue_03++;
                            break;
                        case 3:
                            testGradeValue_04++;
                            break;
                        case 4:
                            testGradeValue_05++;
                            break;
                    }
                    break;
                }
            }
        }

        Debug.Log("1 등급 : " + testGradeValue_01 + "\n" + "2 등급 : " + testGradeValue_02 + "\n"
             + "3 등급 : " + testGradeValue_03 + "\n"
              + "4 등급 : " + testGradeValue_04 + "\n"
               + "5 등급 : " + testGradeValue_05);

    }


    private Character CharacterGacha()
    {
        int grade = 0;

        int kjGValue = 0;
        for (int i = 0; i < gachaRateDic[1].gradeRate.Count; i++)
        {
            kjGValue += gachaRateDic[1].gradeRate[i];
        }

        int gradeRandValue = Random.Range(0, kjGValue);
        for (int i = 0; i < gachaRateDic[1].gradeRate.Count; i++)
        {
            int minValue = 0;

            if (i != 0)
            {
                for (int j = 0; j < i; j++)
                {
                    minValue += gachaRateDic[1].gradeRate[j];
                }
            }

            int maxValue = minValue + gachaRateDic[1].gradeRate[i];

            if (gradeRandValue >= minValue && gradeRandValue < maxValue)
            {
                grade = i;
                break;
            }
        }

        List<Character> gachaCharList = CharacterGradeDic[grade + 1];

        int randCharacterIndex = Random.Range(0, gachaCharList.Count);

        return gachaCharList[randCharacterIndex];
    }

    public void Gatcha()
    {
        Character cha = CharacterGacha();

        nameText.text = textDic[cha.name].ko;
        atkText.text = cha.atkPower.ToString();
        hpText.text = cha.hp.ToString();
        gradeText.text = cha.grade.ToString();
        criChanText.text = cha.criChan.ToString();
        criMulText.text = cha.criMul.ToString();
    }
}

public class TextString
{
    public string ko;
    public string en;
    //public string ko;
    //public string ko;
    //public string ko;
}

public class Gacha
{
    public int gachaId;
    public List<int> gradeRate = new List<int>();
}


public class Character
{
    public int chaId;
    public int grade;
    public int hp;
    public int atkPower;
    public int criChan, criMul;
    public string name;

    public Character(int _id, int _grade, int _hp, int _atkPower,
        int _criChan, int _criMul, string _name)
    {
        chaId = _id;
        grade = _grade;
        hp = _hp;
        atkPower = _atkPower;
        criChan = _criChan;
        criMul = _criMul;
        name = _name;
    }
}