using UnityEngine;

public  class UI_SlideAnimation : UI_MovementAnimation
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
      HideBehindScreen();
      
      StartAnimation(AnimationType.appear, _duration, _startDelay);
   }

   private void HideBehindScreen()
   {
      _rectTransform.anchoredPosition = _startAnchoredPosition;
   }
   
   private void OnPlayerDied()
   {
      StartAnimation(AnimationType.disappear, _duration, _endDelay);
   }
}