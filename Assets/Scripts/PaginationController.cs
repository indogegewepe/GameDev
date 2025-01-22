using UnityEngine;
using UnityEngine.UI;

public class PaginationController : MonoBehaviour
{
    public GameObject[] pages; // Array untuk menampung halaman
    public Button nextButton; // Tombol Next
    public Button previousButton; // Tombol Previous
    public float animationDuration = 0.5f; // Durasi animasi slide

    private int currentPageIndex = 0; // Indeks halaman saat ini
    private bool isAnimating = false; // Status animasi

    void Start()
    {
        UpdatePages();
        UpdateButtons();
    }

    public void OnNextButtonClicked()
    {
        if (currentPageIndex < pages.Length - 1 && !isAnimating)
        {
            int nextPageIndex = currentPageIndex + 1;
            StartCoroutine(AnimatePageTransition(currentPageIndex, nextPageIndex, true));
            currentPageIndex = nextPageIndex;
            UpdateButtons();
        }
    }

    public void OnPreviousButtonClicked()
    {
        if (currentPageIndex > 0 && !isAnimating)
        {
            int previousPageIndex = currentPageIndex - 1;
            StartCoroutine(AnimatePageTransition(currentPageIndex, previousPageIndex, false));
            currentPageIndex = previousPageIndex;
            UpdateButtons();
        }
    }

    private void UpdatePages()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }
    }

    private void UpdateButtons()
    {
        nextButton.interactable = currentPageIndex < pages.Length - 1;
        previousButton.interactable = currentPageIndex > 0;
    }

    private System.Collections.IEnumerator AnimatePageTransition(int fromIndex, int toIndex, bool isNext)
    {
        isAnimating = true;

        GameObject fromPage = pages[fromIndex];
        GameObject toPage = pages[toIndex];

        RectTransform fromRect = fromPage.GetComponent<RectTransform>();
        RectTransform toRect = toPage.GetComponent<RectTransform>();

        toPage.SetActive(true);

        float elapsedTime = 0f;
        Vector3 fromStartPosition = fromRect.anchoredPosition;
        Vector3 toStartPosition = isNext ? new Vector3(Screen.width, 0, 0) : new Vector3(-Screen.width, 0, 0);

        Vector3 fromEndPosition = isNext ? new Vector3(-Screen.width, 0, 0) : new Vector3(Screen.width, 0, 0);
        Vector3 toEndPosition = Vector3.zero;

        toRect.anchoredPosition = toStartPosition;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            fromRect.anchoredPosition = Vector3.Lerp(fromStartPosition, fromEndPosition, t);
            toRect.anchoredPosition = Vector3.Lerp(toStartPosition, toEndPosition, t);

            yield return null;
        }

        fromRect.anchoredPosition = fromEndPosition;
        toRect.anchoredPosition = toEndPosition;

        fromPage.SetActive(false);
        isAnimating = false;

        UpdatePages(); // Pastikan halaman aktif diperbarui
    }
}
