using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCurio.AlteredTimeline
{
    public class StatusView : MonoBehaviour
    {
        [SerializeField] private Image _leftCursor;
        [SerializeField] private Image _rightCursor;
        [SerializeField] private TextMeshProUGUI _leftName;
        [SerializeField] private TextMeshProUGUI _rightName;
        [SerializeField] private Image _leftSeparator;
        [SerializeField] private Image _rightSeparator;
        [SerializeField] private Image _healthImage;
        [SerializeField] private Image _manaImage;

        private Alignment _panelAlignment;
        public Alignment PanelAlignment
        {
            get => _panelAlignment;

            set
            {
                switch (value)
                {
                    case Alignment.Left:
                        _leftName.gameObject.SetActive(false);
                        _rightName.gameObject.SetActive(true);
                        _leftCursor.gameObject.SetActive(false);
                        _rightCursor.gameObject.SetActive(true);
                        _leftSeparator.gameObject.SetActive(false);
                        _rightSeparator.gameObject.SetActive(true);
                        break;
                    case Alignment.Right:
                        _leftName.gameObject.SetActive(true);
                        _rightName.gameObject.SetActive(false);
                        _leftCursor.gameObject.SetActive(true);
                        _rightCursor.gameObject.SetActive(false);
                        _leftSeparator.gameObject.SetActive(true);
                        _rightSeparator.gameObject.SetActive(false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }

                _panelAlignment = value;
            }
        }

        public void Awake()
        {
            PanelAlignment = Alignment.Right;
            SetCursorVisibility(false);
        }

        public void SetName(string nameText)
        {
            _leftName.text = nameText;
            _rightName.text = nameText;
        }

        public void SetCursorVisibility(bool isVisible)
        {
            _rightCursor.enabled = isVisible;
            _leftCursor.enabled = isVisible;
        }

        public ICharacter Character { get; set; }

        public enum Alignment { Right, Left }

        public void HealthChange(float value) => _healthImage.fillAmount = value;

        public void ManaChange(float value) => _manaImage.fillAmount = value;
    }
}
