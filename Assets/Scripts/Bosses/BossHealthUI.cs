using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public Image healthFill;

    public void SetHealth(float current, float max)
    {
        healthFill.fillAmount = current / max;
    }
}
