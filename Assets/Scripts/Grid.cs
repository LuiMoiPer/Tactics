using System;
using System.Collections.Generic;

public class Grid {

    public static readonly int DEFAULT_WIDTH = 16;
    public static readonly int DEFAULT_HEIGHT = 10;
    public static Random random = new Random();

    public int width {
        get {
            return _width;
        }
    }
    public int height {
        get {
            return _height;
        }
    }

    private int _width;
    private int _height;

    public Grid(int width, int height) {
        this._width = width;
        this._height = height;
    }
 
    public bool isValidPosition(Coord position) {
        if ((position.x >= 0 && position.x < width) && (position.y >= 0 && position.y < height)) {
            return true;
        }
        else {
            return false;
        }
    }
}