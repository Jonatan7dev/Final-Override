using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator an;
    PlayerMovment pm;
    SpriteRenderer sr;

    void Start()
    {
        an = GetComponent<Animator>();
        pm = GetComponent<PlayerMovment>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        an.SetBool("Move", pm.moveDir != Vector2.zero);

        // Solo cambia la dirección si se mueve horizontalmente
        if (pm.moveDir.x > 0)
        {
            sr.flipX = false;
        }
        else if (pm.moveDir.x < 0)
        {
            sr.flipX = true;
        }
    }
}
