using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MainScript : MonoBehaviour
{

    public int score = 0;
    public int energyBar = 100;
    public bool gameOver = false;

    public GameObject left;
    public GameObject up;
    public GameObject down;
    public GameObject right;

    //TIME WHEN ARROW APPEARS

    ArrayList leftTime = new ArrayList();
    ArrayList upTime = new ArrayList();
    ArrayList downTime = new ArrayList();
    ArrayList rightTime = new ArrayList();

    //TYPE OF ARROW

    ArrayList leftType = new ArrayList();
    ArrayList upType = new ArrayList();
    ArrayList downType = new ArrayList();
    ArrayList rightType = new ArrayList();

    //HOW LONG A LONG KEY SHOULD BE PRESSED / HOW MANY TIMES A MULTIKEY SHOULD BE PRESSED

    ArrayList leftDuration = new ArrayList();
    ArrayList upDuration = new ArrayList();
    ArrayList downDuration = new ArrayList();
    ArrayList rightDuration = new ArrayList();

    //IF WE CAN GET THE ABOVE THREE INTO ONE TUPLE, WE CAN ALSO DO THAT - I GAVE UP ON THAT APPROACH


    //THE LIST OF ARROWS CURRENTLY ON THE SCREEN

    public ArrayList leftArrows = new ArrayList();
    public ArrayList upArrows = new ArrayList();
    public ArrayList downArrows = new ArrayList();
    public ArrayList rightArrows = new ArrayList();

    //WHETHER THE KEY HAS BEEN PRESSED IN THE MOST RECENT ONGUI CALL - USED TO ELIMINATE DOUBLE PRESSES

    bool leftDown = false;
    bool upDown = false;
    bool downDown = false;
    bool rightDown = false;

    //WHETHER A LONG PRESS IS IN PROGRESS

    bool leftLongDown = false;
    bool upLongDown = false;
    bool downLongDown = false;
    bool rightLongDown = false;

    //WHETHER A MULTI PRESS IS IN PROGRESS

    bool leftMultiDown = false;
    bool upMultiDown = false;
    bool downMultiDown = false;
    bool rightMultiDown = false;

    //HOW MANY TIMES A MULTI ARROW HAS BEEN PRESSED

    int leftMultiPressed = 0;
    int upMultiPressed = 0;
    int downMultiPressed = 0;
    int rightMultiPressed = 0;

    //THE START TIME OF THE MOST RECENT LONG PRESS OR MULTI PRESS

    int leftDownTime = 0;
    int upDownTime = 0;
    int downDownTime = 0;
    int rightDownTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Score"));
        Application.targetFrameRate = 60;

        TextAsset input = Resources.Load("test") as TextAsset;
        string inputText = input.text;
        string[] lines = Regex.Split(inputText, "\r\n");

        for (int i = 0; i < lines.Length; i++)
        {
            Debug.Log(lines[i]);
            string[] props = Regex.Split(lines[i], "-");
            Debug.Log(props[0]);
            if (Equals(props[2],"Left"))
            {
                leftTime.Add(int.Parse(props[1]));
                leftType.Add(props[0]);
                if (!Equals(props[0], "Short"))
                {
                    leftDuration.Add(int.Parse(props[3]));
                }
            }
            if (Equals(props[2], "Up"))
            {
                upTime.Add(int.Parse(props[1]));
                upType.Add(props[0]);
                if (!Equals(props[0], "Short"))
                {
                    upDuration.Add(int.Parse(props[3]));
                }
            }
            if (Equals(props[2], "Down"))
            {
                downTime.Add(int.Parse(props[1]));
                downType.Add(props[0]);
                if (!Equals(props[0], "Short"))
                {
                    downDuration.Add(int.Parse(props[3]));
                }
            }
            if (Equals(props[2], "Right"))
            {
                rightTime.Add(int.Parse(props[1]));
                rightType.Add(props[0]);
                if (!Equals(props[0], "Short"))
                {
                    rightDuration.Add(int.Parse(props[3]));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        int timer = Time.frameCount;

        //LEFT FACTORY

        if (leftTime.Count > 0)
        {
            if ((int)leftTime[0] == timer)
            {
                GameObject tempLeft = Instantiate(left, new Vector3(-3, -4, 0), Quaternion.identity);
                tempLeft.AddComponent<UpMover>();
                tempLeft.AddComponent<ArrowProps>();
                tempLeft.GetComponent<ArrowProps>().time = timer;
                tempLeft.GetComponent<ArrowProps>().type = leftType[0].ToString();
                if (!Equals(leftType[0].ToString(), "Short"))
                {
                    tempLeft.GetComponent<ArrowProps>().duration = (int)leftDuration[0];
                    tempLeft.transform.localScale = new Vector3(1, 0.02f * (int)leftDuration[0], 1);
                    tempLeft.transform.position = tempLeft.transform.position + new Vector3(0, -0.01f * (int)leftDuration[0], 0);
                    leftDuration.RemoveAt(0);
                }
                else
                {
                    tempLeft.transform.localScale = new Vector3(1, 0.02f, 1);
                }
                
                leftArrows.Add(tempLeft);
                leftTime.RemoveAt(0);
                leftType.RemoveAt(0);
                
            }
        }

        //UP FACTORY

        if (upTime.Count > 0)
        {
            if ((int)upTime[0] == timer)
            {
                GameObject tempUp = Instantiate(up, new Vector3(-1, -4, 0), Quaternion.identity);
                tempUp.AddComponent<UpMover>();
                tempUp.AddComponent<ArrowProps>();
                tempUp.GetComponent<ArrowProps>().time = timer;
                tempUp.GetComponent<ArrowProps>().type = upType[0].ToString();
                if (!Equals(upType[0].ToString(), "Short"))
                {
                    tempUp.GetComponent<ArrowProps>().duration = (int)upDuration[0];
                    tempUp.transform.localScale = new Vector3(1, 0.02f * (int)upDuration[0], 1);
                    tempUp.transform.position = tempUp.transform.position + new Vector3(0, -0.01f * (int)upDuration[0], 0);
                    upDuration.RemoveAt(0);
                }
                upArrows.Add(tempUp);
                upTime.RemoveAt(0);
                upType.RemoveAt(0);
                
            }
        }

        //DOWN FACTORY

        if (downTime.Count > 0)
        {
            if ((int)downTime[0] == timer)
            {
                GameObject tempDown = Instantiate(down, new Vector3(1, -4, 0), Quaternion.identity);
                tempDown.AddComponent<UpMover>();
                tempDown.AddComponent<ArrowProps>();
                tempDown.GetComponent<ArrowProps>().time = timer;
                tempDown.GetComponent<ArrowProps>().type = downType[0].ToString();
                if (!Equals(downType[0].ToString(), "Short"))
                {
                    tempDown.GetComponent<ArrowProps>().duration = (int)downDuration[0];
                    tempDown.transform.localScale = new Vector3(1, 0.02f * (int)downDuration[0], 1);
                    tempDown.transform.position = tempDown.transform.position + new Vector3(0, -0.01f * (int)downDuration[0], 0);
                    downDuration.RemoveAt(0);
                }
                downArrows.Add(tempDown);
                downTime.RemoveAt(0);
                downType.RemoveAt(0);
                
            }
        }

        //RIGHT FACTORY

        if (rightTime.Count > 0)
        {
            if ((int)rightTime[0] == timer)
            {
                GameObject tempRight = Instantiate(right, new Vector3(3, -4, 0), Quaternion.identity);
                tempRight.AddComponent<UpMover>();
                tempRight.AddComponent<ArrowProps>();
                tempRight.GetComponent<ArrowProps>().time = timer;
                tempRight.GetComponent<ArrowProps>().type = rightType[0].ToString();
                if (!Equals(rightType[0].ToString(), "Short"))
                {
                    tempRight.GetComponent<ArrowProps>().duration = (int)rightDuration[0];
                    tempRight.transform.localScale = new Vector3(1, 0.02f * (int)rightDuration[0], 1);
                    tempRight.transform.position = tempRight.transform.position + new Vector3(0, -0.01f * (int)rightDuration[0]);
                    rightDuration.RemoveAt(0);
                }
                rightArrows.Add(tempRight);
                rightTime.RemoveAt(0);
                rightType.RemoveAt(0);
                
            }
        }

        //END OF LEVEL

        if (leftTime.Count == 0 && upTime.Count == 0 && downTime.Count == 0 && rightTime.Count == 0 && leftArrows.Count == 0 && upArrows.Count == 0 && downArrows.Count == 0 && rightArrows.Count == 0)
        {
            PlayerPrefs.SetInt("Score", score);
            Debug.Log("You win! Your score is " + PlayerPrefs.GetInt("Score"));
        }
    }

    void OnGUI()
    {
        GameObject currentLeft = null;
        GameObject currentUp = null;
        GameObject currentDown = null;
        GameObject currentRight = null;
        if (leftArrows.Count > 0)
        {
            currentLeft = (GameObject)leftArrows[0];
        }
        if (upArrows.Count > 0)
        {
            currentUp = (GameObject)upArrows[0];
        }
        if (downArrows.Count > 0)
        {
            currentDown = (GameObject)downArrows[0];
        }
        if (rightArrows.Count > 0)
        {
            currentRight = (GameObject)rightArrows[0];
        }

        //LEFT KEYPRESS HANDLER

        if (Input.GetKey("left") && leftArrows.Count > 0)
        {
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "Short"))
            {
                if (determineAccuracy(currentLeft))
                {
                    leftArrows.RemoveAt(0);
                }
            }
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "Long"))
            {
                leftDownTime = Time.frameCount;
                leftLongDown = true;
            }
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "Multi"))
            {
                if (!leftMultiDown)
                {
                    leftDownTime = Time.frameCount;
                }
                leftMultiPressed += 1;
                Debug.Log(leftMultiPressed);
                if (leftMultiPressed == currentLeft.GetComponent<ArrowProps>().duration)
                {
                    int multiFinishTime = Time.frameCount;
                    determineMultiAccuracy(leftDownTime, multiFinishTime, currentLeft.GetComponent<ArrowProps>().time, currentLeft.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentLeft);
                    leftArrows.RemoveAt(0);
                }
                leftMultiDown = true;
            }
            leftDown = true;
        }

        if (!Input.GetKey("left"))
        {
            leftDown = false;
            if (leftLongDown)
            {
                leftLongDown = false;
                determineLongAccuracy(leftDownTime, Time.frameCount, currentLeft.GetComponent<ArrowProps>().time, currentLeft.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentLeft);
            }
        }

        //UP KEYPRESS HANDLER

        if (Input.GetKey("up") && upArrows.Count > 0)
        {
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "Short"))
            {
                if (determineAccuracy(currentUp))
                {
                    upArrows.RemoveAt(0);
                }
            }
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "Long"))
            {
                upDownTime = Time.frameCount;
                upLongDown = true;
            }
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "Multi"))
            {
                if (!upMultiDown)
                {
                    upDownTime = Time.frameCount;
                }
                upMultiPressed += 1;
                Debug.Log(upMultiPressed);
                if (upMultiPressed == currentUp.GetComponent<ArrowProps>().duration)
                {
                    int multiFinishTime = Time.frameCount;
                    determineMultiAccuracy(upDownTime, multiFinishTime, currentUp.GetComponent<ArrowProps>().time, currentUp.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentUp);
                    upArrows.RemoveAt(0);
                }
                upMultiDown = true;
            }
            upDown = true;
        }

        Debug.Log(upLongDown);

        if (!Input.GetKey("up"))
        {
            upDown = false;
            if (upLongDown)
            {
                upLongDown = false;
                determineLongAccuracy(upDownTime, Time.frameCount, currentUp.GetComponent<ArrowProps>().time, currentUp.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentUp);
            }
        }

        //DOWN KEYPRESS HANDLER

        if (Input.GetKey("down") && downArrows.Count > 0)
        {
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "Short"))
            {
                if (determineAccuracy(currentDown))
                {
                    downArrows.RemoveAt(0);
                }
            }
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "Long"))
            {
                downDownTime = Time.frameCount;
                downLongDown = true;
            }
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "Multi"))
            {
                if (!downMultiDown)
                {
                    downDownTime = Time.frameCount;
                }
                downMultiPressed += 1;
                Debug.Log(downMultiPressed);
                if (downMultiPressed == currentDown.GetComponent<ArrowProps>().duration)
                {
                    int multiFinishTime = Time.frameCount;
                    determineMultiAccuracy(downDownTime, multiFinishTime, currentDown.GetComponent<ArrowProps>().time, currentDown.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentDown);
                    downArrows.RemoveAt(0);
                }
                downMultiDown = true;
            }
            downDown = true;
        }

        Debug.Log(downLongDown);

        if (!Input.GetKey("down"))
        {
            downDown = false;
            if (downLongDown)
            {
                downLongDown = false;
                determineLongAccuracy(downDownTime, Time.frameCount, currentDown.GetComponent<ArrowProps>().time, currentDown.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentDown);
            }
        }

        //RIGHT KEYPRESS HANDLER

        if (Input.GetKey("right") && rightArrows.Count > 0)
        {
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "Short"))
            {
                if (determineAccuracy(currentRight))
                {
                    rightArrows.RemoveAt(0);
                }
            }
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "Long"))
            {
                rightDownTime = Time.frameCount;
                rightLongDown = true;
            }
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "Multi"))
            {
                if (!rightMultiDown)
                {
                    rightDownTime = Time.frameCount;
                }
                rightMultiPressed += 1;
                Debug.Log(rightMultiPressed);
                if (rightMultiPressed == currentRight.GetComponent<ArrowProps>().duration)
                {
                    int multiFinishTime = Time.frameCount;
                    determineMultiAccuracy(rightDownTime, multiFinishTime, currentRight.GetComponent<ArrowProps>().time, currentRight.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentRight);
                    rightArrows.RemoveAt(0);
                }
                rightMultiDown = true;
            }
            rightDown = true;
        }

        Debug.Log(rightLongDown);

        if (!Input.GetKey("right"))
        {
            rightDown = false;
            if (rightLongDown)
            {
                rightLongDown = false;
                determineLongAccuracy(rightDownTime, Time.frameCount, currentRight.GetComponent<ArrowProps>().time, currentRight.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentRight);
            }
        }

        //DELETE ARROWS OVER 3

        if (currentLeft != null)
        {
            if (currentLeft.transform.position.y > 4)
            {
                Object.Destroy(currentLeft);
                leftArrows.RemoveAt(0);
            }
        }
        if (currentUp != null)
        {
            if (currentUp.transform.position.y > 4)
            {
                Object.Destroy(currentUp);
                upArrows.RemoveAt(0);
            }
        }
        if (currentDown != null)
        {
            if (currentDown.transform.position.y > 4)
            {
                Object.Destroy(currentDown);
                downArrows.RemoveAt(0);
            }
        }
        if (currentRight != null)
        {
            if (currentRight.transform.position.y > 4)
            {
                Object.Destroy(currentRight);
                rightArrows.RemoveAt(0);
            }
        }
    }

    public void energyBarChanger(int change)
    {
        energyBar = energyBar + change;
        if (energyBar > 100)
        {
            energyBar = 100;
        }
        if (energyBar <= 0)
        {
            endGame();
        }
    }

    public void endGame()
    {
        Debug.Log("You Lose :(");
        gameOver = true;
    }

    bool determineAccuracy(GameObject arrow)
    {
        bool isAccurate = false;
        if (arrow.transform.position.y > 1.8 && arrow.transform.position.y < 2.2)
        {
            Object.Destroy(arrow);
            score = score + 50;
            isAccurate = true;
        }
        else if (arrow.transform.position.y > 1.6 && arrow.transform.position.y < 2.4)
        {
            Object.Destroy(arrow);
            score = score + 40;
            isAccurate = true;
        }
        else if (arrow.transform.position.y > 1.4 && arrow.transform.position.y < 2.6)
        {
            Object.Destroy(arrow);
            score = score + 30;
            isAccurate = true;
        }
        if (!isAccurate)
        {
            energyBarChanger(-20);
        }
        return isAccurate;
    }

    public void determineLongAccuracy(int realStartTime, int endTime, int startTime, int duration)
    {
        float durationAccuracy = (float) (endTime - realStartTime) / duration;
        int startAccuracy = Mathf.Abs(startTime - realStartTime + 300);

        bool isAccurate = false;
        if (startAccuracy < 10 && (durationAccuracy < 1.1 || durationAccuracy > 0.9))
        {
            score = score + 50;
            isAccurate = true;
        }
        else if (startAccuracy < 20 && (durationAccuracy < 1.2 || durationAccuracy > 0.8))
        {
            score = score + 40;
            isAccurate = true;
        }
        else if (startAccuracy < 30 && (durationAccuracy < 1.3 || durationAccuracy > 0.7))
        {
            score = score + 30;
            isAccurate = true;
        }
        if (!isAccurate)
        {
            energyBarChanger(-20);
        }
    }

    public void determineMultiAccuracy(int realStartTime, int endTime, int startTime, int duration)
    {
        float durationAccuracy = (float)(endTime - realStartTime) / duration;
        int startAccuracy = Mathf.Abs(startTime - realStartTime + 300);
        Debug.Log(durationAccuracy);

        bool isAccurate = false;
        if (startAccuracy < 10 && (durationAccuracy < 10))
        {
            score = score + 50;
            isAccurate = true;
        }
        else if (startAccuracy < 20 && (durationAccuracy < 12))
        {
            score = score + 40;
            isAccurate = true;
        }
        else if (startAccuracy < 30 && (durationAccuracy < 15))
        {
            score = score + 30;
            isAccurate = true;
        }
        if (!isAccurate)
        {
            energyBarChanger(-20);
        }
    }
}
