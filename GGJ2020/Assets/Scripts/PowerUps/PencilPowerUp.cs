using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilPowerUp : PowerUpBase
{
    public override void addPowerUp(PlayerPowers player)
    {
        player.addPencil();
        Destroy(this.gameObject);

    }
}
