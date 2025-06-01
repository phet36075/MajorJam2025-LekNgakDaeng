using UnityEngine;
using UnityEngine.UI;
namespace Petchcious.Trade_Game
{
    public class TradeManager_Petchcious : MonoBehaviour
    {
        public string[] itemList; 
        public string currentItem; 

        public SpriteRenderer orderImage;
        public Sprite[] itemSprites;
        public string[] spriteNames;
        public void Start()
        {
            GenerateNewOrder();
        }

        public void GenerateNewOrder()
        {
            int index = Random.Range(0, itemList.Length);
            currentItem = itemList[index];
            for (int i = 0; i < spriteNames.Length; i++)
            {
                if (spriteNames[i] == currentItem)
                {
                    orderImage.sprite = itemSprites[i];
                    break;
                }
            }
            Debug.Log("Trade Offer: " + currentItem);
        }
    }
}

