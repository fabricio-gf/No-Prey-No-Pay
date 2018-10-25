using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    void OnTouchingWall(Vector2 _normal, ContactPoint2D[] _contacts);
    void OnTouchingGround(Vector2 _normal, ContactPoint2D[] _contacts);

    void OnTouchingAnother(Vector2 _normal, ContactPoint2D[] _contacts);

    void OnLeavingWall();
    void OnLeavingGround();
}
