using System;
using System.Windows.Forms;

namespace RedisPubSub.WinFormTest
{
    public partial class Form1 : Form
    {
        private string ChannelName = "test-channel";
        private RedisSubscriber _redisSubscriber;

        public Form1()
        {
            InitializeComponent();

            _redisSubscriber = new RedisSubscriber();

            _redisSubscriber.Subscribe(ChannelName, (message) =>
            {
                txtSubscriber1.Text = message;
            });

            _redisSubscriber.Subscribe(ChannelName, (message) =>
            {
                txtSubscriber2.Text = message;
            });
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            var message = txtPublisher.Text;
            _redisSubscriber.Publish(ChannelName, message);
        }
    }
}
