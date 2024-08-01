using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FilterEditor : MonoBehaviour
{
    // Button Move Cover
    [Tooltip("Button to move the cover to the left")]
    [SerializeField] Button buttonMoveLeft;

    [Tooltip("Button to move the cover to the right")]
    [SerializeField] Button buttonMoveRight;

    [Tooltip("Button to move the cover up")]
    [SerializeField] Button buttonMoveUp;

    [Tooltip("Button to move the cover down")]
    [SerializeField] Button buttonMoveDown;
    // Button change Size Cover

    [Tooltip("Button to increase the size of the cover in the x direction")]
    [SerializeField] Button buttonSizePlusX;

    [Tooltip("Button to decrease the size of the cover in the x direction")]
    [SerializeField] Button buttonSizeMinusX;

    [Tooltip("Button to increase the size of the cover in the y direction")]
    [SerializeField] Button buttonSizePlusY;

    [Tooltip("Button to decrease the size of the cover in the y direction")]
    [SerializeField] Button buttonSizeMinusY;

    // Button Other Options

    [Tooltip("Button to reset the position and size of the cover")]
    [SerializeField] Button ResetCoverPositionAndSize;

    [Tooltip("Button to delete the cover")]
    [SerializeField] Button buttonDeleteCover;

    // Droptdown TextMeshPro Covers

    [Tooltip("Dropdown to select the cover to edit")]
    [SerializeField] TMP_Dropdown dropdownCovers;
    // All Covers

    [Tooltip("List of all covers")]
    public List<FilterCover> covers;

    // Amount to move and size the cover

    [Tooltip("Amount to move the cover")]
    [SerializeField] float moveAmount = 5f;

    [Tooltip("Amount to size the cover")]
    [SerializeField] float sizeAmount = 0.1f;

    // Default Color
    [Tooltip("Default color of the cover")]
    [SerializeField] Color defaultColor = Color.white;

    /// <summary>
    /// Change the dropdown options when the filter changes.
    /// </summary>
    public void OnFilterChange()
    {
        dropdownCovers.ClearOptions();
        List<string> options = new List<string>();
        foreach (FilterCover cover in covers)
        {
            options.Add("Filter " + covers.IndexOf(cover));
        }
        dropdownCovers.AddOptions(options);
        if (options.Count > 0)
        {
            DropDownValueChanged(0);
            ResetColor();
        }

    }

    /// <summary>
    /// Change the value of the dropdown
    /// </summary>
    /// <param name="value"> The value to change the dropdown to.</param> 
    public void DropDownValueChanged(int value)
    {
        RemoveAllListenerButton();
        ResetColor();
        buttonMoveLeft.onClick.AddListener(() => covers[value].MoveX(-moveAmount));
        buttonMoveRight.onClick.AddListener(() => covers[value].MoveX(moveAmount));
        buttonMoveUp.onClick.AddListener(() => covers[value].MoveY(moveAmount));
        buttonMoveDown.onClick.AddListener(() => covers[value].MoveY(-moveAmount));
        buttonSizePlusX.onClick.AddListener(() => covers[value].SizeX(sizeAmount));
        buttonSizeMinusX.onClick.AddListener(() => covers[value].SizeX(-sizeAmount));
        buttonSizePlusY.onClick.AddListener(() => covers[value].SizeY(sizeAmount));
        buttonSizeMinusY.onClick.AddListener(() => covers[value].SizeY(-sizeAmount));
        ResetCoverPositionAndSize.onClick.AddListener(() => { covers[value].ResetPositionAndScale(); covers[value].SetActiveCover(true); });
        buttonDeleteCover.onClick.AddListener(() => covers[value].SetActiveCover(false));

        //change color of selected cover
        covers[value].SetColor(Color.red);
    }

    public void ResetColor()
    {
        foreach (FilterCover cover in covers)
        {
            cover.SetColor(defaultColor);
        }
    }

    /// <summary>
    /// Remove all listener from the buttons
    /// </summary>
    private void RemoveAllListenerButton()
    {
        buttonMoveLeft.onClick.RemoveAllListeners();
        buttonMoveRight.onClick.RemoveAllListeners();
        buttonMoveUp.onClick.RemoveAllListeners();
        buttonMoveDown.onClick.RemoveAllListeners();
        buttonSizePlusX.onClick.RemoveAllListeners();
        buttonSizeMinusX.onClick.RemoveAllListeners();
        buttonSizePlusY.onClick.RemoveAllListeners();
        buttonSizeMinusY.onClick.RemoveAllListeners();
        ResetCoverPositionAndSize.onClick.RemoveAllListeners();
        buttonDeleteCover.onClick.RemoveAllListeners();
    }


}
