using System;

public class UnitAddedEventArgs : EventArgs {
    public Unit unit;

    public UnitAddedEventArgs(Unit unit) {
        this.unit = unit;
    }
}