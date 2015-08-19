using SiweiSoft.SAPIService.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SiweiSoft.SAPIServer
{
    public partial class SAPIServer : Form
    {
        public SAPIServer()
        {
            InitializeComponent();
        }

        private void SvcStartStop_Click(object sender, EventArgs e)
        {
            LogCommentM(CommentType.Info, "服务开启");
            LogCommentM(CommentType.Warn, "服务开启");
            LogCommentM(CommentType.Error, "服务开启");
        }

        public void LogCommentM(CommentType commentType, string comment)
        {
            string mark = null;
            ItemType itemType = ItemType.Error;
            if (commentType == CommentType.Info)
            {
                mark = "消息";
                itemType = ItemType.Info;
            }
            else if (commentType == CommentType.Warn)
            {
                mark = "警告";
                itemType = ItemType.Warn;
            }
            else if (commentType == CommentType.Error)
            {
                mark = "错误";
                itemType = ItemType.Error;
            }

            string message = String.Format("{0} [{1}] {2}", DateTime.Now.ToString(), mark, comment);
            SListBoxItem item = new SListBoxItem(message, itemType);

            //添加滚动效果，在添加记录前，先计算滚动条是否在底部，从而决定添加后是否自动滚动
            bool scoll = false;
            if (logsBox.TopIndex == logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight))
                scoll = true;
            //添加记录
            logsBox.Items.Add(item);
            //滚动到底部
            if (scoll)
                logsBox.TopIndex = logsBox.Items.Count - (int)(logsBox.Height / logsBox.ItemHeight);
        }
    }
}
