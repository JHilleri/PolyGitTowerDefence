using UnityEngine;
using System.Collections;

public class ColoredProjectile : MonoBehaviour
{
    public void updateColor()
    {
        var projectile = GetComponent<Projectile>();
        if(projectile == null) throw new MissingComponentException("Projectiles component missing");
        var spriteRender = GetComponent<SpriteRenderer>();
        if(spriteRender == null) throw new MissingComponentException("SpriteRenderer component missing");
        spriteRender.color = projectile.element.couleur;
    }
}
