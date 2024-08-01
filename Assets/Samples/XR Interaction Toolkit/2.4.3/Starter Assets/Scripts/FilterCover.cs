using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FilterCover : MonoBehaviour
{
    // initial position and rotation of the transform
    Vector3 initialPosition;
    Vector3 initialRotation;

    [Tooltip("The cover object to manipulate.")]
    [SerializeField] GameObject coverObject;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation.eulerAngles;
    }

    /// <summary>
    /// En/Disable the coverObject
    /// </summary>
    /// <param> name="state">
    /// The state to set the coverObject to.
    /// </param> 
    public void SetActiveCover(bool state)
    {
        Debug.Log(gameObject.name + ", SetActiveCover, state: " + state);
        coverObject.SetActive(state);
    }

    /// <summary>
    /// Set the color of the coverObject.
    /// </summary>
    /// <param name="color"> The color to set the coverObject to.</param>  
    public void SetColor(Color color)
    {
        coverObject.GetComponent<Image>().color = color;
    }

    /// <summary>
    /// Reset the position of the coverObject.
    /// </summary>
    public void ResetPositionAndScale()
    {
        coverObject.transform.localPosition = initialPosition;
        coverObject.transform.localScale = Vector3.one;
    }

    ///<summary>
    /// Reset the rotation of the coverObject.
    /// </summary>
    public void ResetRotation()
    {
        coverObject.transform.rotation = Quaternion.Euler(initialRotation);
    }

    /// <summary>
    /// Move the coverObject by a certain amount in the x direction.
    /// </summary>
    /// <param name="amount"> The amount to move the transform by.</param>
    public void MoveX(float amount) => Move(Vector3.right, amount);

    /// <summary>
    /// Move the coverObject by a certain amount in the y direction.
    /// </summary>
    /// <param name="amount"> The amount to move the transform by.</param>
    public void MoveY(float amount) => Move(Vector3.up, amount);

    /// <summary>
    /// Move the <see cref="coverObject"/> by a certain amount in the z direction.
    /// </summary>
    /// <param name="amount"> The amount to move the transform by.</param>
    public void MoveZ(float amount) => Move(Vector3.forward, amount);

    /// <summary>
    /// Move the coverObject by a certain amount in a given direction.
    /// </summary>
    /// <param name="direction"> The direction to move the coverObject in.</param>
    public void Move(Vector3 direction, float amount)
    {
        Debug.Log(gameObject.name + ", Move, direction: " + direction + ", amount: " + amount);
        coverObject.transform.localPosition += direction * amount;
    }

    /// <summary>
    /// Change the size of the coverObject by a certain amount in the x direction.
    /// </summary>
    /// <param name="amount"> The amount to change the size of the coverObject by.</param> 
    public void SizeX(float amount) => Size(Vector3.right, amount);

    /// <summary>
    /// Change the size of the coverObject by a certain amount in the y direction.
    /// </summary>
    /// <param name="amount"> The amount to change the size of the coverObject by.</param>
    public void SizeY(float amount) => Size(Vector3.up, amount);

    /// <summary>
    /// Change the size of the coverObject by a certain amount in the z direction.
    /// </summary>
    /// <param name="direction"> The direction to change the size of the coverObject in.</param>
    /// <param name="amount"> The amount to change the size of the coverObject by.</param>
    public void Size(Vector3 direction, float amount)
    {
        Vector3 scale = coverObject.transform.localScale;
        scale += direction * amount;
        coverObject.transform.localScale = scale;
    }

    /// <summary>
    /// Rotate the coverObject by a certain amount in the x direction.
    /// </summary>
    /// <param name="angle"> The euler angle to rotate the coverObject by.</param>
    public void RotateX(float angle) => RotateAroundAxis(angle, Vector3.right);

    /// <summary>
    /// Rotate the coverObject by a certain amount in the y direction.
    /// </summary>
    /// <param name="angle">
    /// The euler angle to rotate the coverObject by.
    /// </param>
    public void RotateY(float angle) => RotateAroundAxis(angle, Vector3.up);

    /// <summary> 
    /// Rotate the coverObject by a certain amount in the z direction.
    /// </summary>
    /// <param name="angle">The euler angle to rotate the coverObject by.</param> 
    /// <param name="axis">The axis to rotate the coverObject around.</param> 
    public void RotateAroundAxis(float angle, Vector3 axis)
    {
        coverObject.transform.Rotate(axis, angle);
    }
}