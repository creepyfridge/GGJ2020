using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazorPowerUp : PowerUpBase
{
    public override void addPowerUp(PlayerPowers player)
    {
        player.addRazor();
        Destroy(this.gameObject);

    }
}
