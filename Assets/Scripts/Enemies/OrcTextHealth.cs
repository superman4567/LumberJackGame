using System.Globalization;
using TMPro;
using UnityEngine;

// @Deprecated
public class OrcTextHealth : MonoBehaviour
{
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 1);
    private TextMeshPro _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        Destroy(gameObject, destroyTime);
    }

    public void Setup(float damage)
    {
        _textMeshPro.SetText(damage.ToString(CultureInfo.CurrentCulture));
    }
}
