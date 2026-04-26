using SW.Code.SO;
using SW.Code.Test;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SW.Code.Temp
{
    public class TempButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI[] dataTextList;
        private TestTurretSO _myTurret;
        private Button _myButton;
        
        public void Init(TestTurretSO turretData)
        {
            _myTurret = turretData;
            icon.sprite = turretData.TurretImage;
            _myButton = GetComponent<Button>();

            _myButton.onClick.AddListener(SetText);
        }

        private void SetText()
        {
            string setcolor = "";
            switch (_myTurret.TurretRating)
            {
                case TestTurretRating.SSS:
                    {
                        setcolor = "<color=yellow>";
                        break;
                    }
                case TestTurretRating.SS:
                    {
                        setcolor = "<color=black>";
                        break;
                    }
                case TestTurretRating.S:
                    {
                        setcolor = "<color=blue>";
                        break;
                    }
                case TestTurretRating.A:
                    {
                        setcolor = "<color=cyan>";
                        break;
                    }
                case TestTurretRating.B:
                    {
                        setcolor = "<color=white>";
                        break;
                    }
                case TestTurretRating.C:
                    {
                        setcolor = "<color=yellow>";
                        break;
                    }
                case TestTurretRating.D:
                    {
                        setcolor = "<color=yellow>";
                        break;
                    }
                case TestTurretRating.F:
                    {
                        setcolor = "<color=rad>";
                        break;
                    }
            }
            dataTextList[4].text = setcolor + _myTurret.TurretRating.ToString();
        }

    }
}