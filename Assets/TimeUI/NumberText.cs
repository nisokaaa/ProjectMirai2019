using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberText : MonoBehaviour
{
    public long num = 1234567890;   //表示したい数字
    public int digit = 16;          //表示する最大桁数
    public bool zeroFill = false;   //0埋めするかどうか
    private List<Image> NumImageList = new List<Image>();
    [SerializeField] private Sprite[] spriteNumbers = new Sprite[10];//実際の画像

    // Update is called once per frame
    void Update()
    {
        if (NumImageList.Count == digit)
        {
            //桁数が揃っているので数値を表示する
            long num2 = num;
            int numDigit = 0;
            if (num2 > 0)
            {
                numDigit = ((int)Mathf.Log10(num2) + 1);
            }
            if (numDigit > digit)
            {
                //数値が桁数を超えている
                for (int i = 0; i < NumImageList.Count; i++)
                {
                    Image numImage = NumImageList.ToArray()[i];
                    if (numImage != null)
                    {
                        numImage.color = Color.white;
                        numImage.sprite = spriteNumbers[spriteNumbers.Length - 1];
                    }
                }
            }
            else
            {
                //数値が桁数を超えていない
                int[] numIndexs = new int[numDigit];
                for (int i = 0; i < numDigit; i++)
                {
                    numIndexs[i] = (int)(num2 % 10);
                    num2 = num2 / 10;
                }
                for (int i = 0; i < NumImageList.Count; i++)
                {
                    Image numImage = NumImageList.ToArray()[i];
                    if (numImage != null)
                    {
                        if (numDigit == 0 && i == 0)
                        {
                            //数値が0だった時の処理（1桁目は必ず0で表示）
                            numImage.color = Color.white;
                            numImage.sprite = spriteNumbers[0];
                        }
                        else if (i < numIndexs.Length)
                        {
                            //数値を反映する
                            numImage.color = Color.white;
                            numImage.sprite = spriteNumbers[numIndexs[i]];
                        }
                        else
                        {
                            if (zeroFill)
                            {
                                //0埋め
                                numImage.color = Color.white;
                                numImage.sprite = spriteNumbers[0];
                            }
                            else
                            {
                                //非表示
                                numImage.color = Color.clear;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (NumImageList.Count < digit)
            {
                //桁数が足りないので増やす
                GameObject numImageObj = new GameObject();
                if (numImageObj != null)
                {
                    numImageObj.name = "NumberImage" + (NumImageList.Count + 1);
                    numImageObj.transform.SetParent(this.transform);
                    RectTransform thisRect = this.GetComponent<RectTransform>();
                    if (thisRect != null)
                    {
                        Image numImage = numImageObj.AddComponent<Image>();
                        if (numImage != null)
                        {
                            numImage.color = Color.clear;
                            RectTransform numImageRect = numImageObj.GetComponent<RectTransform>();
                            if (numImageRect != null)
                            {
                                if (spriteNumbers != null && spriteNumbers.Length > 0)
                                {
                                    numImageRect.sizeDelta = new Vector2(spriteNumbers[0].bounds.size.x * (thisRect.sizeDelta.y / spriteNumbers[0].bounds.size.y), thisRect.sizeDelta.y);
                                    if (NumImageList.Count == 0)
                                    {
                                        numImageObj.transform.localPosition = new Vector3(thisRect.sizeDelta.x / 2 - numImageRect.sizeDelta.x / 2, 0);
                                    }
                                    else
                                    {
                                        Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                                        if (image != null)
                                        {
                                            numImageObj.transform.localPosition = new Vector3(image.transform.localPosition.x - numImageRect.sizeDelta.x, 0);
                                        }
                                    }
                                    NumImageList.Add(numImage);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //桁数が多いので減らす
                Image image = NumImageList.ToArray()[NumImageList.Count - 1];
                if (image != null)
                {
                    NumImageList.RemoveAt(NumImageList.Count - 1);
                    Destroy(image.gameObject);
                }
            }
        }
    }
}