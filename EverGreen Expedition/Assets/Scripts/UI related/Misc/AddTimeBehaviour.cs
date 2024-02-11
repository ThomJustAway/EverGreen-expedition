using Assets.Scripts.Scripts_for_level_selection;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI_related.Misc
{
    public class AddTimeBehaviour : MonoBehaviour
    {
        [SerializeField] private TimeSystem timeSystem;
        private TextMeshProUGUI text;
        private Animator animator;

        private void Awake() //make it disable when awake to prevent any race case
        {
            gameObject.SetActive(false);
        }

        public void PlayAnimation(int days)
        {
            gameObject.SetActive(true);
            if (animator == null || text == null)
            {
                animator = gameObject.GetComponent<Animator>();
                text = gameObject.GetComponent<TextMeshProUGUI>();
            }

            text.text = $"+{days} Days";
            animator.SetTrigger("Activate");
        }

        //called when the animation is ended
        public void OnAnimationEnd()
        {
            timeSystem.ReflectTimeChanges();
            gameObject.SetActive(false);
        }
    }
}