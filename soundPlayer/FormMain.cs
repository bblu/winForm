using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
namespace winApp
{
    public partial class FormMain : Form
    {
        string[] titles = { "第一关：小脑幕切迹疝", "第二关：枕骨大孔疝" };
        string[] questions={ "识记：疝入组织：颞叶沟回\n      疝入孔道：小脑幕裂空（切迹）\n      疝入后果：受压脑组织缺血、水肿；动眼神经受挤压可产生动眼神经麻痹，造成瞳孔先小后大" +
                "，最后散大；同侧大脑脚受挤压造成对侧肢体瘫痪\n\n练习：小脑幕切迹疝可能有哪些表现，多选",
                "识记：疝入组织：小脑扁桃体\n      疝入孔道：枕骨大孔\n      疝入后果：直接压迫脑干，呼吸抑制出现较早\n\n练习：枕骨大孔疝可能有哪些表现，多选"};

        string[] items ={
                  "同侧瞳孔先小后大,对侧瞳孔先小后大,同侧瞳孔大小不定,对侧瞳孔大小不定,双侧瞳孔散大,进行性意识障碍;意识始终清醒,呼吸突然停止,同侧肢体瘫痪,对侧肢体瘫痪,双侧肢体瘫痪,有喷射性呕吐",
                  "同侧瞳孔先小后大,对侧瞳孔先小后大,瞳孔无明显变化,进行性意识障碍,意识清醒;呼吸突然停止,呼吸停止晚于意识障碍,呼吸停止早于意识障碍,无肢体瘫痪,有喷射性呕吐" };
        string[] answers ={ 
            "同侧瞳孔先小后大,进行性意识障碍,对侧肢体瘫痪,有喷射性呕吐",
        "瞳孔无明显变化,意识清醒,呼吸突然停止,呼吸停止早于意识障碍,无肢体瘫痪,有喷射性呕吐"
        };
        int[] ac={4,6};
        int curIndex = 0;
        SoundPlayer[] sounds={null,null};
        public FormMain()
        {
            InitializeComponent();
            imageBox.Visible = false;            
            loadQuestion(0);
            sounds[0] = new SoundPlayer(winApp.Properties.Resources.app);
            sounds[0].Load();
            sounds[1] = new SoundPlayer(winApp.Properties.Resources.sha);
            sounds[1].Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            imageBox.Visible = button1.Text == "提交";
            
            if (button1.Text == "重新开始")
            {
                loadQuestion(0);                
            }
            else if (button1.Text == "下一题")
            {
                if (curIndex == 2)
                    button1.Text = "闯关成功";
                else
                    loadQuestion(curIndex+1);              
            }
            else if(button1.Text == "提交")
            {
                if (isRight())
                {
                    imageBox.Image = winApp.Properties.Resources.img_0;
                    sounds[0].Play();    
                    if(curIndex<1)
                        button1.Text = "下一题";
                    else 
                        button1.Text = "闯关成功";
                }
                else
                {
                    imageBox.Image = winApp.Properties.Resources.img_1;
                    button1.Text = "重新开始";
                    sounds[1].Play();
                }
               
            }
            else if (button1.Text == "闯关成功")
            {
                button1.Text = "重新开始";
            }
        }
        private bool isRight()
        {
            System.Collections.ArrayList list = new ArrayList(10);        
            list.AddRange(checkedListBox0.CheckedItems);
            list.AddRange(checkedListBox1.CheckedItems);
            if(list.Count != ac[curIndex])
                return false;
            foreach(object obj in list)
            {
                if(answers[curIndex].IndexOf(obj.ToString())<0)
                    return false;
            }
            return true;
        }
        private void loadQuestion(int i)
        {
            curIndex = i;
            listBox1.SelectedIndex = i;
            string str = questions[i];
            textQuestion.Clear();
            textQuestion.AppendText(str);
            string[] strItems = items[i].Split(';');
            checkedListBox0.Items.Clear();
            checkedListBox1.Items.Clear();
            checkedListBox0.Items.AddRange(strItems[0].Split(','));
            checkedListBox1.Items.AddRange(strItems[1].Split(','));
            button1.Text = "提交";
            this.Text = titles[i];
        }

        private void imageBox_Click(object sender, EventArgs e)
        {
            if (button1.Text == "闯关成功")
            {
                loadQuestion(0);
            }
        }
    }
}
