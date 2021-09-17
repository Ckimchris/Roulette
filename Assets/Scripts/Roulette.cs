using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Roulette : MonoBehaviour
{
    private float numberResult;
    private bool spinning;
    private GameObject prize;
    public GameObject sparks; 
    public GameObject wheel;
    public Transform wheel_sections;
    public Transform wheelParent;
    public List<GameObject> prizes;
    public List<Transform> spriteList;
    public GameObject Button;
    public List<AnimationCurve> animationCurves;

    public List<int> prizeWinCount;

    private int SectorOneCount;
    private int SectorTwoCount;
    private int SectorThreeCount;
    private int SectorFourCount;
    private int SectorFiveCount;
    private int SectorSixCount;
    private int SectorSevenCount;
    private int SectorEightCount;

    //private List<int> prizeCount = new List<int>(prizes.Count);

    // Start is called before the first frame update
    void Start()
    {
        spinning = false;
    }

    public void Spin()
    {
        numberResult = Random.Range(1f, 100f);
        if(numberResult <= 20f)
        {
            StartCoroutine(RedeemPrize(1080f + 22.5f, 0));
        }
        else if(numberResult > 20f && numberResult <= 30f)
        {
            StartCoroutine(RedeemPrize(1080f + 67.5f, 1));
        }
        else if(numberResult > 30f&& numberResult <= 40f)
        {
            StartCoroutine(RedeemPrize(1080f + 112.5f, 2));
        }
        else if(numberResult > 40f && numberResult <= 50f)
        {
            StartCoroutine(RedeemPrize(1080f + 157.5f, 3));
        }
        else if(numberResult > 50f && numberResult <= 55f)
        {
            StartCoroutine(RedeemPrize(1080f + 202.5f, 4));
        }
        else if(numberResult > 55f && numberResult <= 75f)
        {
            StartCoroutine(RedeemPrize(1080f + 247.5f, 5));
        }
        else if(numberResult > 75f && numberResult <= 80f)
        {
            StartCoroutine(RedeemPrize(1080f + 292.5f, 6));
        }
        else if(numberResult > 80f && numberResult <= 100f)
        {
            StartCoroutine(RedeemPrize(1080f + 337.5f, 7));
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene("Roulette");
    }

    void SetToRestart()
    {
        Button.GetComponent<Button>().onClick.RemoveListener(Spin);
        Button.GetComponent<Button>().onClick.AddListener(ResetScene);
        Button.transform.GetChild(0).GetComponent<Text>().text = "Restart";
    }

    void GetSpriteList(Transform obj)
    {
        foreach(Transform child in wheelParent)
        {
            spriteList.Add(child);
        }

        foreach(Transform child in obj)
        {
            if(prize != child.gameObject)
            {
                if(null == child)
                {
                    continue;
                }
                spriteList.Add(child);
                GetSpriteList(child);
            }
        }
        spriteList.Add(obj);
    }

    IEnumerator SpinTheWheel (float time, float endAngle)
    {
        spinning = true;
        
        float timer = 0.0f;        
        float startAngle = wheel.transform.eulerAngles.z;        
        endAngle = endAngle - startAngle;
        
        while (timer < time) {
        //to calculate rotation
            float angle = endAngle * animationCurves [0].Evaluate (timer / time) ;
            wheel.transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }
        
        wheel.transform.eulerAngles = new Vector3 (0.0f, 0.0f, endAngle + startAngle);
    }

    IEnumerator RedeemPrize(float degrees, int prizeNumber)
    {
        StartCoroutine(SpinTheWheel (3f, degrees));
        prize = prizes[prizeNumber];
        yield return new WaitForSeconds(3f);
        StartCoroutine(FadeOut(30f));
        sparks.transform.position = new Vector3(prize.transform.position.x, prize.transform.position.y, 1f);
        sparks.GetComponent<ParticleSystem>().Play();
        SetToRestart();
    }

    IEnumerator FadeOut(float fadeTime)
    {
        GetSpriteList(wheel_sections);

        float elapsedTime = 0f;

        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            foreach(Transform child in spriteList)
            {
                Color initialColor = child.gameObject.GetComponent<SpriteRenderer>().color;
                Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0);
                Color currentColor = Color.Lerp(initialColor, targetColor, elapsedTime/ fadeTime);
                child.gameObject.GetComponent<SpriteRenderer>().color = currentColor;
            }
            yield return null;
        }
    }

    public void Spin(bool testing)
    {
        numberResult = Random.Range(1f, 100f);
        if(numberResult <= 20f)
        {
            if(testing)
            {
                SectorOneCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 22.5f, 0));
            }
        }
        else if(numberResult > 20f && numberResult <= 30f)
        {
            if(testing)
            {
                SectorTwoCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 67.5f, 1));
            }
        }
        else if(numberResult > 30f && numberResult <= 40f)
        {
            if(testing)
            {
                SectorThreeCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 112.5f, 2));
            }
        }
        else if(numberResult > 40f && numberResult <= 50f)
        {
            if(testing)
            {
                SectorFourCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 157.5f, 3));
            }
        }
        else if(numberResult > 50f && numberResult <= 55f)
        {
            if(testing)
            {
                SectorFiveCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 202.5f, 4));
            }
        }
        else if(numberResult > 55f && numberResult <= 75f)
        {
            if(testing)
            {
                SectorSixCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 247.5f, 5));
            }
        }
        else if(numberResult > 75f && numberResult <= 80f)
        {
            if(testing)
            {
                SectorSevenCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 292.5f, 6));
            }
        }
        else if(numberResult > 80f && numberResult <= 100f)
        {
            if(testing)
            {
                SectorEightCount+=1;
            }
            else
            {
                StartCoroutine(RedeemPrize(1080f + 337.5f, 7));
            }
        }
    }

    public void RecordWins()
    {
        prizeWinCount.Add(SectorOneCount);
        prizeWinCount.Add(SectorTwoCount);
        prizeWinCount.Add(SectorThreeCount);
        prizeWinCount.Add(SectorFourCount);
        prizeWinCount.Add(SectorFiveCount);
        prizeWinCount.Add(SectorSixCount);
        prizeWinCount.Add(SectorSevenCount);
        prizeWinCount.Add(SectorEightCount);
    }
}
