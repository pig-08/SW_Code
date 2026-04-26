using UnityEngine;

namespace SW.Code.Title
{
    public class TitleSword : MonoBehaviour
    {
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private float ShakeValue = 0.1f;
        private bool isShake = true;

        private void Update()
        {
            if(isShake)
            {
                float randx = Random.Range(-ShakeValue, ShakeValue);
                float randy = Random.Range(-ShakeValue, ShakeValue);
                transform.position = new Vector3(transform.position.x + randx, transform.position.y + randy);
                StopPosition();
            }
        }

        private async void StopPosition()
        {
            isShake = false;
            await Awaitable.WaitForSecondsAsync(0.05f);
            transform.position = startPosition;
            isShake = true;
        }
    }
}