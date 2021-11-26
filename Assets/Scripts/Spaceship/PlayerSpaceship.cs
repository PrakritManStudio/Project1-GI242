using System;
using Manager;
using UnityEngine;

namespace Spaceship
{
    public class PlayerSpaceship : Basespaceship, IDamagable
    {
        // [SerializeField] private AudioClip playerFireSound;
        // [SerializeField] private float playerFireSoundVol = 0.4f;
        // [SerializeField] private AudioClip playerExplodeSound;
        // [SerializeField] private float playerExplodeSoundVol = 0.4f;
        
        public event Action OnExploded;

        private void Awake()
        {
            Debug.Assert(defaultBullet != null, "defaultBullet cannot be null");
            Debug.Assert(gunPosition != null, "gunPosition cannot be null");
            //Debug.Assert(playerFireSound != null, "playerFireSound cannot be null");

            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            UIManager.HPPlayer = Hp;
        }

        public void Init(int hp, float speed)
        {
            base.Init(hp, speed, defaultBullet);
        }

        public override void Fire()
        {
            var bullet = Instantiate(defaultBullet, gunPosition.position, Quaternion.identity);
            bullet.Init(Vector2.up);
            
            //AudioSource.PlayClipAtPoint(playerFireSound, Camera.main.transform.position, playerFireSoundVol);
            //SoundManager.Instance.Play(audioSource, SoundManager.Sound.PlayerFire);
            SoundManager.Instance.PlayerFireSound();
        }

        public void TakeHit(int damage)
        {
            Hp -= damage;
            UIManager.HPPlayer = Hp;
            if (Hp > 0)
            {
                return;
            }
            Explode();
        }

        public void Explode()
        {
            Debug.Assert(Hp <= 0, "HP is more than zero");
            Destroy(gameObject);
            OnExploded?.Invoke();
            
            //AudioSource.PlayClipAtPoint(playerExplodeSound, Camera.main.transform.position, playerExplodeSoundVol);
            //SoundManager.Instance.Play(audioSource, SoundManager.Sound.PlayerExplode);
            SoundManager.Instance.PlayerExplodeSound();
        }
    }
}