using SW.Code.SO;
using SW.Code.Test;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SW.Code.Test
{
    public class TestSetTurretDataText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _textList;

        private List<char[]> _charArrayList;
        int maxValue;
        private bool _isSpecial;


        public void Init()
        {
            _charArrayList = new List<char[]>();
        }

        public void SetTurretDataText(TestTurretSO turretData)
        {
            _charArrayList.Clear();
            StopAllCoroutines();
            SetList(turretData);
            StartCoroutine(SetTurretDataTime(0.1f));
        }

        private IEnumerator SetTurretDataTime(float time)
        {
            _textList[0].text = "";
            _textList[1].text = "공격력 : ";
            _textList[2].text = "공격속도 : ";

            if (_isSpecial)
                _textList[3].text = "환생코인가격 : <color=purple>";
            else
                _textList[3].text = "가격 : ";

            if (_isSpecial)
                _textList[4].text = "등급 : <color=yellow>";
            else
                _textList[4].text = "등급 : ";

            for (int i = 0; i < _charArrayList[0].Length; i++)
            {
                _textList[0].text += _charArrayList[0][i];
                yield return new WaitForSeconds(time);
            }

            for (int j = 0; j < maxValue; j++)
            {
                if (_charArrayList[1].Length > j)
                    _textList[1].text += _charArrayList[1][j];

                if (_charArrayList[2].Length > j)
                    _textList[2].text += _charArrayList[2][j];

                if (_isSpecial)
                {
                    if (_charArrayList[5].Length > j)
                        _textList[3].text += _charArrayList[5][j];
                }
                else
                {
                    if (_charArrayList[3].Length > j || !_isSpecial)
                        _textList[3].text += _charArrayList[3][j];
                }

                if (_charArrayList[4].Length > j)
                    _textList[4].text += _charArrayList[4][j];

                yield return new WaitForSeconds(time);
            }
        }
        private void SetList(TestTurretSO myTurretData)
        {
            _charArrayList.Add(myTurretData.TurretName.ToCharArray());
            _charArrayList.Add(myTurretData.FireDamage.ToString().ToCharArray());
            _charArrayList.Add(myTurretData.FireSpeed.ToString().ToCharArray());
            _charArrayList.Add(myTurretData.Price.ToString().ToCharArray());
            _charArrayList.Add(myTurretData.TurretRating.ToString().ToCharArray());
            _charArrayList.Add(myTurretData.SpecialPrice.ToString().ToCharArray());

            _isSpecial = myTurretData.IsSpecial;
            int max = _charArrayList[1].Length;
            int range = _isSpecial ? _charArrayList.Count : _charArrayList.Count - 1;

            for (int i = 2;  i < range; i++)
            {
                if (max < _charArrayList[i].Length)
                    max = _charArrayList[i].Length;
            }

            maxValue = max;
        }
    }
}