using TMPro;
using UnityEngine;

namespace UI.FloatingText
{
    public class FloatingText : MonoBehaviour
    {
        private TextMeshPro _textMeshPro;

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
        }

        public void SetText(string text)
        {
            _textMeshPro.SetText(text);
        }
    }
}