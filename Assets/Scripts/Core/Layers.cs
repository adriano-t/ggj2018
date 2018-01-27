using UnityEngine;
public class Layers
{
    public static int LAYER_DEFAULT = 0;
    public static int LAYER_TRANSPARENTFX = 1;
    public static int LAYER_IGNORE_RAYCAST = 2;
    public static int LAYER_WATER = 4;
    public static int LAYER_UI = 5;
    public static int LAYER_PLAYER = 8;
    public static int MASK_DEFAULT = 1 << 0;
    public static int MASK_TRANSPARENTFX = 1 << 1;
    public static int MASK_IGNORE_RAYCAST = 1 << 2;
    public static int MASK_WATER = 1 << 4;
    public static int MASK_UI = 1 << 5;
    public static int MASK_PLAYER = 1 << 8;

    public static int Negate (int layer)
    {
        return ~layer;
    }
}
