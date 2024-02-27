using Stats.Instances;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class ItemDisplayController : MonoBehaviour
    {
        [SerializeField] private Image _itemIco;
        [SerializeField] private TMP_Text _level;

        private ObjectInstance _instance;

        public ObjectInstance Instance => _instance;

        public void Set(ObjectInstance instance)
        {
            _itemIco.sprite = instance.StatsData.Ico;
            _level.text = $"Level {instance.CurrentLvl}";
            _instance = instance;
        }
        
        public void Setup(Sprite ico, int level)
        {
            _itemIco.sprite = ico;
            _level.text = $"Level {level}";
        }

        public void UpdateLevel(int level)
        {
            _level.text = $"Level {level}";
        }
    }
}