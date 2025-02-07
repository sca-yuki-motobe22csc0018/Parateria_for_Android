using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class CharaSpineMove : MonoBehaviour
{
	/// <summary> 再生するアニメーション名 </summary>
	[SerializeField]
	private string charaRunAnimation;

	[SerializeField]
	private string charaJumpAnimation;

	[SerializeField]
	private string charaDieAnimation;

	private int timer=0;
	bool set;

	enum State
	{
		run=1, jump=2,die=3,doubleJump=4
	}
	State state;
	

	private SkeletonAnimation  _skeletonAnimation;

	/// <summary> ゲームオブジェクトに設定されているSkeletonAnimation </summary>
	private SkeletonAnimation skeletonAnimation = default;

	/// <summary> Spineアニメーションを適用するために必要なAnimationState </summary>
	private Spine.AnimationState spineAnimationState = default;

	private void Start()
	{
		// ゲームオブジェクトのSkeletonAnimationを取得
		skeletonAnimation = GetComponent<SkeletonAnimation>();

		// SkeletonAnimationからAnimationStateを取得
		spineAnimationState = skeletonAnimation.AnimationState;

		_skeletonAnimation = GetComponent<SkeletonAnimation>();
	}

	private void Update()
	{
        if (PlayerController.st == 1&&state!=State.run)
        {
            PlayRunAnimation();
			state = State.run;
        }
        if (PlayerController.st == 2 && state != State.jump)
        {
            PlayJumpAnimation();
			state = State.jump;
        }
        if (PlayerController.st == 3 && state != State.die)
        {
            PlayDieAnimation();
				state = State.die;
        }
		if (Input.GetKeyDown(KeyCode.Space))
		{
            state = State.doubleJump;
        }
	}

	/// <summary>
	/// Spineアニメーションを再生
	/// testAnimationNameに再生したいアニメーション名を記載してください。
	/// </summary>
	private void PlayRunAnimation()
	{
		spineAnimationState.SetAnimation(0, charaRunAnimation, true);
	}

	private void PlayJumpAnimation()
	{
		spineAnimationState.SetAnimation(0, charaJumpAnimation, false);
	}

	private void PlayDieAnimation()
	{
		spineAnimationState.SetAnimation(0, charaDieAnimation, false);
	}
}
