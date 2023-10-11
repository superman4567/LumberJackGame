using UnityEngine;

namespace Helpers
{
    public static class StaticHelpers
    {
        private const string TotemTag = "Totem";

        public static bool IsPlayerLayer(Collider col)
        {
            return (GameManager.PLAYER_LAYER & 1 << col.gameObject.layer) != 0;
        }

        public static bool IsTotemTag(Collider col)
        {
            return col.CompareTag(TotemTag);
        }
    }
}