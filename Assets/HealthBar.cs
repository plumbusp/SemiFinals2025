using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Colorful Time Slider")]
    [SerializeField] Slider _slider;
    [SerializeField] float _maxHealth;

    [SerializeField] private Image _fillImage;
    [SerializeField] private Color color1, color2;
    private WaitForSeconds waitUntilEnd;
    private float currentTime;
    private float t_forLerp;

    private void Start()
    {
        waitUntilEnd = new WaitForSeconds(_maxHealth);

        _slider.maxValue = _maxHealth;
        _slider.value = 0;
    }
    void Update()
    {
        currentTime += Time.deltaTime;
        t_forLerp = currentTime / _maxHealth;
        _slider.value = Mathf.Lerp(0, 1, t_forLerp) * _maxHealth;
        _fillImage.color = Color.Lerp(color1, color2, t_forLerp);
    }

    public void Decrease(float minusValue)
    {
        _slider.value = _slider.value - minusValue;

        float t_forLerp = _slider.value / _slider.maxValue;

        _fillImage.color = Color.Lerp(color1, color2, t_forLerp);
    }
}
