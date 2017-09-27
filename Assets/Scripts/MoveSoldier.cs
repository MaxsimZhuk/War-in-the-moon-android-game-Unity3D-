using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class MoveSoldier : MonoBehaviour
    {

        public GameObject Soldier_Waite;
        public GameObject Soldier_Go_Right;
        public GameObject Soldier_Go_Left;
        public GameObject Soldier_Shoot_Right;
        public GameObject Soldier_Shoot_Left;
        private GameObject Soldier;
        public GameObject Healplus;
        public GameObject Healminus;
        private float Healse;
       
        enum Direction
        {
            Left,
            Right,
            Waite,
            ShootRight,
            ShootLeft
        }

        private Direction TargetDirection;
        public GameObject Target;
        public GameObject EnemyTarget;
        // Use this for initialization
        void Start ()
        {
            Healse = 40;
           
            Healplus.SetActive(false);
          
            
            Soldier = Instantiate(Soldier_Waite, transform.position, transform.rotation);
            Soldier.transform.SetParent(transform);
            TargetDirection = Direction.Left;
            
            Healplus = Instantiate(Healplus, transform.position , transform.rotation);
            Healplus.transform.SetParent(transform);
            
            Healplus.transform.position = transform.position + Vector3.up * (float) 0.5;
            if (gameObject.CompareTag("Team2"))
            {
                Healplus.SetActive(true);
            }


            /*  Healminus = Instantiate(Healminus, transform.position, transform.rotation);
            Healminus.transform.SetParent(transform);
            Healminus.transform.position = Vector3.up * (float)0.5;*/

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Target == null && EnemyTarget == null)
            {
                if (TargetDirection != Direction.Waite)
                {
                    Destroy(Soldier);
                    Soldier = Instantiate(Soldier_Waite, transform.position, transform.rotation);
                    Soldier.transform.SetParent(transform);
                    TargetDirection = Direction.Waite;

                }
            }
            if (Target == null)
            {
                if (EnemyTarget == null)
                {

                    RaycastHit info;
                    for (float i = 0; i <= 2; i = (float) (i + 0.1))
                    {
                        if (Physics.Raycast(transform.position, new Vector3(1, 1 - i), out info, 5f))
                        {
                            if (!info.collider.gameObject.CompareTag(gameObject.tag))
                            {
                                EnemyTarget = info.collider.gameObject;
                            }
                        }
                        if (Physics.Raycast(transform.position, new Vector3(1 - i, -1), out info, 5f))
                        {
                            if (!info.collider.gameObject.CompareTag(gameObject.tag))
                            {
                                EnemyTarget = info.collider.gameObject;
                            }
                        }
                        if (Physics.Raycast(transform.position, new Vector3(-1, -1 + i), out info, 5f))
                        {
                            if (!info.collider.gameObject.CompareTag(gameObject.tag))
                            {
                                EnemyTarget = info.collider.gameObject;
                            }
                        }
                        if (Physics.Raycast(transform.position, new Vector3(-1 + i, 1), out info, 5f))
                        {
                            if (!info.collider.gameObject.CompareTag(gameObject.tag))
                            {
                                EnemyTarget = info.collider.gameObject;
                            }
                        }
                    }

                }
                else
                {
                    if (Vector3.Distance(EnemyTarget.transform.position, gameObject.transform.position) < 3)
                    {
                        EnemyTarget.BroadcastMessage("MyAmShootingForYou");
                        if (transform.position.x < EnemyTarget.transform.position.x)
                        {
                            if (TargetDirection != Direction.ShootLeft)
                            {
                                Destroy(Soldier);
                                Soldier = Instantiate(Soldier_Shoot_Left, transform.position, transform.rotation);
                                Soldier.transform.SetParent(transform);
                                TargetDirection = Direction.ShootLeft;
                            }
                        }


                        if (transform.position.x > EnemyTarget.transform.position.x)
                        {
                            if (TargetDirection != Direction.ShootRight)
                            {
                                Destroy(Soldier);
                                Soldier = Instantiate(Soldier_Shoot_Right, transform.position, transform.rotation);
                                Soldier.transform.SetParent(transform);
                                TargetDirection = Direction.ShootRight;
                            }
                        }

                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, EnemyTarget.transform.position,
                            (float) 0.009);
                        

                        if (transform.position.x < EnemyTarget.transform.position.x)
                        {
                            if (TargetDirection != Direction.Left)
                            {
                                Destroy(Soldier);
                                Soldier = Instantiate(Soldier_Go_Left, transform.position, transform.rotation);
                                Soldier.transform.SetParent(transform);
                                TargetDirection = Direction.Left;
                            }

                        }
                        if (transform.position.x > EnemyTarget.transform.position.x)
                        {
                            if (TargetDirection != Direction.Right)
                            {
                                Destroy(Soldier);
                                Soldier = Instantiate(Soldier_Go_Right, transform.position, transform.rotation);
                                Soldier.transform.SetParent(transform);
                                TargetDirection = Direction.Right;
                            }

                        }
                    }
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, (float) 0.01);
                if (transform.position.x < Target.transform.position.x)
                {

                    if (TargetDirection != Direction.Left)
                    {
                        Destroy(Soldier);
                        Soldier = Instantiate(Soldier_Go_Left, transform.position, transform.rotation);
                        Soldier.transform.SetParent(transform);
                        TargetDirection = Direction.Left;
                    }
                }
                if (transform.position.x > Target.transform.position.x)
                {
                    if (TargetDirection != Direction.Right)
                    {
                        Destroy(Soldier);
                        Soldier = Instantiate(Soldier_Go_Right, transform.position, transform.rotation);
                        Soldier.transform.SetParent(transform);
                        TargetDirection = Direction.Right;
                    }
                }
             
                if (Target.transform.position == transform.position)
                {
                    Destroy(Target);
                }
            }
        }



        public void MyActive()
        {
            Healplus.SetActive(true); 
        }

        public void MyUnActive()
        {
            Healplus.SetActive(false);
        }

        public void getTarget(GameObject T)
        {
            Destroy(Target);
            Target = new GameObject();
            Target.transform.position = T.transform.position;
        }

        public void MyAmShootingForYou()
        {
            Healse = (float) (Healse-0.1);
            Healplus.transform.localScale -= new Vector3(0.001f, 0.0f, 0.0f);
            Healplus.transform.position += Vector3.left * (float) 0.0005;
            if (Healse < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
