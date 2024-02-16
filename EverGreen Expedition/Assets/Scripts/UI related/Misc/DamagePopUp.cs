using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.UI_related.Misc
{
    public class DamagePopUp : MonoBehaviour
    {
        private DamageContainer damageContainer;
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI text;

        public void Init( DamageContainer damageContainer)
        {
            this.damageContainer = damageContainer;
        }

        public void Play(int damageCount)
        {
            text.text = $"-{damageCount} <sprite=3>";

            string key = "Start";
            animator.SetTrigger(key);
            StartCoroutine(TimerToGoBack());
        }

        private IEnumerator TimerToGoBack()
        {
            yield return new WaitForSeconds(0.5f);
            damageContainer.Retrieve(this);

        }
    }
}