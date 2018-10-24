using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    void OnTouchingWall(Vector2 _normal);
    void OnTouchingGround(Vector2 _normal);
    void OnLeavingWall();
    void OnLeavingGround();
}
