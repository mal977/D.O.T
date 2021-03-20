using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    private VideoClip winTutorialClip;
    [SerializeField]
    private VideoClip mistakeTutorialClip;
    [SerializeField]
    private GameObject videoPlayer;
    [SerializeField]
    private GameObject instructionOne;
    [SerializeField]
    private GameObject instructionTwo;
    [SerializeField]
    private GameObject instructionThree;
    [SerializeField]
    private GameObject prevBtn;
    [SerializeField]
    private GameObject nextBtn;
    [SerializeField]
    private GameObject startTestBtn;

    private int pageNo = 1;
    private int minPage = 1;
    private int maxPage = 3;

    public void NextBtn()
    {
        if (pageNo == minPage)
            prevBtn.SetActive(true);
        pageNo++;
        if (pageNo == maxPage)
            nextBtn.SetActive(false);
        PageEvent();

    }

    public void PreviousBtn()
    {
        if (pageNo == maxPage)
            nextBtn.SetActive(true);
        pageNo--;
        if (pageNo == minPage)
            prevBtn.SetActive(false);

        PageEvent();
    }

    private void PageEvent()
    {
        switch (pageNo)
        {
            case 1:
                videoPlayer.SetActive(true);
                videoPlayer.GetComponent<VideoPlayer>().clip = winTutorialClip;
                videoPlayer.GetComponent<VideoPlayer>().Play();
                instructionOne.SetActive(true);
                instructionTwo.SetActive(false);
                break;
            case 2:
                videoPlayer.SetActive(true);
                videoPlayer.GetComponent<VideoPlayer>().clip = mistakeTutorialClip;
                videoPlayer.GetComponent<VideoPlayer>().Play();
                instructionOne.SetActive(false);
                instructionTwo.SetActive(true);
                instructionThree.SetActive(false);
                startTestBtn.SetActive(false);
                break;
            case 3:
                videoPlayer.SetActive(false);
                instructionTwo.SetActive(false);
                instructionThree.SetActive(true);
                startTestBtn.SetActive(true);
                break;
        }
    }
    
}
