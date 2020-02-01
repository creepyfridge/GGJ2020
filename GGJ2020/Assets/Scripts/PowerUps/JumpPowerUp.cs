using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : PowerUpBase
{
    public override void addPowerUp(PlayerPowers player)
    {
        player.addSpring();
        Destroy(this.gameObject);

    }
}
