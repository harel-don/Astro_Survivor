using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimationScript : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField]private ScriptForPlayer m_ScriptForPlayer;
    [SerializeField]private PlayerControler m_Controler;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        animator.SetInteger("direction", m_Controler.GetDirect());
        if (m_ScriptForPlayer.getMove())
        {
            animator.SetTrigger("walk");
        }
    }
}
