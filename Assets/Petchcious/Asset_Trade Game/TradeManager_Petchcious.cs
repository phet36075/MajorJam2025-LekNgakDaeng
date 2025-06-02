using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Petchcious.Trade_Game
{
    public class TradeManager_Petchcious : MonoBehaviour
    {
        private Dictionary<string, Sprite> spriteDict;
        public string[] itemList; 
        public string currentItem; 

        public SpriteRenderer orderImage;
        public Sprite[] itemSprites;
        public string[] spriteNames;
        public void Start()
        {
            spriteDict = new Dictionary<string, Sprite>();
            for (int i = 0; i < spriteNames.Length; i++)
            {
                spriteDict[spriteNames[i]] = itemSprites[i];
            }
            GenerateNewOrder();
        }

        public void GenerateNewOrder()
        {
            int index = Random.Range(0, itemList.Length);
            currentItem = itemList[index];

            if (spriteDict.TryGetValue(currentItem, out Sprite sprite))
            {
                orderImage.sprite = sprite;
            }
            else
            {
                Debug.LogWarning("Sprite not found for item: " + currentItem);
            }

            Debug.Log("Trade Offer: " + currentItem);
        }
    }
}

