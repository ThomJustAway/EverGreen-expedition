using Assets.Scripts.Data_manager;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI_related
{
    public class CharacterSelectionPanel : MonoBehaviour
    {
        private PlayerCurrentFernWeaverStats statsSelected;
        [SerializeField] private GameObject submitButton;

        private void Start()
        {

            submitButton.SetActive(false);
        }

        public void SetStat(PlayerCurrentFernWeaverStats stats)
        {
            statsSelected = stats;
            submitButton.SetActive(true);
        }

        public void OnContinue()
        {
            GameManager.Instance.SetPlayerState(statsSelected);
            SceneManager.LoadScene(SceneName.MainLevel);
        }

    }
}