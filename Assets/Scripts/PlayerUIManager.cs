using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// PlayerにHPのUI実装
// ・HPの作成
// ・コードからHPゲージの変更：HPを取得/変更

public class PlayerUIManager : MonoBehaviour
{
    public Slider hpSlider;
    public Slider staminaSlider;

    public void Init(PlayerManager playerManager)
    {
        hpSlider.maxValue = playerManager.maxHp;
        hpSlider.value = playerManager.maxHp;
        staminaSlider.maxValue = playerManager.maxStanima;
        staminaSlider.value = playerManager.maxStanima;
    }

    public void UpdateHP(int hp)
    {
        hpSlider.DOValue(hp, 0.5f);
    }

    public void UpdateStamina(int stamina)
    {
        staminaSlider.DOValue(stamina, 0.5f);
    }
}
