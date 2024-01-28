using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Assets._Game.Scripts.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] List<ItemPickup> listPrefabs;
        [SerializeField] List<GameObject> listPutPositions;
        private ItemPickup item;
        [SerializeField] float startX, startY;
        [SerializeField] float speedXY;

        private float cooldown = 0;
        private float desiredX, desiredY;

        [SerializeField] bool isActive = true;

        [SerializeField] float timeWaitPerSpawn = 10f;
        [SerializeField] float xOffset = 0.1f;
        [SerializeField] float yOffset = 0.1f;
        [SerializeField] float zOffset = 0.1f;

        private void Start()
        {
            SelectRandomPickup();
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                MoveTowardsDesired();
                if (transform.position.x == desiredX && transform.position.y == desiredY)
                {
                    if (item != null)
                    {
                        ItemInPosition();
                    }
                    else
                    {
                        SpawnerOutside();
                    }
                }
            }
            else
            {
                cooldown -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    SelectRandomPickup();
                }
            }
        }

        private void SelectRandomPickup()
        {
            transform.position = new Vector3(startX, startY, transform.position.z);
            var spawned = Instantiate(listPrefabs[Random.Range(0, listPrefabs.Count - 1)]);
            if (spawned != null)
            {
                item = spawned;
                var pos = listPutPositions[Random.Range(0, listPutPositions.Count - 1)].transform.position;
                SetDesiredXY(pos.x, pos.y);
                item.transform.position = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
                item.transform.SetParent(transform);
                //Debug.Log("Spawned item: " + spawned.name);
            }
            isActive = true;
            cooldown = timeWaitPerSpawn;
        }

        private void MoveTowardsDesired()
        {
            float x = transform.position.x;
            float y = transform.position.y;
            if (x != desiredX)
            {

                float xDir;
                if (x < desiredX)
                {
                    xDir = 1;

                    x += speedXY * xDir * Time.deltaTime;

                    if (x < desiredX)
                    {
                        x = desiredX;
                    }
                }
                else
                {
                    xDir = -1;

                    x += speedXY * xDir * Time.deltaTime;

                    if (x > desiredX)
                    {
                        x = desiredX;
                    }
                }
            }

            if (y != desiredY)
            {
                float yDir;
                if (y < desiredY)
                {
                    yDir = 1;
                    y += speedXY * yDir * Time.deltaTime;

                    if (y > desiredY)
                    {
                        y = desiredY;
                    }
                }
                else
                {
                    yDir = -1;
                    y += speedXY * yDir * Time.deltaTime;

                    if (y < desiredY)
                    {
                        y = desiredY;
                    }
                }
            }


            transform.position = new Vector3(x, y, transform.position.z);
        }

        private void SetDesiredXY(float x, float y)
        {
            desiredX = x;
            desiredY = y;
        }

        private void ItemInPosition()
        {
            item.transform.SetParent(null);
            ItemPickupManager.instance.AddItem(item);
            SetDesiredXY(startX, startY);
            item = null;
        }

        private void SpawnerOutside()
        {
            //Debug.Log("Spawner is back at start");
            SetDesiredXY(startX, startY);
            transform.position = new Vector3(startX, startY, transform.position.z);
            isActive = false;
        }
    }
}