using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using System;

[CustomEditor(typeof(FieldOfView))]
public class FeildOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        //Creates a cirlce around the player of what they are able to see
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position,Vector3.up,Vector3.forward,360,fov.radius);

        //Caclulates Angle of view radius
        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle/2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle/2);

        //Makes yellow lines that show the angle of the FOV
        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position,fov.transform.position + viewAngle02 * fov.radius);

        //If you can see a player, draw a green line to said player
        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
    }

    //Does some fancy math stuff to help draw a cone of vision
    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,MathF.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
