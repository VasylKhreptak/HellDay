using UnityEngine;

public  class UI_StartSlideAnimation : UI_SlideAnimation
{
   [SerializeField] private float _startDelay = 0.5f;
   [SerializeField] private float _endDelay = 1;

   private void OnEnable()
   {
      Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDied);
   }
   
   private void OnDisable()
   {
      Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDied);
   }

   private void Start()
   {
      HideElementBehindScreen();
      
      Animate(AnimationType.show, _duration, _startDelay);
   }

   private void OnPlayerDied()
   {
      Animate(AnimationType.hide, _duration, _endDelay);
   }
}