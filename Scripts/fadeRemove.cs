using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeRemove : StateMachineBehaviour
{
        SpriteRenderer sprite;
        GameObject objToRemove;

        [SerializeField] private float fadeTime = 0.5f;
        private float timeElapsed = 0f;

        Color startColor;

        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
                timeElapsed = 0f;
                sprite = animator.GetComponent<SpriteRenderer>();
                startColor = sprite.color;
                objToRemove = animator.gameObject;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
                timeElapsed += Time.deltaTime;

                float newAlpha = startColor.a * (1 - (timeElapsed / fadeTime));

                sprite.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

                if (timeElapsed > fadeTime)
                {
                        Destroy(objToRemove);
                }

        }
}
