using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YIS.Code.Skills;

namespace PSW.Code.Dial
{
    public class SkillCircle : MonoBehaviour
    {
        [SerializeField] private List<PointData> pointDataList;
        [SerializeField] private SkillPointType startAngleType;
        [SerializeField] private float pointTime = 0.8f;
        [SerializeField] private float shakingValue = 25f;

        private Dictionary<SkillPointType, Vector3> _pointDic = new Dictionary<SkillPointType, Vector3>();
        private Dictionary<SkillPointType, Vector3> _sizeDic = new Dictionary<SkillPointType, Vector3>();

        private SkillPointType _currentAngleType;
        private bool _isOnAngle;

        private SkillIconSettingCompo _skillIconSetting;

        public void Init()
        {
            _skillIconSetting = GetComponentInChildren<SkillIconSettingCompo>();
            _currentAngleType = startAngleType;
            
            foreach(PointData data in pointDataList)
            {
                _pointDic.Add(data.PointType, data.MovePoint);
                _sizeDic.Add(data.PointType, data.TargetSize);
            }
            transform.DOLocalMove(_pointDic.GetValueOrDefault(_currentAngleType), 0);
            transform.DOScale(_sizeDic.GetValueOrDefault(_currentAngleType), 0).OnComplete(() => _isOnAngle = false);
        }

        public bool GetIsOnAngle() => _isOnAngle;

        public SkillIconSettingCompo GetSettingCompo() => _skillIconSetting;

        public void PopIIcon() => _skillIconSetting.PopUp(); 

        public void Angle(bool isLeft)
        {
            _isOnAngle = true;

            int typeValue = (int)_currentAngleType;

            if (isLeft)
            {
                typeValue++;
                typeValue %= (int)SkillPointType.Max;
            }
            else
            {
                typeValue--;
                if (typeValue < 0) 
                    typeValue = (int)SkillPointType.Left;
            }

            _currentAngleType = (SkillPointType)typeValue;
            MoveAngle(isLeft);
        }
        private async void MoveAngle(bool isLeft)
        {
            Vector3 targetAngle = transform.eulerAngles;

            if (isLeft)
                targetAngle.z -= shakingValue;
            else
                targetAngle.z += shakingValue;

            Vector3 endAngle = transform.eulerAngles;

            transform.DORotate(targetAngle, pointTime);
            transform.DOLocalMove(_pointDic.GetValueOrDefault(_currentAngleType), pointTime);
            transform.DOScale(_sizeDic.GetValueOrDefault(_currentAngleType), pointTime);
            await Awaitable.WaitForSecondsAsync(pointTime - 0.2f, destroyCancellationToken);
            transform.DORotate(endAngle, pointTime).SetEase(Ease.InOutBack).
                OnComplete(() => _isOnAngle = false);

        }
         
    }

    [Serializable]
    public struct PointData
    {
        public SkillPointType PointType;
        public Vector3 MovePoint;
        public Vector3 TargetSize;
    }

    public enum SkillPointType
    {
        Top = 0,
        Right = 1,
        BottomRight = 2,
        BottomLeft = 3,
        Left = 4,
        Max = 5
    }

}