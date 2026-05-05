using Febucci.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace PSW.Code.Text
{
    public class TextAnimator : MonoBehaviour
    {
        [SerializeField] private TextAnimator_TMP textAnimator;
        [SerializeField] private TypewriterByCharacter character;

        public void Appearance(string text)
        {
            textAnimator.SetText(text, true);
            textAnimator.ResetState();
        }

        public void AppearanceFixedTime(string text, float totalTime)
        {
            character.ShowText($"<TypewriterByCharacter duration={totalTime}>{text}</TypewriterByCharacter>");
            textAnimator.ResetState();
        }
    }
}