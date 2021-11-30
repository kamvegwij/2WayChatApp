using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using  Microsoft.AspNetCore.SignalR.Client;

namespace _2WayChatApp
{
    public partial class MainPage : ContentPage
    {
        HubConnection hubConnection;
        public MainPage()
        {
            InitializeComponent();
            RealTime();
        }
        async private void RealTime()
        {
            SignalRChatSetup();
            await SignalRConnect();
        }
        private void SignalRChatSetup()
        {
            var ip = "localhost"; //current computer
            hubConnection = new HubConnectionBuilder().WithUrl($"http://{ip}:5001/chatHub");
            hubConnection.On<string, string>("RecieveMessage", (pnum.message) =>
            {
                var recievedMessage = $"{pnum}: {message}"; // will display as e.g +27812345678: "message here"
                this.MessageHolder.Text += recievedMessage + "\n";
            }
        }
    }
    async Task SignalRConnect()
    {
        try
        {
            await hubConnection.StartAsync();
        }catch (Exception ex)
        {
            //throw exception
        }
    }
    private async void OnMessageBtnClick(object sender, EventArgs e)
    {
        await SignalRSendMessage(this.PhoneNumber.Text, this.Message.Text);
    }
    aync Task SignalRSendMessage(string pnum, string message)
    {
        try
        {
            await hubConnection.InvokeAsync("SendMessage", pnum, message);
        }
        catch (Exception ex)
        {
            //exception..
        }
    }
}
