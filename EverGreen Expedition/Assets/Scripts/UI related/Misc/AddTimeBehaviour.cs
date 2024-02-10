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

        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            text = gameObject.GetComponent<TextMeshProUGUI>();
            gameObject.SetActive(false);
        }

        public void PlayAnimation(int days)
        {
            gameObject.SetActive(true);
            text.text = $"+{days} Days";
            animator.SetTrigger("Activate");
        }

        public void OnAnimationEnd()
        {
            timeSystem.ReflectTimeChanges();
            gameObject.SetActive(false);

        }
    }
}