using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages; // Array to store your level pages (Page1, Page2, etc.)

    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage); // Show the first page initially
    }

    public void ShowNextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void ShowPreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    private void ShowPage(int pageIndex)
    {
        // Deactivate all pages
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == pageIndex); // Only activate the current page
        }
    }
}

