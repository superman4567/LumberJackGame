using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerCombat))]
    [DisallowMultipleComponent]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private string animatorThrowAxeParameter = "Throw";
        [SerializeField] private string animatorRecallAxeParameter = "Recall";
        private int _animatorThrowAxeHash;
        private int _animatorRecallAxeHash;
        private Animator _animator;
        private PlayerCombat _playerCombat;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animatorThrowAxeHash = Animator.StringToHash(animatorThrowAxeParameter);
            _animatorRecallAxeHash = Animator.StringToHash(animatorRecallAxeParameter);
            _playerCombat = GetComponent<PlayerCombat>();
        }

        private void OnEnable()
        {
            _playerCombat.OnAxeThrown += PlayerCombat_OnAxeThrown;
            _playerCombat.OnAxeRecall += PlayerCombat_OnAxeRecall;
        }

        private void OnDisable()
        {
            _playerCombat.OnAxeThrown -= PlayerCombat_OnAxeThrown;
            _playerCombat.OnAxeRecall -= PlayerCombat_OnAxeRecall;
        }

        private void PlayerCombat_OnAxeThrown()
        {
            _animator.ResetTrigger(_animatorThrowAxeHash);
            _animator.SetTrigger(_animatorThrowAxeHash);
        }

        private void PlayerCombat_OnAxeRecall()
        {
            _animator.ResetTrigger(_animatorRecallAxeHash);
            _animator.SetTrigger(animatorRecallAxeParameter);
        }
    }
}