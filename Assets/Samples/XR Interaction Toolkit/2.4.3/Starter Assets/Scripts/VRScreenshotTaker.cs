using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.UI;
using TMPro;

public class VRScreenshotTaker : MonoBehaviour
{
    public static VRScreenshotTaker Instance { get; private set; }
    VRScreenshotTaker() => Instance = this;

    [SerializeField] Camera screenshotCamera; // Die Kamera, von der du einen Screenshot machen möchtest
    [SerializeField] LazyFollow lazyFollowCamera;
    [SerializeField] GameObject greenScreen;
    [SerializeField] Transform greenScreenPosition;
    private Collider[] colliders;
    [SerializeField] GameObject notification;
    [SerializeField] FilterEditor filterEditor;
    private bool needFilter;

    [SerializeField] bool saveWithNoFilter = false;

    //Save/Edit Buttons
    [SerializeField] Button buttonSaveWithFilter;
    [SerializeField] Button buttonSaveWithoutFilter;
    [SerializeField] Button buttonEditFilter;
    [SerializeField] GameObject TextFilterInformation;

    public bool SaveWithNoFilter
    {
        set => saveWithNoFilter = value;
    }
    private Coroutine showNotification;

    private void Start()
    {
        PersonalData[] personalData = FindObjectsOfType<PersonalData>();
        colliders = personalData.Select(p => p.CoverCollider).ToArray();

    }

    public static void ActivateSaveCapturing()
    {
        Instance.Reset();
        Instance.greenScreen.SetActive(true);
        Instance.greenScreen.transform.position = Instance.greenScreenPosition.position;
        Instance.needFilter = false;
        Instance.notification.SetActive(false);
        if (Instance.showNotification != null)
            Instance.StopCoroutine(Instance.showNotification);
        Instance.StartCoroutine(Instance.DisableLazyFollow());
        Instance.saveWithNoFilter = false;
        foreach (FilterCover cover in Instance.colliders.Select(c => c.GetComponentInParent<FilterCover>()))
            cover.SetActiveCover(false);
        // Check visibility using the FieldOfView method
        List<FilterCover> visibleCovers = Instance.colliders.Where(c => FieldOfView.IsVisibleFromCamera(Instance.screenshotCamera, c)).Select(c => c.GetComponentInParent<FilterCover>()).ToList();
        Instance.filterEditor.covers = visibleCovers;
        Instance.filterEditor.OnFilterChange();
        Instance.needFilter = visibleCovers.Count() > 0; //Instance.colliders.Any(c => FieldOfView.IsVisibleFromCamera(Instance.screenshotCamera, c));

        if (Instance.needFilter)
        {
            Instance.buttonEditFilter.gameObject.SetActive(true);
            Instance.buttonSaveWithFilter.GetComponentInChildren<TMP_Text>().text = "Save (with Filter)";
            Instance.buttonSaveWithoutFilter.gameObject.SetActive(true);
            Instance.TextFilterInformation.SetActive(true);
            foreach (FilterCover cover in visibleCovers)
                cover.SetActiveCover(true);
        }
    }

    public static void TakeScreenshot()
    {

        RenderTexture before = Instance.screenshotCamera.targetTexture;
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        Instance.screenshotCamera.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        Instance.filterEditor.ResetColor();
        if (Instance.saveWithNoFilter)
        {
            foreach (FilterCover cover in Instance.filterEditor.covers)
                cover.SetActiveCover(false);
            if (Instance.needFilter)
                Instance.showNotification = Instance.StartCoroutine(Instance.Notification());
        }

        // Screenshot capturing
        string timeStamp = DateTime.Now.ToString("HH-mm-ss");
        string fileName = $"SaveScreenshot_{timeStamp}.png";
        string path = Path.Combine(Application.persistentDataPath, fileName);

        Instance.screenshotCamera.Render();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        Instance.screenshotCamera.targetTexture = before;
        RenderTexture.active = null;
        Destroy(renderTexture);

        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        Debug.Log($"Screenshot saved to {path}");

        Instance.Reset();

    }

    public void Reset()
    {
        Instance.filterEditor.gameObject.SetActive(false);
        Instance.greenScreen.SetActive(false);
        Instance.lazyFollowCamera.enabled = true;
        Instance.TextFilterInformation.SetActive(false);
        Instance.buttonEditFilter.gameObject.SetActive(false);
        Instance.buttonSaveWithFilter.GetComponentInChildren<TMP_Text>().text = "Save";
        Instance.buttonSaveWithoutFilter.gameObject.SetActive(false);
        Instance.colliders.Select(c => c.GetComponentInParent<FilterCover>()).ToList().ForEach(c => c.SetActiveCover(false));
    }
    IEnumerator Notification()
    {
        yield return new WaitForEndOfFrame();
        notification.SetActive(true);
        yield return new WaitForSeconds(10);
        notification.SetActive(false);
    }
    IEnumerator DisableLazyFollow()
    {
        yield return new WaitForEndOfFrame();
        lazyFollowCamera.enabled = false;
    }
}
