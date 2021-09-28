using System.Collections;
using UnityEngine;

public class UI_SlideAnimation : UI_MovementAnimation
{
   [SerializeField] private float _startDelay = 0.5f;

   private void Awake()
   {
      Messenger.AddListener(GameEvent.PLAYER_DIED, OnPlayerDied);

      _targetAnchoredPosition = _rectTransform.anchoredPosition;
   }
   
   private void OnDestroy()
   {
      Messenger.RemoveListener(GameEvent.PLAYER_DIED, OnPlayerDied);
   }

   private IEnumerator Start()
   {
      HideBehindScreen();
      
      yield return new WaitForSeconds(_startDelay);
      
      StartAnimation(AnimationType.appear, _duration);
   }

   private void HideBehindScreen()
   {
      _rectTransform.position = _startTransform.position;
   }
   
   private void OnPlayerDied()
   {
      StartAnimation(AnimationType.disappear, _duration);
   }
}