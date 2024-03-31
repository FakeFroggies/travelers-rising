using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    public int Health {get; set;}
    public void TakeDamage(int value);
    public void Die();    
}
