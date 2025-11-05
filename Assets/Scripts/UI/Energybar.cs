using UnityEngine;
using UnityEngine.UI;

public class Energybar : MonoBehaviour
{
    public Image energybarFill;

    private void Awake()
    {
        UIManager.SetEnergybar(this);
    }

    public void UpdateEnergybarUI(float amount)
    {
        if (energybarFill != null)
        {
            energybarFill.fillAmount = amount;
        }
    }
}
