using UnityEngine;

namespace Sabanishi.ActionTutorial
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D myCollider;
        [SerializeField] private Transform[] blocks;
        private Vector2 _velocityCache;
        private Vector3 _positionCache;

        private const float Speed = 50f;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _velocityCache = new Vector2(0, -9.8f);
            _rb = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            var velocity = InputKey();
            _rb.velocity = velocity;
            //transform.position += (Vector3)(velocity * Time.fixedDeltaTime);
            //Collision();
            //_velocityCache = velocity;
            //_positionCache = transform.position;
        }

        public void Collision()
        {
            //ブロックと衝突していた場合、プレイヤーを押し戻す
            var size = myCollider.size;

            foreach (var block in blocks)
            {
                if (block == null) continue;
                var blockPos = block.position;
                var blockSize = block.GetComponent<BoxCollider2D>().size;
                
                //左右方向
                if (transform.position.x + size.x / 2 > blockPos.x - blockSize.x / 2 &&
                    transform.position.x - size.x / 2 < blockPos.x + blockSize.x / 2 &&
                    transform.position.y + size.y / 2 > blockPos.y - blockSize.y / 2 &&
                    transform.position.y - size.y / 2 < blockPos.y + blockSize.y / 2)
                {
                    if(_positionCache.x - transform.position.x > 0)
                    {
                        transform.position = new Vector3(blockPos.x - blockSize.x / 2 - size.x / 2, transform.position.y,
                            transform.position.z);
                    }
                    else if(_positionCache.x - transform.position.x < 0)
                    {
                        transform.position = new Vector3(blockPos.x + blockSize.x / 2 + size.x / 2, transform.position.y,
                            transform.position.z);
                    }
                }
                
                //上下方向
                if (transform.position.x + size.x / 2 > blockPos.x - blockSize.x / 2 &&
                    transform.position.x - size.x / 2 < blockPos.x + blockSize.x / 2 &&
                    transform.position.y + size.y / 2 > blockPos.y - blockSize.y / 2 &&
                    transform.position.y - size.y / 2 < blockPos.y + blockSize.y / 2)
                {
                    if(_positionCache.y - transform.position.y < 0)
                    {
                        transform.position = new Vector3(transform.position.x, blockPos.y - blockSize.y / 2 - size.y / 2,
                            transform.position.z);
                    }
                    else if(_positionCache.y - transform.position.y > 0)
                    {
                        transform.position = new Vector3(transform.position.x, blockPos.y + blockSize.y / 2 + size.y / 2,
                            transform.position.z);
                    }
                }
            }
        }

        private Vector2 InputKey()
        {
            if (Input.GetButton("Right"))
            {
                return new Vector2(Speed, _rb.velocity.y);
            }
            else if (Input.GetButton("Left"))
            {
                return new Vector2(-Speed, _rb.velocity.y);
            }
            else
            {
                return new Vector2(0, _rb.velocity.y);
            }
        }
    }
}