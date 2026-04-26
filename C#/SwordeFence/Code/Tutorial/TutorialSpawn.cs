using UnityEngine;

namespace SW.Code.Tutorial
{
    public class TutorialSpawn : Spawn
    {
        [SerializeField] private Vector2 startPoint;
        [SerializeField] private Enemy tutorialNomalSkeleton;
        private void Start()
        {

        }
        private void Update()
        {
            
        }


        public override  void StartSpawn()
        {
            Enemy enemy = Instantiate(tutorialNomalSkeleton);
            enemy.transform.position = startPoint;
        }
    }
}