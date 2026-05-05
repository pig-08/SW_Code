using PSW.Code.Input;
using PSW.Code.CombinationSkill;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using YIS.Code.Defines;
using YIS.Code.Skills;
using static UnityEngine.Rendering.DebugUI;

namespace PSW.Code.Dial
{
    public class Dial : MonoBehaviour
    {
        [SerializeField] private List<SkillDataSO> testDataList;
        [SerializeField] private UiInputSO uiInput;
        [SerializeField] private SkilliconData skilliconData;
        [SerializeField] private CombinationSkillPanel combinationSkillPanel;

        //아이콘 리스트
        private List<SkillCircle> _skillCircleList = new List<SkillCircle>();
        private Dictionary<Grade, List<SkillDataSO>> _skillDic = new Dictionary<Grade, List<SkillDataSO>>();

        //아이콘 인덱스
        private int _topIndex = 0;
        private int _changeIndex = 0;

        //스킬 인덱스
        private int _currentSkillIndex = -1;
        private Grade _currentType = Grade.Common;

        private void Start()
        {
            _skillCircleList = GetComponentsInChildren<SkillCircle>().ToList();

            for (int i = 0; i < (int)Grade.Legendary + 1; ++i)
                _skillDic.Add((Grade)i, new List<SkillDataSO>());

            foreach (SkillDataSO data in testDataList)
            {
                if (_skillDic.TryGetValue(data.grade, out List<SkillDataSO> dataList))
                {
                    dataList.Add(data);
                }
            }

            _skillCircleList.ForEach(v => v.Init());
            uiInput.OnChoicePressrd += AddSkill;

            SetCurrentData(true);

            _skillCircleList[0].GetSettingCompo().SetSkill(_skillDic.GetValueOrDefault(_currentType)[_currentSkillIndex]);

            SetLeftRightSkillIcon(true, skilliconData.LeftSkillIcon);
            SetLeftRightSkillIcon(false, skilliconData.RightSkillIcon);
        }

        private void Update()
        {
            if (uiInput.IsArrow && SetAll_IsOnAnga() == false)
                SetAngle(uiInput.IsLeft);
        }

        private void AddSkill()
        {
            //if (combinationSkillPanel.GetIsPoolItem()) return;

            //combinationSkillPanel.AddSkill(_skillDic.GetValueOrDefault(_currentType)[_currentSkillIndex]);
            _skillCircleList[_topIndex].PopIIcon();
        }

        private void OnDestroy()
        {
            uiInput.OnChoicePressrd -= AddSkill;
        }

        private void SetAngle(bool isLeft)
        {
            SetCurrentData(isLeft);
            AddInt(ref _topIndex, !isLeft, _skillCircleList.Count - 1);

            _changeIndex = _topIndex;
            AddInt(ref _changeIndex, !isLeft, _skillCircleList.Count - 1);
            SetLeftRightSkillIcon(isLeft, _skillCircleList[_changeIndex].GetSettingCompo());

            _skillCircleList.ForEach(v => v.Angle(isLeft));

        }

        private void SetLeftRightSkillIcon(bool isLeft, SkillIconSettingCompo settionCompo)
        {
            List<SkillDataSO> tempList = _skillDic.GetValueOrDefault(_currentType);
            int rightIndex = _currentSkillIndex;

            if (AddInt(ref rightIndex, isLeft, tempList.Count - 1))
            {
                Grade grade = GetNextTpye(isLeft);
                tempList = _skillDic.GetValueOrDefault(grade);

                if (isLeft == false)
                    rightIndex = -1;

                SetInit(ref rightIndex, tempList.Count - 1);
            }
            settionCompo.SetSkill(tempList[rightIndex]);
        }

        private void SetCurrentData(bool isLeft)
        {
            List<SkillDataSO> tempList = _skillDic.GetValueOrDefault(_currentType);
            if (AddInt(ref _currentSkillIndex, isLeft, tempList.Count - 1))
            {
                SetNext(isLeft);
                tempList = _skillDic.GetValueOrDefault(_currentType);

                if (isLeft == false)
                    _currentSkillIndex = -1;

                SetInit(ref _currentSkillIndex, tempList.Count - 1);
            }
        }

        private void SetNext(bool isLeft)
        {
            int typeValue = (int)_currentType;
            AddInt(ref typeValue, isLeft, (int)Grade.Legendary);
            List<SkillDataSO> tempList = _skillDic.GetValueOrDefault((Grade)typeValue);
            while (tempList.Count == 0)
            {
                AddInt(ref typeValue, isLeft, (int)Grade.Legendary);
                tempList = _skillDic.GetValueOrDefault((Grade)typeValue);
            }
            _currentType = (Grade)typeValue;
        }

        private Grade GetNextTpye(bool isLeft)
        {
            int typeValue = (int)_currentType;
            AddInt(ref typeValue, isLeft, (int)Grade.Legendary);
            List<SkillDataSO> tempList = _skillDic.GetValueOrDefault((Grade)typeValue);
            while (tempList.Count == 0)
            {
                AddInt(ref typeValue, isLeft, (int)Grade.Legendary);
                tempList = _skillDic.GetValueOrDefault((Grade)typeValue);
            }
            return (Grade)typeValue;
        }

        private bool SetAll_IsOnAnga()
        {
            bool AllOnAnga = false;

            _skillCircleList.ForEach(v =>
            {
                AllOnAnga = AllOnAnga || v.GetIsOnAngle();
            });

            return AllOnAnga;
        }
        private bool AddInt(ref int value, bool isLeft, int maxValue)
        {
            value = isLeft ? ++value : --value;

            return SetInit(ref value, maxValue);
        }
        private bool SetInit(ref int value, int maxValue)
        {
            if (value > maxValue)
            {
                value = 0;
                return true;
            }
            else if (value < 0)
            {
                value = maxValue;
                return true;
            }
            return false;
        }
    }




    [Serializable]
    public struct SkilliconData
    {
        public SkillIconSettingCompo LeftSkillIcon;
        public SkillIconSettingCompo RightSkillIcon;
    }

}