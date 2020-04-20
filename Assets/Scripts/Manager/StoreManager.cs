using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public Button[] button;
    public Sprite[] button_sprite;
    private int[] item_price = { 200, 100, 100 };
    public Text coin_text;
    public Text[] item_price_text;
    // Start is called before the first frame update
    void Start()
    {
        if (!(PlayerPrefs.HasKey("is_init")))
        {
            PlayerPrefs.SetInt("totalPoint", 0);
            PlayerPrefs.SetInt("is_init", 1);
            buy_Item(0);
        }
        for(int i = 0; i < item_price.Length; i++)
            item_price_text[i].text = item_price[i].ToString() + " P";
    }

    public void change_Button(int num)
    {
        if (PlayerPrefs.HasKey("button" + num.ToString()))
            equip_Item(num);
        else
            buy_Item(num);
    }

    private void buy_Item(int num)
    {
        if (PlayerPrefs.HasKey("totalPoint") && PlayerPrefs.GetInt("totalPoint") >= item_price[num])
        {
            PlayerPrefs.SetInt("totalPoint", PlayerPrefs.GetInt("totalPoint") - item_price[num]);
            equip_Item(num);
        }
        else
        {
            Debug.Log("코인이 부족합니다");
        }
    }

    public void equip_Item(int num)
    {
        for(int i = 0; i < button.Length; i++)
            if(PlayerPrefs.HasKey("button" + i.ToString()))
                PlayerPrefs.SetInt("button" + i.ToString(), -1);
        button[num].GetComponent<Image>().sprite = button_sprite[2];
        PlayerPrefs.SetInt("button" + num.ToString(), 1);
        PlayerPrefs.SetInt("player_sprite", num);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < button.Length; i++)
            if (PlayerPrefs.HasKey("button" + i.ToString()))
                button[i].GetComponent<Image>().sprite = button_sprite[1 + PlayerPrefs.GetInt("button" + i.ToString())];
            else
                button[i].GetComponent<Image>().sprite = button_sprite[1];
        if (PlayerPrefs.HasKey("totalPoint"))
            coin_text.text = "Point : " + PlayerPrefs.GetInt("totalPoint").ToString();
        else
            coin_text.text = "Point : " + "000";
    }
}
