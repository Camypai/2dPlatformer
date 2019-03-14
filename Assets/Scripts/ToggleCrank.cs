using UnityEngine;

namespace DefaultNamespace
{
    public class ToggleCrank : MonoBehaviour
    {
        public bool toggle = false;
        [SerializeField] private GameObject column;
        [SerializeField] private Sprite crankUp;
        [SerializeField] private Sprite crankDown;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (toggle)
            {
                var columnScript = column.GetComponent<LiftMovement>();
                columnScript.move = true;
                Toggle();
                toggle = !toggle;
            }
        }

        private void Toggle()
        {
            _spriteRenderer.sprite = _spriteRenderer.sprite == crankUp ? crankDown : crankUp;
        }
    }
}