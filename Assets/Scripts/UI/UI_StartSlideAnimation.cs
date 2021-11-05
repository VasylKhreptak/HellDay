using DG.Tweening;
using UnityEngine;

public class UI_StartSlideAnimation : UI_SlideAnimation
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

   protected override void Start()
   {
      HideBehindScreen();

      this.DOWait(_startDelay).OnComplete(() => { SetAnimationState(true); });
   }

   private void OnPlayerDied()
   {
      this.DOWait(_endDelay).OnComplete(() => { SetAnimationState(false); });
   }
}