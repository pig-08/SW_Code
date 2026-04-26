using System;
using System.Collections.Generic;
using UnityEngine;

namespace SW.Code.Stat
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSyatem/StatSO")]
    public class StatSO : ScriptableObject, ICloneable
    {
        /// <summary>
        /// value가 변경됨에 따라서 발행되는 이벤트
        /// </summary>
        /// <param mainName="stat"></param>
        /// <param mainName="current"></param>
        /// <param mainName="previous"></param>
        public delegate void ValueChangeHandler(StatSO stat, float current, float previous);
        public event ValueChangeHandler OnValueChange;

        /// <summary>
        /// 스탯의 이름
        /// </summary>
        public string statName;
        /// <summary>
        /// 스탯설명
        /// </summary>
        [TextArea]
        public string description;

        /// <summary>
        /// 스탯의 기본값 , 최소값 , 최대값
        /// </summary>
        [SerializeField] private float _baseValue, _minValue, _maxValue;
        /// <summary>
        /// 업그레이드 타입
        /// </summary>
        [SerializeField] private UpGradepType type;
        /// <summary>
        /// Modify들을 모아놓은 딕셔너리 중복검사를 하기위해 딕셔너리를 이용하였다.
        /// </summary>
        private Dictionary<object, float> _modifyDictionary = new Dictionary<object, float>();

        private List<float> _percentList = new List<float>();

        /// <summary>
        /// 퍼센트를 활용하는 값인지에 따른 bool값
        /// </summary>
        [field: SerializeField] public bool IsPercent { get; private set; }

        /// <summary>
        /// 현재 baseValue에 더해지는 값
        /// </summary>
        private float _modifiedValue = 0;
        public float MaxValue
        {
            get => _maxValue;
            set => _maxValue = value;
        }

        public float MinValue
        {
            get => _minValue;
            set => _minValue = value;
        }

        public float Value
        {
            get
            {
                float value = _baseValue + _modifiedValue;

                foreach (float percent in _percentList)
                {
                    value *= percent;
                }
                
                return Mathf.Clamp(value, MinValue, MaxValue);
            }
        }
        public bool IsMax => Value == MaxValue;
        public bool IsMin => Value == MinValue;

        public float BaseValue
        {
            get => _baseValue;

            set
            {
                float prevValue = Value;
                _baseValue = Mathf.Clamp(value, MinValue, MaxValue);
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }
         
        public UpGradepType Type => type;
        /// <summary>
        /// modify를 추가하는 함수
        /// </summary>
        /// <param mainName="key"></param>
        /// <param mainName="value"></param>
        public void AddModifier(object key, float value, bool isPercent = false)
        {

            if (_modifyDictionary.ContainsKey(key)) return;

            float prevValue = Value;


            if (isPercent)
                _percentList.Add(value);
            else
                _modifiedValue += value;

            _modifyDictionary.Add(key, value);

            TryInvokeValueChangedEvent(Value, prevValue);
        }

        /// <summary>
        /// 존재하는 modify중 key와 동일한 key를 가지는 modify가 존재한다면 제거한다
        /// </summary>
        /// <param mainName="key"></param>
        public void RemoveModifier(object key, bool isPercent = false)
        {
            if (_modifyDictionary.TryGetValue(key, out float value))
            {
                float prevValue = 0;
                if (isPercent)
                {
                    prevValue = Value;
                    int index = _percentList.IndexOf(value);
                    _percentList.Remove(index);
                }
                else
                {
                    prevValue = Value;
                    _modifiedValue -= value;
                    _modifyDictionary.Remove(key);
                }
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        /// <summary>
        /// 모든 Modify를 제거하는 함수
        /// </summary>
        public void ClearAllModifier()
        {
            float prevValue = Value;
            _modifyDictionary.Clear();
            _percentList.Clear();
            _modifiedValue = 0;
            TryInvokeValueChangedEvent(Value, prevValue);
        }

        /// <summary>
        /// 이전값과 일치하지 않으면 OnValueChange 인보크
        /// </summary>
        /// <param mainName="current"></param>
        /// <param mainName="prevValue"></param>
        private void TryInvokeValueChangedEvent(float current, float prevValue)
        {
            //이전값과 일치하지 않으면 이벤트 인보크
            if (Mathf.Approximately(current, prevValue) == false)
            {
                OnValueChange?.Invoke(this, current, prevValue);
            }
        }

        public object Clone() => Instantiate(this);
    }
}