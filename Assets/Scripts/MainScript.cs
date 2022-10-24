using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{

    public string difficulty;
    public string level;
    public int prevHighScore;

    public int score = 0;
    public int energyBar = 100;
    public bool gameOver = false;

    public float incTime;
    public float endTime;

    public GameObject left;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject pause;

    public GameObject pauseScreen;

    public GameObject leftLongBody;
    public GameObject leftLongHead;

    public GameObject upLongBody;
    public GameObject upLongHead;

    public GameObject downLongBody;
    public GameObject downLongHead;

    public GameObject rightLongBody;
    public GameObject rightLongHead;

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

    public GameObject leftLongBodyInstance; 
    public GameObject leftLongHeadInstance;

    public GameObject upLongBodyInstance;
    public GameObject upLongHeadInstance;

    public GameObject downLongBodyInstance;
    public GameObject downLongHeadInstance;

    public GameObject rightLongBodyInstance;
    public GameObject rightLongHeadInstance;

    //WHETHER THE KEY HAS BEEN PRESSED IN THE MOST RECENT ONGUI CALL - USED TO ELIMINATE DOUBLE PRESSES

    bool leftDown = false;
    bool upDown = false;
    bool downDown = false;
    bool rightDown = false;
    bool escDown = false;

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

    float leftMultiPressed = 0.0f;
    float upMultiPressed = 0.0f;
    float downMultiPressed = 0.0f;
    float rightMultiPressed = 0.0f;

    //THE START TIME OF THE MOST RECENT LONG PRESS OR MULTI PRESS

    float leftDownTime = 0;
    float upDownTime = 0;
    float downDownTime = 0;
    float rightDownTime = 0;

    public GameObject currentLeft = null;
    public GameObject currentUp = null;
    public GameObject currentDown = null;
    public GameObject currentRight = null;

    // I - AUDIO PLAYERS

    [SerializeField] private PlaySoundQuick backings = null;
    [SerializeField] private PlaySoundQuick guitar = null;
    [SerializeField] private PlaySoundQuick wrong_note = null;

    private AudioSource guitarPlayer = null;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("Difficulty"));
        Debug.Log(PlayerPrefs.GetString("Level"));
        difficulty = PlayerPrefs.GetString("Difficulty");
        level = PlayerPrefs.GetString("Level");

        prevHighScore = PlayerPrefs.GetInt(difficulty + level);
        Debug.Log("Your previous high score on level " + level + " at " + difficulty + " is " + prevHighScore);
        Application.targetFrameRate = 60;

        TextAsset input = Resources.Load(level+"-"+difficulty) as TextAsset;
        string inputText = input.text;
        string[] lines = Regex.Split(inputText, "\r\n");

        for (int i = 0; i < lines.Length; i++)
        {
            string[] props = Regex.Split(lines[i], "-");
            if (Equals(props[2],"L"))
            {
                leftTime.Add(float.Parse(props[1]));
                leftType.Add(props[0]);
                if (!Equals(props[0], "s"))
                {
                    leftDuration.Add(float.Parse(props[3]));
                }
            }
            if (Equals(props[2], "U"))
            {
                upTime.Add(float.Parse(props[1]));
                upType.Add(props[0]);
                if (!Equals(props[0], "s"))
                {
                    upDuration.Add(float.Parse(props[3]));
                }
            }
            if (Equals(props[2], "D"))
            {
                downTime.Add(float.Parse(props[1]));
                downType.Add(props[0]);
                if (!Equals(props[0], "s"))
                {
                    downDuration.Add(float.Parse(props[3]));
                }
            }
            if (Equals(props[2], "R"))
            {
                rightTime.Add(float.Parse(props[1]));
                rightType.Add(props[0]);
                if (!Equals(props[0], "s"))
                {
                    rightDuration.Add(float.Parse(props[3]));
                }
            }
        }

        // I - Get the guitar's audiosource
        guitarPlayer = guitar.GetComponent<AudioSource>();

        // I - set the correct clips for the audio players
        AudioClip b_sound = Resources.Load(level + "-b") as AudioClip;
        AudioClip g_sound = Resources.Load(level + "-g") as AudioClip;

        // I - set each audio track to the correct player
        backings.SetClip(b_sound);
        guitar.SetClip(g_sound);

        // I - start the audio tracks
        backings.Play();
        guitar.Play();
    }

    // Update is called once per frame
    void Update()
    {
        int timer = Time.frameCount;

        incTime = incTime + Time.deltaTime;

        //LEFT FACTORY

        if (leftTime.Count > 0)
        {
            if ((float)leftTime[0] <= incTime)
            {
                GameObject tempLeft = Instantiate(left, new Vector3(-145, 125, -1), Quaternion.identity);
                tempLeft.AddComponent<ArrowProps>();
                tempLeft.GetComponent<ArrowProps>().time = incTime;
                tempLeft.GetComponent<ArrowProps>().direction = "L";
                tempLeft.GetComponent<ArrowProps>().type = leftType[0].ToString();
                if (Equals(leftType[0].ToString(), "l"))
                {
                    tempLeft.GetComponent<ArrowProps>().duration = (float)leftDuration[0];

                    leftLongBodyInstance = Instantiate(leftLongBody, new Vector3(-145, 132 + 48f * (float)leftDuration[0], 0), Quaternion.identity);
                    leftLongBodyInstance.transform.localScale = new Vector3(38f, 100f * (100f / 95f) * (float)leftDuration[0], 1);
                    leftLongBodyInstance.AddComponent<DownMover>();

                    leftLongHeadInstance = Instantiate(leftLongHead, new Vector3(-145, 135 + 100f * (float)leftDuration[0], 0), Quaternion.identity);
                    leftLongHeadInstance.transform.localScale = new Vector3(38f, 38f, 1);
                    leftLongHeadInstance.AddComponent<DownMover>();

                    leftDuration.RemoveAt(0);
                }
                else if (Equals(leftType[0].ToString(), "m"))
                {
                    tempLeft.GetComponent<ArrowProps>().duration = (float)leftDuration[0];
                    leftDuration.RemoveAt(0);
                }
                //else
                //{
                //    //tempLeft.transform.localScale = new Vector3(1, 0.02f, 1);
                //}
                
                leftArrows.Add(tempLeft);
                leftTime.RemoveAt(0);
                leftType.RemoveAt(0);
                
            }
        }

        //UP FACTORY

        if (upTime.Count > 0)
        {
            if ((float)upTime[0]  <= incTime)
            {
                GameObject tempUp = Instantiate(up, new Vector3(-45, 125, -1), Quaternion.identity);
                tempUp.AddComponent<ArrowProps>();
                tempUp.GetComponent<ArrowProps>().direction = "U";
                tempUp.GetComponent<ArrowProps>().time = incTime;
                tempUp.GetComponent<ArrowProps>().type = upType[0].ToString();
                if (Equals(upType[0].ToString(), "l"))
                {
                    tempUp.GetComponent<ArrowProps>().duration = (float)upDuration[0];

                    upLongBodyInstance = Instantiate(upLongBody, new Vector3(-45, 132 + 48f * (float)upDuration[0], 0), Quaternion.identity);
                    upLongBodyInstance.transform.localScale = new Vector3(38f, 101f * (float)upDuration[0], 1);
                    upLongBodyInstance.AddComponent<DownMover>();

                    upLongHeadInstance = Instantiate(upLongHead, new Vector3(-45, 141 + 90.3f * (float)upDuration[0], 0), Quaternion.identity);
                    upLongHeadInstance.transform.localScale = new Vector3(38f, 38f, 1);
                    upLongHeadInstance.AddComponent<DownMover>();
                    upDuration.RemoveAt(0);
                }
                else if (Equals(upType[0].ToString(), "m"))
                {
                    tempUp.GetComponent<ArrowProps>().duration = (float)upDuration[0];
                    upDuration.RemoveAt(0);
                }
                //else
                //{
                //    //tempUp.transform.localScale = new Vector3(1, 0.02f, 1);
                //}
                upArrows.Add(tempUp);
                upTime.RemoveAt(0);
                upType.RemoveAt(0);
                
            }
        }

        //DOWN FACTORY

        if (downTime.Count > 0)
        {
            if ((float)downTime[0] <= incTime)
            {
                GameObject tempDown = Instantiate(down, new Vector3(50, 125, -1), Quaternion.identity);
                tempDown.AddComponent<ArrowProps>();
                tempDown.GetComponent<ArrowProps>().time = incTime;
                tempDown.GetComponent<ArrowProps>().direction = "D";
                tempDown.GetComponent<ArrowProps>().type = downType[0].ToString();
                if (Equals(downType[0].ToString(), "l"))
                {
                    tempDown.GetComponent<ArrowProps>().duration = (float)downDuration[0];

                    downLongBodyInstance = Instantiate(downLongBody, new Vector3(50, 115 + 50f * (float)downDuration[0], 0), Quaternion.identity);
                    downLongBodyInstance.transform.localScale = new Vector3(38f, 70f * (float)downDuration[0], 1);
                    downLongBodyInstance.AddComponent<DownMover>();

                    downLongHeadInstance = Instantiate(downLongHead, new Vector3(50, 127 + 100f * (float)downDuration[0], 0), Quaternion.identity);
                    downLongHeadInstance.transform.localScale = new Vector3(38f, 38f, 1);
                    downLongHeadInstance.AddComponent<DownMover>();

                    downDuration.RemoveAt(0);
                }
                else if (Equals(downType[0].ToString(), "m"))
                {
                    tempDown.GetComponent<ArrowProps>().duration = (float)downDuration[0];
                    downDuration.RemoveAt(0);
                }
                //else
                //{
                //    //tempDown.transform.localScale = new Vector3(1, 0.02f, 1);
                //}
                downArrows.Add(tempDown);
                downTime.RemoveAt(0);
                downType.RemoveAt(0);
                
            }
        }

        //RIGHT FACTORY

        if (rightTime.Count > 0)
        {
            if ((float)rightTime[0] <= incTime)
            {
                GameObject tempRight = Instantiate(right, new Vector3(140, 125, -1), Quaternion.identity);
                tempRight.AddComponent<ArrowProps>();
                tempRight.GetComponent<ArrowProps>().time = incTime;
                tempRight.GetComponent<ArrowProps>().direction = "R";
                tempRight.GetComponent<ArrowProps>().type = rightType[0].ToString();
                if (Equals(rightType[0].ToString(), "l"))
                {
                    tempRight.GetComponent<ArrowProps>().duration = (float)rightDuration[0];

                    rightLongBodyInstance = Instantiate(rightLongBody, new Vector3(140, 132 + 48f * (float)rightDuration[0], 0), Quaternion.identity);
                    rightLongBodyInstance.transform.localScale = new Vector3(38f, 100f * (100f / 96) * (float)rightDuration[0], 1);
                    rightLongBodyInstance.AddComponent<DownMover>();

                    rightLongHeadInstance = Instantiate(rightLongHead, new Vector3(140, 138 + 99f * (float)rightDuration[0], 0), Quaternion.identity);
                    rightLongHeadInstance.transform.localScale = new Vector3(38f, 38f, 1);
                    rightLongHeadInstance.AddComponent<DownMover>();

                    rightDuration.RemoveAt(0);
                }
                else if (Equals(rightType[0].ToString(), "m"))
                {
                    tempRight.GetComponent<ArrowProps>().duration = (float)rightDuration[0];
                    rightDuration.RemoveAt(0);
                }
                //else
                //{
                //    //tempRight.transform.localScale = new Vector3(1, 0.02f, 1);
                //}
                rightArrows.Add(tempRight);
                rightTime.RemoveAt(0);
                rightType.RemoveAt(0);
                
            }
        }

        //END OF LEVEL

        if (leftTime.Count == 0 && upTime.Count == 0 && downTime.Count == 0 && rightTime.Count == 0 && leftArrows.Count == 0 && upArrows.Count == 0 && downArrows.Count == 0 && rightArrows.Count == 0)
        {
            if (score > PlayerPrefs.GetInt(difficulty + level))
            {
                PlayerPrefs.SetInt(difficulty + level, score);
            }
            if (!gameOver)
            {
                Debug.Log("You win! Your score is " + score);
                endTime = incTime;
            }
            gameOver = true;
        }

        if (incTime > endTime + 2 && gameOver)
        {
            if (PlayerPrefs.GetString("Mode") == "story")
            {
                if (level == "1")
                {
                    SceneManager.LoadScene(6, LoadSceneMode.Single);
                    PlayerPrefs.SetString("Level", "2");
                    if (!(PlayerPrefs.GetString("MaxCompletedLevel") == "2"))
                    {
                        PlayerPrefs.SetString("MaxCompletedLevel", "1");
                    }
                }
                if (level == "2")
                {
                    SceneManager.LoadScene(7, LoadSceneMode.Single);
                    PlayerPrefs.SetString("Level", "3");
                    PlayerPrefs.SetString("MaxCompletedLevel", "2");
                }
                if (level == "3")
                {
                    SceneManager.LoadScene(9, LoadSceneMode.Single);
                }
            }
            if (PlayerPrefs.GetString("Mode") == "free")
            {
                SceneManager.LoadScene(9, LoadSceneMode.Single);
            }
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
        // I - added alt keypress
        if ((Input.GetKey("left") || Input.GetKey("s")) && leftArrows.Count > 0)
        {
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "s"))
            {
                determineAccuracy(currentLeft);
                leftArrows.RemoveAt(0);
            }
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "l"))
            {
                leftDownTime = incTime;
                leftLongDown = true;
            }
            if (!leftDown && Equals(currentLeft.GetComponent<ArrowProps>().type, "m"))
            {
                if (!leftMultiDown)
                {
                    leftDownTime = incTime;
                }
                leftMultiPressed += 1;
                leftMultiDown = true;
                if (leftMultiPressed == currentLeft.GetComponent<ArrowProps>().duration)
                {
                    float multiFinishTime = incTime;
                    determineMultiAccuracy(leftDownTime, multiFinishTime, currentLeft.GetComponent<ArrowProps>().time, currentLeft.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentLeft.gameObject);
                    leftArrows.RemoveAt(0);
                    leftMultiDown = false;
                }
            }
            leftDown = true;
        }

        // I - added alt keypress
        if (!(Input.GetKey("left") || Input.GetKey("s")))
        {
            leftDown = false;
            if (leftLongDown)
            {
                leftLongDown = false;
                determineLongAccuracy(leftDownTime, incTime, currentLeft.GetComponent<ArrowProps>().time, currentLeft.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentLeft.gameObject);
                Object.Destroy(leftLongBodyInstance.gameObject);
                Object.Destroy(leftLongHeadInstance.gameObject);
                leftArrows.RemoveAt(0);
            }
        }

        //UP KEYPRESS HANDLER

        if ((Input.GetKey("up") || Input.GetKey("d")) && upArrows.Count > 0)
        {
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "s"))
            {
                determineAccuracy(currentUp);
                upArrows.RemoveAt(0);
            }
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "l"))
            {
                upDownTime = incTime;
                upLongDown = true;
            }
            if (!upDown && Equals(currentUp.GetComponent<ArrowProps>().type, "m"))
            {
                if (!upMultiDown)
                {
                    upDownTime = incTime;
                }
                upMultiPressed += 1;
                upMultiDown = true;
                if (upMultiPressed == currentUp.GetComponent<ArrowProps>().duration)
                {
                    float multiFinishTime = incTime;
                    determineMultiAccuracy(upDownTime, multiFinishTime, currentUp.GetComponent<ArrowProps>().time, currentUp.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentUp.gameObject);
                    Object.Destroy(upLongBodyInstance.gameObject);
                    Object.Destroy(upLongHeadInstance.gameObject);
                    upArrows.RemoveAt(0);
                    upMultiDown = false;
                }
            }
            upDown = true;
        }

        if (!(Input.GetKey("up") || Input.GetKey("d")))
        {
            upDown = false;
            if (upLongDown)
            {
                upLongDown = false;
                determineLongAccuracy(upDownTime, incTime, currentUp.GetComponent<ArrowProps>().time, currentUp.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentUp.gameObject);
                upArrows.RemoveAt(0);
            }
        }

        //DOWN KEYPRESS HANDLER

        if ((Input.GetKey("down") || Input.GetKey("k")) && downArrows.Count > 0)
        {
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "s"))
            {
                determineAccuracy(currentDown);
                downArrows.RemoveAt(0);
            }
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "l"))
            {
                downDownTime = incTime;
                downLongDown = true;
            }
            if (!downDown && Equals(currentDown.GetComponent<ArrowProps>().type, "m"))
            {
                if (!downMultiDown)
                {
                    downDownTime = incTime;
                }
                downMultiPressed += 1;
                downMultiDown = true;
                if (downMultiPressed == currentDown.GetComponent<ArrowProps>().duration)
                {
                    float multiFinishTime = incTime;
                    determineMultiAccuracy(downDownTime, multiFinishTime, currentDown.GetComponent<ArrowProps>().time, currentDown.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentDown.gameObject);
                    downArrows.RemoveAt(0);
                    downMultiDown = false;
                }
            }
            downDown = true;
        }

        if (!(Input.GetKey("down") || Input.GetKey("k")))
        {
            downDown = false;
            if (downLongDown)
            {
                downLongDown = false;
                determineLongAccuracy(downDownTime, incTime, currentDown.GetComponent<ArrowProps>().time, currentDown.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentDown.gameObject);
                Object.Destroy(downLongBodyInstance.gameObject);
                Object.Destroy(downLongHeadInstance.gameObject);
                downArrows.RemoveAt(0);
            }
        }

        //RIGHT KEYPRESS HANDLER

        if ((Input.GetKey("right") || Input.GetKey("l")) && rightArrows.Count > 0)
        {
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "s"))
            {
                determineAccuracy(currentRight);
                rightArrows.RemoveAt(0);
            }
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "l"))
            {
                rightDownTime = incTime;
                rightLongDown = true;
            }
            if (!rightDown && Equals(currentRight.GetComponent<ArrowProps>().type, "m"))
            {
                if (!rightMultiDown)
                {
                    rightDownTime = incTime;
                }
                rightMultiPressed += 1;
                rightMultiDown = true;
                if (rightMultiPressed == currentRight.GetComponent<ArrowProps>().duration)
                {
                    float multiFinishTime = incTime;
                    determineMultiAccuracy(rightDownTime, multiFinishTime, currentRight.GetComponent<ArrowProps>().time, currentRight.GetComponent<ArrowProps>().duration);
                    Object.Destroy(currentRight.gameObject);
                    Object.Destroy(rightLongBodyInstance.gameObject);
                    Object.Destroy(rightLongHeadInstance.gameObject);
                    rightArrows.RemoveAt(0);
                    rightMultiDown = false;
                }
            }
            rightDown = true;
        }

        if (!(Input.GetKey("right") || Input.GetKey("l")))
        {
            rightDown = false;
            if (rightLongDown)
            {
                rightLongDown = false;
                determineLongAccuracy(rightDownTime, incTime, currentRight.GetComponent<ArrowProps>().time, currentRight.GetComponent<ArrowProps>().duration);
                Object.Destroy(currentRight);
                rightArrows.RemoveAt(0);
            }
        }

        //DELETE ARROWS OVER 3


        //PAUSE THE GAME

        if (Input.GetKey("escape"))
        {
            if (Time.timeScale == 1 && !escDown)
            {
                Time.timeScale = 0;
                pauseScreen = GameObject.Instantiate(pause, new Vector3(0, 0, 0), Quaternion.identity);
                escDown = true;
            }
            if (Time.timeScale == 0 && !escDown)
            {
                Object.Destroy(pauseScreen.gameObject);
                Time.timeScale = 1;
                escDown = true;
            }
        }
        if (!Input.GetKey("escape"))
        {
            escDown = false;
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
        SceneManager.LoadScene(8, LoadSceneMode.Single);
    }

    void determineAccuracy(GameObject arrow)
    {
        bool isAccurate = false;
        if (arrow.transform.position.y > -185 && arrow.transform.position.y < -165)
        {
            score = score + 50;
            isAccurate = true;
            energyBarChanger(3);
        }
        else if (arrow.transform.position.y > -200 && arrow.transform.position.y < -150)
        {
            score = score + 40;
            isAccurate = true;
            energyBarChanger(2);
        }
        else if (arrow.transform.position.y > -215 && arrow.transform.position.y < -135)
        {
            score = score + 30;
            isAccurate = true;
            energyBarChanger(1);
        }
        if (!isAccurate)
        {
            energyBarChanger(-5);
            // I - mute the guitar's audio on incorrect note
            guitarPlayer.mute = true;
            // I - play an audio indicator
            wrong_note.PlayFromListRandom();
        }
        // I - restore guitar audio on correct note
        else
        {
            if (guitarPlayer.mute)
                guitarPlayer.mute = false;
        }
        Object.Destroy(arrow.gameObject);
    }

    public void determineLongAccuracy(float realStartTime, float endTime, float startTime, float duration)
    {
        float durationAccuracy = (float) (endTime - realStartTime) / duration;
        float startAccuracy = Mathf.Abs(startTime - realStartTime) - 3f;

        bool isAccurate = false;
        if (startAccuracy < 0.2 && (durationAccuracy < 1.1 && durationAccuracy > 0.9))
        {
            score = score + 50;
            isAccurate = true;
            energyBarChanger(5);
        }
        else if (startAccuracy < 0.3 && (durationAccuracy < 1.2 && durationAccuracy > 0.8))
        {
            score = score + 40;
            isAccurate = true;
            energyBarChanger(4);
        }
        else if (startAccuracy < 0.4 && (durationAccuracy < 1.3 && durationAccuracy > 0.7))
        {
            score = score + 30;
            isAccurate = true;
            energyBarChanger(2);
        }
        if (!isAccurate)
        {
            energyBarChanger(-3);
        }
        // I - restore guitar audio on correct note
        else
        {
            if (guitarPlayer.mute)
                guitarPlayer.mute = false;
        }
    }

    public void determineMultiAccuracy(float realStartTime, float endTime, float startTime, float duration)
    {
        float durationAccuracy = (float)(endTime - realStartTime) / duration;
        float startAccuracy = Mathf.Abs(startTime - realStartTime + 5);
    
        bool isAccurate = false;
        if (startAccuracy < 10 && (durationAccuracy < 0.2))
        {
            score = score + 50;
            isAccurate = true;
        }
        else if (startAccuracy < 20 && (durationAccuracy < 0.3))
        {
            score = score + 40;
            isAccurate = true;
        }
        else if (startAccuracy < 30 && (durationAccuracy < 0.4))
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
