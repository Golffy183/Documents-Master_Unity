using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDev3.Project
{
    public class DeviceStart : MonoBehaviour, IInteractable, IActorEnterExitHandler
    {
        [Header("Return")]
        public string DeviceID;
        [Header("Config")]
        public float SpeedDevice = 4f;
        [Header("----------------------------")]
        public TimeCount timeCount;
        private bool Working = false;
        private bool isFinished = false;
        public GameObject image;

        void Start()
        {
            timeCount.maxTime = SpeedDevice;
        }

        public void Interact(GameObject actor)
        {
            var timeSystem = this.gameObject.GetComponent<TimeSystem>();
            var inventory = actor.gameObject.GetComponent<Inventory>();
            if (inventory.m_ItemInventory.Count == 0)
            {
                if (Working == false)
                {
                    timeSystem.Working = true;
                    FindObjectOfType<AudioManager>().Play("RecieveDevice");
                    image.SetActive(false);
                    Working = true;
                    Invoke("DeviceFinished", SpeedDevice);
                }

                if (isFinished)
                {
                    inventory.AddItem(DeviceID);
                    FindObjectOfType<AudioManager>().Play("PickUpItem");
                    image.SetActive(true);
                    timeSystem.Working = false;
                    isFinished = false;
                    Working = false;
                }
            }
            else
            {
                inventory.m_ItemInventory.Clear();
                inventory.AddItem("DestroyItem");
            }
        }

        public void ActorEnter(GameObject actor)
        {
           
        }

        public void ActorExit(GameObject actor)
        {
           
        }

        void DeviceFinished()
        {
            isFinished = true;
        }
    }
}