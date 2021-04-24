/*
 * Actions that are performed by units, holds the cooldown.
 */
public abstract class Action {
    public Coord position;
    public Unit target;
    protected Unit actor;
    
    public abstract int Perform();
    public abstract int GetCost();
    protected virtual bool isReady() {
        if (actor != null) {
            return true;
        }
        else {
            return false;
        }
    }
}