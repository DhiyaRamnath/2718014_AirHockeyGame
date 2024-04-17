public struct Boundary
{
    public float Up, Down, Left, Right;
    public Boundary(float up, float down, float left, float right)
    {
        Up = up;
        Down = down;
        Left = left;
        Right = right;
    }
}//this code has been used for 3 different game objects so for easier referencing, its in its own script to be called into other scripts for Ai/Player/Puck