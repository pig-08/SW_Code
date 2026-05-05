using DG.Tweening;
using PSW.Code.Deck;
using PSW.Code.Dial.Slot;
using PSW.Code.EventBus;
using YIS.Code.Skills;

public class DescriptionSkillIcon_Controller : SkillIconSetting_Controller
{
    private DescriptionSkillIcon_View _view;
    private DescriptionSkillIcon_Model _model;

    private void Awake()
    {
        _view = view as DescriptionSkillIcon_View;
        _model = model as DescriptionSkillIcon_Model;

        Bus<DescriptionSkillIEvent>.OnEvent += Description;
        Bus<DonChangeSkillDataEvent>.OnEvent += SetDonChange;
    }

    private void OnDestroy()
    {
        Bus<DescriptionSkillIEvent>.OnEvent -= Description;
        Bus<DonChangeSkillDataEvent>.OnEvent -= SetDonChange;
    }

    private void SetDonChange(DonChangeSkillDataEvent evt) => _model.SetDonChangeData(evt.isDonChange);

    private void Description(DescriptionSkillIEvent evt)
    {
        if (_model.GetDonChangeData() && evt.changeData == false) return;

        if (evt.skillData != null)
        {
            SetSkill(evt.skillData);
            _view.SetUpText(evt.skillData.visualData.uiName, evt.skillData.visualData.itemDescription, evt.skillData.grade, true);
        }
        else
        {
            view.DOKill();
            PopUp(false);
            _view.SetUpText("","", YIS.Code.Defines.Grade.Common, false);
        }
    }

    public override void SetSkill(SkillDataSO skillData)
    {
        base.SetSkill(skillData);
        PopUp(true);
    }

    public override void PopUp()
    {
        base.PopUp();
    }
}
