using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

interface IWeapons
{
    Sprite Icon { get; set; }
    void  DisableEffects();
   
}


public class WeaponSwitch : MonoBehaviour {

    // 存放玩家選擇的武器腳本
    private MonoBehaviour[] weaponSlot = new MonoBehaviour[3];

    // 存放轉型為介面的武器腳本 用來存取一些方法
    private IWeapons[] iweapons = new IWeapons[3];

    // 現在選擇的武器欄位 0~2共三把
    private int nowWeapon;

    // 武器欄位UI以及其中的Animator的參考
    public GameObject slotUI;
    private Animator slotAni;

    // 武器欄位1~3的圖片
    public Image slot01, slot02, slot03;
    void Start () {
        slotAni = slotUI.GetComponent<Animator>();
        nowWeapon = 0;
        slotAni.SetBool("ChooseFirstWeapon", true);

        // 設定初始的三把武器
        weaponSlot[0] = GetComponent<PlayerShooting>();
        weaponSlot[1] = GetComponent<Laser>();
        weaponSlot[2] = GetComponent<NoWeapon>();

        // 設定初始的三把武器的介面
        iweapons[0] = (IWeapons)weaponSlot[0];
        iweapons[1] = (IWeapons)weaponSlot[1];
        iweapons[2] = (IWeapons)weaponSlot[2];

        // 經由介面取得每把武器的圖示
        slot01.sprite = iweapons[0].Icon;
        slot02.sprite = iweapons[1].Icon;
        slot03.sprite = iweapons[2].Icon;

      
    }
	
	
	void Update () {

        // 數字鍵1~3切換手上的武器
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nowWeapon = 0;

            // 啟動武器欄位的小動畫
            slotAni.SetBool("ChooseFirstWeapon", true);
            slotAni.SetBool("ChooseSecondWeapon", false);
            slotAni.SetBool("ChooseThirdWeapon", false);

            // 切換武器時同時刪除其他武器的效果
            iweapons[1].DisableEffects();
            iweapons[2].DisableEffects();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nowWeapon = 1;
            slotAni.SetBool("ChooseFirstWeapon", false);
            slotAni.SetBool("ChooseSecondWeapon", true);
            slotAni.SetBool("ChooseThirdWeapon", false);

            iweapons[0].DisableEffects();
            iweapons[2].DisableEffects();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nowWeapon = 2;
            slotAni.SetBool("ChooseFirstWeapon", false);
            slotAni.SetBool("ChooseSecondWeapon", false);
            slotAni.SetBool("ChooseThirdWeapon", true);


            iweapons[0].DisableEffects();
            iweapons[1].DisableEffects();
        }

        // 選取左下欄位的武器
        switch (nowWeapon)
        {
            case 0:
                {
                    weaponSlot[0].enabled = true;
                    weaponSlot[1].enabled = false;
                    weaponSlot[2].enabled = false;


                    break;
                }
            case 1:
                {
                    weaponSlot[0].enabled = false;
                    weaponSlot[1].enabled = true;
                    weaponSlot[2].enabled = false;


                    break;
                }
            case 2:
                {
                    weaponSlot[0].enabled = false;
                    weaponSlot[1].enabled = false;
                    weaponSlot[2].enabled = true;

                    break;
                }

            default:
                {
                    break;
                }
        }
	}


    // 武器選擇視窗要呼叫此方法更換武器
    public void SetWeapons(MonoBehaviour firstWeapon, MonoBehaviour secondWeapon, MonoBehaviour thirdWeapon)
    {
        weaponSlot[0] = firstWeapon;
        weaponSlot[1] = secondWeapon;
        weaponSlot[2] = thirdWeapon;

        // 幹可以這樣轉喔 類別轉為介面
        iweapons[0] = (IWeapons)firstWeapon;
        iweapons[1] = (IWeapons)secondWeapon;
        iweapons[2] = (IWeapons)thirdWeapon;
    }
}
