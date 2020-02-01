using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsPowerUp : PowerUpBase
{
    public override void addPowerUp(PlayerPowers player)
    {
        player.addWheels();
        Destroy(this.gameObject);

    }
}
