using System.Collections.Generic;
using UnityEngine;

public class Move : Action {
    private static string ACTION_NAME = "Move";
    private static int COOLDOWN_PER_UNIT = 100;

    public Move(Unit actor) {
        this.actor = actor;
    }

    public override int Perform() {
        if (isReady()) {
            int cost = GetCost();
            return cost;
        }
        else {
            throw new System.Exception();
        }
    }

    public override int GetCost() {
        if (isReady()) {
            Debug.Log(Coord.Distance(actor.position, this.position));
            return (int) (Coord.Distance(actor.position, this.position) * COOLDOWN_PER_UNIT);
        }
        else {
            throw new System.Exception();
        }
    }

    public List<Coord> ValidLocations() {
        return null;
    }

    protected override bool isReady() {
        return base.isReady() && this.position != null;
    }
}