using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// Enemy��HP��UI����
// �EHP�̍쐬
// �E�R�[�h����HP�Q�[�W�̕ύX�FHP���擾/�ύX

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
