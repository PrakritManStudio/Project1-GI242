using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
    public class EnemySpaceship : Basespaceship, IDamagable
    {
        // [SerializeField] private AudioClip enemyFireSound;
        // [SerializeField] private float enemyFireSoundVol = 0.4f;
        // [SerializeField] private AudioClip enemyExplodeSound;
        // [SerializeField] private float enemyExplodeSoundVol = 0.4f;
        
        //public static int HPEnemy;
        
        //[SerializeField] public double enemyFireRate = 1;
        //private double enemyFireRate = GameManager.Instance.enemyFireRate;
        
        private float fireCounter = 0;

        public event Action OnExploded;
        public event Action OnTakeHit;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            UIManager.HPEnemy = Hp;
        }

        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet);
        }
        
        public void TakeHit(int damage)
        {
            Hp -= damage;
            UIManager.HPEnemy = Hp;
            ScoreManager.Instance.playerScore += 1;
            if (Hp > 0)
            {
                return;
            }
            
            Explode();
            
        }

        public void Explode()
        {
            //Debug.Assert(Hp <= 0, "HP is more than zero");
            Debug.Assert(Hp <= 0, "HP is more than zero");

            //SoundManager.Instance.Play(audioSource, SoundManager.Sound.EnemyExplode);
            SoundManager.Instance.EnemyExplodeSound();
                
            gameObject.SetActive(false);
            Destroy(gameObject);
            OnExploded?.Invoke();
            
            //AudioSource.PlayClipAtPoint(enemyExplodeSound, Camera.main.transform.position, enemyExplodeSoundVol);
        }

        public override void Fire()
        {
            // TODO: Implement this later

            fireCounter += Time.deltaTime;
            if (fireCounter >= GameManager.Instance.enemyFireRate)
            {
                
                //SoundManager.Instance.Play(audioSource, SoundManager.Sound.EnemyFire);
                SoundManager.Instance.EnemyFireSound();
                
                var bullet = Instantiate(defaultBullet, gunPosition.position, Quaternion.identity);
                bullet.Init(Vector2.down);
                fireCounter = 0;

                
                
                //AudioSource.PlayClipAtPoint(enemyFireSound, Camera.main.transform.position, enemyFireSoundVol);
            }
            
        }
    }
}