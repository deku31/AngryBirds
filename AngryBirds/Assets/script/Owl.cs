using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    [SerializeField]
    //mengambil objek colider dari burung
    public CircleCollider2D owl;
    //mengatur radius ledakan
    public float radius;
    public float radiusLedakan;
    //waktu kembali ledakan habis
    public float time;
    //kondisi kapan ledakan mereda
    private bool explosion;
    private int _explosion=1;

    //animasi ledakan
    public Animator anim;
   
    //function ledakan
    public void Explosion()
    {
        if (State == BirdState.HitSomething)
        {
            explosion = true;
        }
    }

    private void Update()
    {
        if (_explosion == 1)
        {
            Explosion();
        }
        if (explosion==false)
        {
            if (Collider.radius != radius && time > 0)
            {
                time -= Time.deltaTime;
            }
            else if (Collider.radius != radius && time <= 0)
            {
                //mengembalikan colider owl
                Collider.radius = radius;
            }
            anim.SetBool("ledak", false);
            
        }
        else
        {
            anim.SetBool("ledak", true);
            //efek radius ledakan
            Collider.radius = radiusLedakan;
            _explosion = 0;
            explosion = false;
        }

    }
}
