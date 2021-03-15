using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirthdayField : MonoBehaviour
{

    [SerializeField]
    private int maxYear = 2010;
    [SerializeField]
    private int minYear = 1930;
    [SerializeField]
    private Button confirmBtn;
    [SerializeField]
    private Button backBtn;
    public GameObject birthdayPanel;

    // DayUI
    [Header("Day Header")]
    [SerializeField]
    private InputField dayText;
    [SerializeField]
    private Button dayUpBtn;
    [SerializeField]
    private Button dayDownBtn;

    // MonthUI
    [Header("Month Header")]
    [SerializeField]
    private InputField monthText;
    [SerializeField]
    private Button monthUpBtn;
    [SerializeField]
    private Button monthDownBtn;

    // YearUI
    [Header("Year Header")]
    [SerializeField]
    private InputField yearText;
    [SerializeField]
    private Button yearUpBtn;
    [SerializeField]
    private Button yearDownBtn;

    public int day = 1;
    public int month = 1;
    public int year = DateTime.Now.Year-10;

    private int[] daysInMonth = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

    void Start()
    {
        dayText.text = AddZeroFrontDate(day);
        monthText.text = AddZeroFrontDate(month);
        yearText.text = year+"";
        confirmBtn.onClick.AddListener(() =>ConfirmButton());
        backBtn.onClick.AddListener(() => BackButton());

        dayUpBtn.onClick.AddListener(() => DayUpButton());
        //dayUpBtn.OnSelect(DayUpButton());
        dayDownBtn.onClick.AddListener(() => DayDownButton());
        monthUpBtn.onClick.AddListener(() => MonthUpButton());
        monthDownBtn.onClick.AddListener(() => MonthDownButton());
        yearUpBtn.onClick.AddListener(() => YearUpButton());
        yearDownBtn.onClick.AddListener(() => YearDownButton());
    }

    private bool isLeapYear(int yearInput)
    {
        if (yearInput % 4 == 0)
            return true;
        return false;
    }

    // Add zero in front of day and month when less than 10
    private string AddZeroFrontDate(int number) 
    {
        if (number < 10)
            return "0" + number;
        return number.ToString();
    }

    // Method to check and edit the user direct input on day input field
    public void OnDayTextEdited() 
    {
        day = Convert.ToInt32(dayText.text);

        if (day <= 0)
            day = 1;

        // Checks leap year
        if (isLeapYear(year) && month == 2 && day > 29)
        {
            day = 29;
            SetDayText();
            return;
        }

        if(day > daysInMonth[month-1])
            day = daysInMonth[month - 1];

        SetDayText();
    }

    // Method to check and edit the user direct input on month input field
    public void OnMonthTextEdited()
    {
        month = Convert.ToInt32(monthText.text);
        if (month <= 0)
            month = 1;
        if (month > 12)
            month = 12;

        SetMonthText();
    }

    public void OnYearTextEdited()
    {
        year = Convert.ToInt32(yearText.text);

        if (year < minYear)
            year = minYear;
        if (year > maxYear)
            year = maxYear;

        SetYearText();
    }

    public void ConfirmButton()
    {
        Debug.Log("Confirm!");
    }

    public void BackButton() 
    {
        birthdayPanel.SetActive(false);
    }

    public void DayDownButton() 
    {
        if (day == 1) 
            if(isLeapYear(year) && month == 2)
                day = 29;
            else
                day = daysInMonth[month-1];
        else
            day--;

        SetDayText();
    }

    public void DayUpButton()
    {
        if (isLeapYear(year) && month == 2)
        {
            if (day == 29)
                day = 1;
            else
                day++;
            SetDayText();
            return;
        }
        if (day == daysInMonth[month - 1])
            day = 1;
        else
            day++;

        SetDayText();
    }

    public void MonthDownButton()
    {
        if(month == 1)
            month = 12;
        else
            month--;
        if (day > daysInMonth[month - 1])
        {
            if (isLeapYear(year) && month == 2)
                day = 29;
            else
                day = daysInMonth[month - 1];
            SetDayText();
        }

        SetMonthText();
    }
    public void MonthUpButton()
    {
        if (month == 12)
            month = 1;
        else
            month++;

        if (day > daysInMonth[month - 1])
        {
            if (isLeapYear(year) && month == 2)
                day = 29;
            else
                day = daysInMonth[month - 1];
            SetDayText();
        }

        SetMonthText();
    }

    public void YearDownButton()
    {
        if (year == minYear) 
            year = maxYear;
        else
            year--;

        SetYearText();
    }
    public void YearUpButton()
    {
        if (year == maxYear)
            year = minYear;
        else
            year++;
        SetYearText();
    }

    private void SetDayText()
    {
        dayText.text = AddZeroFrontDate(day);
    }
    private void SetMonthText()
    {
        monthText.text = AddZeroFrontDate(month);
    }

    private void SetYearText()
    {
        yearText.text = AddZeroFrontDate(year);
    }

}
