using UnityEngine;
using Work.PSB.Code.FieldCode.MapSaves;

namespace Work.PSB.Code.CoreSystem.Tests
{
    public class PortalAnimPlayer : MonoBehaviour
    {
        [SerializeField] private NormalPortalEntity portal;

        public void AnimPortal()
        {
            portal.Open();
        }
        
        
    }
}