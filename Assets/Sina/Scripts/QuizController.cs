using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    public Text Quiz1;
    public Text Quiz2;
    public bool quizset = false;
    public bool quizset1 = false;



    public void setQuiz(int no,string dis,bool color)
    {
        if (no == 0) { 
            if (color) { Quiz1.color = Color.green; }else { Quiz1.color = Color.red; }
            Quiz1.text = dis;
        }
        else {
            if (color) { Quiz2.color = Color.green; } else { Quiz2.color = Color.red; }
            Quiz2.text = dis;
        }
    }

    public void setQuiz(int no, bool color)
    {
        if (no == 0)
        {
            if (color) { Quiz1.color = Color.green; } else { Quiz1.color = Color.red; }
            
        }
        else
        {
            if (color) { Quiz2.color = Color.green; } else { Quiz2.color = Color.red; }
            
        }
    }

    public void resetQuiz(int i)
    {
        if (i == 0)
        {
            Quiz1.text = "";
        }
        else
        {
            Quiz2.text = "";
        }
        
        
    }

    public void setNewQuiz(string dis)
    {
        if (quizset == false)
        {
            setQuiz(0, dis, false);
            quizset = true;
        }
        else
        {
            if (quizset1 == false)
            {
                setQuiz(1, dis, false);
                setQuiz(0, true);
                quizset1 = true;
            }
            else
            {
                Swap();
                setQuiz(1, dis, false);
                setQuiz(0, true);
            }

        }
    }

    public void Swap()
    {
        Quiz1.text = Quiz2.text;
    }

    public void ReWriteQuiz(string dis)
    {
        if (quizset1==false)
        {
            Quiz1.text = dis;
        }
        else
        {
            Quiz2.text= dis;
        }
    }
}
