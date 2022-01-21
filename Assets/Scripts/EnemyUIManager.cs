using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// EnemyにHPのUI実装
// ・HPの作成
// ・コードからHPゲージの変更：HPを取得/変更

public class EnemyUIManager : MonoBehaviour
{
    public Slider hpSlider;

    public void Init(EnemyManager enemyManager)
    {
        hpSlider.maxValue = enemyManager.maxHp;
        hpSlider.value = enemyManager.maxHp;
    }

    public void UpdateUI(int hp)
    {
        //hpSlider.value = hp;
        hpSlider.DOValue(hp, 0.3f);
    }
}
