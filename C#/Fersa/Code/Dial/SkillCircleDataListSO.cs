using System;
using System.Collections.Generic;
using UnityEngine;
using YIS.Code.Defines;

namespace PSW.Code.Dial
{
    [CreateAssetMenu(fileName = "DataListSO", menuName = "SO/Dial/DataList")]
    public class SkillCircleDataListSO : ScriptableObject
    {
        public List<SkillCircleColorData> skillCircleDataList;
        public List<ElementImageData> elementImageDataList;

        public Color GetOutLineColor(Grade grade)
        {
            Color currentColor = new Color();
            foreach (SkillCircleColorData data in skillCircleDataList)
            {
                if (data.Grade == grade)
                    currentColor = data.OutLineColor;
            }
            return currentColor;
        }

        public Sprite GetElementImage(Elemental element)
        {
            Sprite image = null;
            foreach (ElementImageData data in elementImageDataList)
            {
                if (data.elementType == element)
                    image = data.ElementImage;
            }
            return image;
        }
    }

    [Serializable]
    public struct SkillCircleColorData
    {
        public Grade Grade;
        public Color OutLineColor;
    }

    [Serializable]
    public struct ElementImageData
    {
        public Elemental elementType;
        public Sprite ElementImage;
    }

}
