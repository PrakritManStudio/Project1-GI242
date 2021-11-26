using UnityEngine;

namespace Spaceship
{
    public abstract class Basespaceship : MonoBehaviour
    {
        [SerializeField] protected Bullet defaultBullet;
        [SerializeField] protected Transform gunPosition;

        protected AudioSource audioSource;
        
        public int Hp { get; protected set; }
        public float Speed { get; private set; }
        public Bullet Bullet { get; private set; }

        protected void Init(int hp, float speed, Bullet bullet)
        {
            Hp = hp;
            Speed = speed;
            Bullet = bullet;
        }

        public abstract void Fire();
    }
}