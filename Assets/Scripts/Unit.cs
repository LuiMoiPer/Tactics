using System.Collections.Generic;
public class Unit {
    Coord _position;
    Coord[] neighborDeltas = new Coord[]{ 
        new Coord(-1, -1),
        new Coord(-1, 0),
        new Coord(-1, 1),
        new Coord(0, -1),
        new Coord(0, 1),
        new Coord(1, -1),
        new Coord(1, 0),
        new Coord(1, 1)
    };
    Attributes attributes = new Attributes();
    
    //List<Coord> neighbors = new List<Coord>();
    List<Action> actions = new List<Action>();

    public int cooldown {
        get {
            return attributes.cooldown;
        }
    }
    public Coord position {
        get {
            return new Coord(_position.x, _position.y);
        }
    }

    public Coord[] neighboors {
        get {
            return CurrentNeighboors();
        }
    }

    public Unit(Coord position) {
        this._position = position;
    }

    public void IncreaseCooldown(int amount) {
        attributes.cooldown += amount;
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    private Coord[] CurrentNeighboors() {
        Coord[] currentNeigboors = new Coord[neighborDeltas.Length];
        for (int i = 0; i < currentNeigboors.Length; i++) {
            currentNeigboors[i] = position + neighborDeltas[i];
        }
        return currentNeigboors;
    }
}
