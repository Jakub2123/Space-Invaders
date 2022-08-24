using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    public DamagableType type;
    public DmgEffect dmgeffect;
    public Color effectColor;
    public virtual void GetDamage()
    {
        var effect = Instantiate(dmgeffect);
        effect.Setup(transform.position, effectColor);
        Destroy(gameObject);

    }
}

public enum DamagableType
{
    Invader,
    Player,
    Building

}